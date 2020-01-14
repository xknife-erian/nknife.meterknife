using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    /// <summary>
    ///     实现了socket长连接客户端，使用异步事件模型，自动重连
    /// </summary>
    public class KnifeSocketClient : IDisposable, IKnifeSocketClient
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员变量

        protected IPAddress _IpAddress;
        protected int _Port;

        /// <summary>
        ///     异步连接的控制，连接事件完成后释放信号，通过IsConnected判断是否连接成功
        /// </summary>
        private readonly ManualResetEvent _SynConnectWaitEventReset = new ManualResetEvent(false);

        /// <summary>
        ///     重连线程中的阻塞超时控制
        /// </summary>
        private readonly ManualResetEvent _ReconnectResetEvent = new ManualResetEvent(false);

        protected KnifeSocketClientConfig _Config = DI.Get<KnifeSocketClientConfig>();
        protected EndPoint _EndPoint;

        private bool _IsConnecting; //true 正在进行连接, false表示连接动作完成
        protected bool _IsConnected; //连接状态，true表示已经连接上了
        private bool _ReconnectFlag;
        private bool _NeedReconnected; //是否重连
        private Thread _ReconnectedThread;

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        protected KnifeSocketSession _SocketSession;

        private static readonly object _lockObj = new object();

        public KnifeSocketClient()
        {
        }

        #endregion 成员变量

        #region IKnifeSocketClient接口

        public KnifeSocketConfig Config
        {
            get { return _Config; }
            set { _Config = (KnifeSocketClientConfig) value; }
        }

        public void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
            _EndPoint = new IPEndPoint(ipAddress, port);
        }

        #endregion

        #region IDataConnector接口

        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBuilt;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBroken;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataReceived;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataSent;

        public bool Start()
        {
            Initialize();
            AsyncConnect(_IpAddress, _Port);
            _ReconnectedThread.Start();
            return true;
        }

        public bool Stop()
        {
            StopReconnect();

            _ReconnectFlag = false;
            _ReconnectResetEvent.Set();

            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                {
                    Id = _EndPoint
                }));

            try
            {
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _SocketSession.AcceptSocket.Disconnect(true);
                _SocketSession.AcceptSocket.Close();
                return true;
            }
            catch (Exception e)
            {
                _logger.Debug("Socket客户端Shutdown异常。", e);
                return false;
            }
        }

        #endregion

        #region ISessionProvider

        public void Send(EndPoint id, byte[] data)
        {
            ProcessSendData(_SocketSession.Id, _SocketSession.AcceptSocket, data);
        }

        public void SendAll(byte[] data)
        {
            ProcessSendData(_SocketSession.Id, _SocketSession.AcceptSocket, data);
        }

        public void KillSession(EndPoint id)
        {
            ProcessConnectionBrokenActive();
        }

        #endregion

        #region 初始化

        protected void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
            _SocketSession = DI.Get<KnifeSocketSession>();
            _SocketSession.Id = ipPoint;

            _ReconnectFlag = true;
            _ReconnectedThread = new Thread(ReconnectedLoop);
        }

        private void ReconnectedLoop()
        {
            while (_ReconnectFlag)
            {
                if (!_IsConnected) //重连检查，仅启用了自动重连的时候做
                {
                    _NeedReconnected = true;
                }

                if (_NeedReconnected) //需要重连
                {
                    if (!_IsConnected && !_IsConnecting) //未连接
                    {
                        _logger.Debug("Client发起重连尝试");
                        AsyncConnect(_IpAddress, _Port);
                        _ReconnectResetEvent.Reset();//阻塞
                        _ReconnectResetEvent.WaitOne(_Config.ReconnectInterval);
                    }
                    else //已连接，则不需要重连了
                    {
                        _NeedReconnected = false;
                    }
                }
                else
                {
                    //阻塞
                    _ReconnectResetEvent.Reset();
                    _ReconnectResetEvent.WaitOne(_Config.ReconnectInterval);
                }
            }
            _logger.Debug("SocketClient退出重连循环");
        }

        protected virtual void StartReconnect()
        {
            _logger.Info(string.Format("Client启用自动重连。"));
            _NeedReconnected = true;
        }

        protected virtual void StopReconnect()
        {
            _NeedReconnected = false;
        }

        #endregion

        #region IDisposable

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public KnifeSocketClient(bool reconnectFlag, bool isConnecting)
        {
            _ReconnectFlag = reconnectFlag;
            _IsConnecting = isConnecting;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KnifeSocketClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed || disposing)
            {
                Stop();
                _IsDisposed = true;
            }
        }

        #endregion

        #region 监听

        /// <summary>
        ///     异步连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        protected virtual void AsyncConnect(IPAddress ipAddress, int port)
        {
            try
            {
                if (_SocketSession.AcceptSocket == null || !_SocketSession.AcceptSocket.Connected)
                {
                    _SocketSession.AcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = Config.SendTimeout,
                        ReceiveTimeout = Config.ReceiveTimeout,
                        SendBufferSize = Config.MaxBufferSize,
                        ReceiveBufferSize = Config.ReceiveBufferSize
                    };
                }

                _IsConnecting = true;

                var asyncConnectEventArgs = new SocketAsyncEventArgs {RemoteEndPoint = _EndPoint};
                asyncConnectEventArgs.Completed += AsynCompleted;

                if (!_SocketSession.AcceptSocket.ConnectAsync(asyncConnectEventArgs))
                {
                    lock (_lockObj)
                    {
                        ProcessConnect(asyncConnectEventArgs);
                    }
                }
            }
            catch (Exception e)
            {
                _IsConnecting = false;
                _logger.Error(string.Format("客户端异步连接远端时异常.{0}", e));
            }
        }

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (null == e)
                return;
            lock (_lockObj)
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        ProcessConnect(e);
                        break;
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(e);
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSend(e);
                        break;
                    case SocketAsyncOperation.Disconnect:
                        ProcessDisconnectAndCloseSocket(e);
                        break;
                    default:
                        _logger.Debug(string.Format("未处理的Socket状态：{0}", e.LastOperation));
                        break;
                }
            }
        }

        protected virtual void ProcessConnect(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success:
                case SocketError.IsConnected: //连接成功了
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _IsConnected = true;

                        var handler = SessionBuilt;
                        if (handler != null)
                        {
                            handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                            {
                                Id = _EndPoint
                            }));
                        }

                        //如果有自动重连，则通知timer停止重连
                        StopReconnect();

                        _SynConnectWaitEventReset.Set(); //释放连接等待的阻塞信号

                        _logger.Debug("client连接成功，开始接收数据");

                        var data = new byte[Config.ReceiveBufferSize];
                        e.SetBuffer(data, 0, data.Length); //设置数据包
                        if (!_SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                            ProcessReceive(e);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当成功连接时的处理发生异常.", ex);
                    }
                    break;
                }
                default: //连接失败
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _logger.Warn(string.Format("连接失败:{0}", e.SocketError));
                        _IsConnected = false;

                        //自动重连，则尝试重连
                        StartReconnect();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当连接未成功时的处理发生异常.", ex);
                    }
                    break;
                }
            }
            _IsConnecting = false;
        }

        protected virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                switch (e.SocketError)
                {
                    case SocketError.Success: //连接正常
                    {
                        if (e.BytesTransferred > 0) //有数据
                        {
                            PrcessReceiveData(e); //处理收到的数据

                            //继续接收
                            var data = new byte[Config.ReceiveBufferSize];
                            e.SetBuffer(data, 0, data.Length); //设置数据包
                            if (!_SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                                ProcessReceive(e);
                        }
                        else
                        {
                            var data = new byte[Config.ReceiveBufferSize];
                            e.SetBuffer(data, 0, data.Length); //设置数据包
                            if (!_SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                                ProcessReceive(e);
                        }
                        break;
                    }
                    default: //其他未处理的Socket状态
                    {
                        _logger.Info(string.Format("Client接收时发现连接中断。{0}", e.SocketError));
                        break;
                    }
                }
            }
            catch (Exception ex) //接收处理异常了
            {
                _logger.Error(string.Format("Client接收处理发生异常。"), ex);
            }
        }

        /// <summary>
        ///     处理连接被动中断，从远端发起的中断，本地接收或发送时出现异常货socket已经释放的情况
        /// </summary>
        protected virtual void ProcessConnectionBrokenPassive()
        {
            _IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
            {
                var session = new EndPointKnifeTunnelSession() {Id = _EndPoint};
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        protected virtual void ProcessConnectionBrokenActive()
        {
            try
            {
                _logger.Debug("KnifeSocketClient执行主动断开");
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _SocketSession.AcceptSocket.Disconnect(true);
                _SocketSession.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                _logger.Error("Socket客户端Shutdown异常。", e);
            }
            _IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
            {
                var session = new EndPointKnifeTunnelSession() { Id = _EndPoint };
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        protected virtual void PrcessReceiveData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            var handler = DataReceived;
            if (handler != null)
            {
                _SocketSession.Id = _EndPoint;
                _SocketSession.Data = data;
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(_SocketSession));
            }
        }

        protected virtual void ProcessSend(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success: //连接正常
                {
                    _logger.Debug(string.Format("ClientSend: {0}", e.Buffer.ToHexString()));
                    var dataSentHandler = DataSent;
                    if (dataSentHandler != null)
                    {
                        dataSentHandler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                        {
                            Id = e.RemoteEndPoint,
                            Data = e.Buffer
                        }));
                    }
                    return;
                }
                default:
                {
                    _logger.Info(string.Format("Client发送时发现未处理的Socket状态。{0}", e.SocketError));
                    _IsConnected = false;

                    var handler = SessionBroken;
                    if (handler != null)
                    {
                        var session = new EndPointKnifeTunnelSession {Id = _EndPoint};
                        handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(session));
                    }
                    //如果有自动重连，则需要启用自动重连
                    StartReconnect();
                    return;
                }
            }
        }

        private void ProcessDisconnectAndCloseSocket(SocketAsyncEventArgs receiveSendEventArgs)
        {
            try
            {
                _SocketSession.AcceptSocket.Close();
                _SocketSession.AcceptSocket = null;

                _IsConnected = false;

                var handler = SessionBroken;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                    {
                        Id = _EndPoint
                    }));
                }
            }
            catch (Exception e)
            {
                _logger.Warn("关闭Socket客户端异常。", e);
            }
        }

        #endregion

        #region 发送消息

        protected virtual void ProcessSendData(EndPoint id, Socket socket, byte[] data)
        {
            try
            {
                var socketSession = new KnifeSocketSession
                {
                    AcceptSocket = socket,
                    Data = data,
                    Id = id
                };
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, socketSession);
            }
            catch (SocketException e)
            {
                _logger.Info(string.Format("Client发送时发现连接中断。"));
                _IsConnected = false;

                var handler = SessionBroken;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                    {
                        Id = _EndPoint
                    }));
                }
                //如果有自动重连，则需要启用自动重连
                StartReconnect();
            }
        }

        protected virtual void AsynEndSend(IAsyncResult result)
        {
            try
            {
                var session = result.AsyncState as KnifeSocketSession;
                if (session != null)
                {
                    var data = session.Data;
                    var id = session.Id;
                    var length = session.AcceptSocket.EndSend(result);
                    if (length != data.Length)
                    {
                        _logger.Warn(string.Format("发送数据长度异常:expected:{1},actual:{0}", length, data.Length));
                        return;
                    }

                    //发送成功
                    _logger.Debug(string.Format("ClientSend: {0}", data.ToHexString()));
                    var dataSentHandler = DataSent;
                    if (dataSentHandler != null)
                    {
                        dataSentHandler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                        {
                            Id = id,
                            Data = data
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.WarnFormat("结束挂起的异步发送异常.{0}", e.Message);
            }
        }

        #endregion
    }
}