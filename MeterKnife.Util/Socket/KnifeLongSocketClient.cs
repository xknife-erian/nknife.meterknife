using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Logging;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Socket.Interfaces;
using MeterKnife.Util.Tunnel.Common;
using MeterKnife.Util.Tunnel.Events;

namespace MeterKnife.Util.Socket
{
    public class KnifeLongSocketClient : ISocketClient, IDisposable
    {
        private static readonly ILog _logger = LogManager.GetLogger<KnifeLongSocketClient>();
        public KnifeLongSocketClient(bool reconnectFlag, bool isConnecting, 
            SocketClientConfig config, SocketSession socketSession)
        {
            _ReconnectFlag = reconnectFlag;
            _IsConnecting = isConnecting;
            _config = config;
            _socketSession = socketSession;
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
                var session = new TunnelSession();
                handler.Invoke(this, new SessionEventArgs(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        protected virtual void ProcessConnectionBrokenActive()
        {
            try
            {
                _logger.Debug("KnifeSocketClient执行主动断开");
                _socketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _socketSession.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                _logger.Error("Socket客户端Shutdown异常。", e);
            }
            _IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
            {
                var session = new TunnelSession();
                handler.Invoke(this, new SessionEventArgs(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        private void Close()
        {
            lock (_lockObj)
            {
                _socketSession.ResetBuffer();

                try
                {
                    _socketSession.State = SessionState.Closed;
                    if (_socketSession.AcceptSocket != null)
                    {
                        _socketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                        _socketSession.AcceptSocket.Disconnect(true);
                        _socketSession.AcceptSocket.Close();
                    }
                }
                catch (Exception e)
                {
                    _logger.Warn("Socket客户端关闭时有异常", e);
                }
                _logger.Debug("Socket客户端关闭");
            }
        }

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

        protected SocketClientConfig _config;
        protected EndPoint _EndPoint;

        private bool _IsConnecting; //true 正在进行连接, false表示连接动作完成
        protected bool _IsConnected; //连接状态，true表示已经连接上了
        private bool _ReconnectFlag = true;
        private bool _NeedReconnected; //是否重连
        private Thread _ReconnectedThread;

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        protected SocketSession _socketSession;

        private static readonly object _lockObj = new object();

        #endregion 成员变量

        #region IKnifeSocketClient接口

        public SocketConfig Config
        {
            get { return _config; }
            set { _config = (SocketClientConfig) value; }
        }

        public void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
            _EndPoint = new IPEndPoint(ipAddress, port);
        }

        #endregion

        #region IDataConnector接口

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;

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
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
            }

            try
            {
                _socketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                //_SocketSession.AcceptSocket.Disconnect(true);
                _socketSession.AcceptSocket.Close();
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

        public void Send(long id, byte[] data)
        {
            ProcessSendData(data);
        }

        public void SendAll(byte[] data)
        {
            ProcessSendData(data);
        }

        public void KillSession(long id)
        {
            ProcessConnectionBrokenActive();
        }

        #endregion

        #region 初始化

        protected void Initialize()
        {
            if (_IsDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");
            }

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
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
                        _ReconnectResetEvent.Reset(); //阻塞
                        _ReconnectResetEvent.WaitOne(_config.ReconnectInterval);
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
                    _ReconnectResetEvent.WaitOne(_config.ReconnectInterval);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KnifeLongSocketClient()
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
                //Close();

                if (_socketSession.AcceptSocket == null || !_socketSession.AcceptSocket.Connected)
                {
                    _socketSession.AcceptSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = Config.SendTimeout,
                        ReceiveTimeout = Config.ReceiveTimeout,
                        SendBufferSize = Config.MaxBufferSize,
                        ReceiveBufferSize = Config.ReceiveBufferSize
                    };
                }

                _IsConnecting = true;

                _SynConnectWaitEventReset.Reset();
                _socketSession.AcceptSocket.BeginConnect(ipAddress, port, EndAsyncConnect, this);

                _SynConnectWaitEventReset.WaitOne();
            }
            catch (Exception e)
            {
                _IsConnecting = false;
                _logger.Error(string.Format("客户端异步连接远端时异常.{0}", e));
            }
        }

        private void EndAsyncConnect(IAsyncResult ar)
        {
            _IsConnecting = false;
            _IsConnected = true;
            //如果有自动重连，则通知timer停止重连
            StopReconnect();
            _SynConnectWaitEventReset.Set(); //释放连接等待的阻塞信号

            var handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
            }

            ReceiveDatagram();
        }

        private void ReceiveDatagram()
        {
            try // 一个客户端连续做连接 或连接后立即断开，容易在该处产生错误，系统不认为是错误
            {
                // 开始接受来自该客户端的数据
                _socketSession.AcceptSocket.BeginReceive(_socketSession.ReceiveBuffer, 0, _socketSession.ReceiveBufferSize, SocketFlags.None, EndReceiveDatagram, this);
            }
            catch (Exception err) // 读 Socket 异常，准备关闭该会话
            {
                _socketSession.DisconnectType = DisconnectType.Exception;
                _socketSession.State = SessionState.Inactive;

                OnSessionReceiveException();
            }
        }

        private void EndReceiveDatagram(IAsyncResult iar)
        {
            if (!_socketSession.AcceptSocket.Connected)
            {
                OnSessionReceiveException();
                return;
            }

            try
            {
                // Shutdown 时将调用 ReceiveData，此时也可能收到 0 长数据包
                var readBytesLength = _socketSession.AcceptSocket.EndReceive(iar);
                iar.AsyncWaitHandle.Close();

                if (readBytesLength == 0)
                {
                    _socketSession.DisconnectType = DisconnectType.Normal;
                    _socketSession.State = SessionState.Inactive;
                }
                else // 正常数据包
                {
                    _socketSession.LastSessionTime = DateTime.Now;
                    // 合并报文，按报文头、尾字符标志抽取报文，将包交给数据处理器
                    var data = new byte[readBytesLength];
                    Array.Copy(_socketSession.ReceiveBuffer, 0, data, 0, readBytesLength);
                    PrcessReceiveData(data);
                    ReceiveDatagram();
                }
            }
            catch (Exception err) // 读 socket 异常，关闭该会话，系统不认为是错误（这种错误可能太多）
            {
                if (_socketSession.State == SessionState.Active)
                {
                    _socketSession.DisconnectType = DisconnectType.Exception;
                    _socketSession.State = SessionState.Inactive;

                    OnSessionReceiveException();
                }
            }
        }

        private void OnSessionReceiveException()
        {
            ProcessConnectionBrokenPassive();
        }

        protected virtual void PrcessReceiveData(byte[] data)
        {
            var handler = DataReceived;
            if (handler != null)
            {
//                _SocketSession.Id = _EndPoint;
                _socketSession.Data = data;
                handler.Invoke(this, new SessionEventArgs(_socketSession));
            }
        }

        #endregion

        #region 发送消息

        protected virtual void ProcessSendData(byte[] data)
        {
            try
            {
                if (_socketSession.AcceptSocket != null && _socketSession.AcceptSocket.Connected)
                    _socketSession.AcceptSocket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, data);
            }
            catch (SocketException e)
            {
                _logger.Info(string.Format("Client发送时发现连接中断。{0}", e.Data), e);
                _IsConnected = false;

                var handler = SessionBroken;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
                }
                //如果有自动重连，则需要启用自动重连
                StartReconnect();
            }
        }

        protected virtual void AsynEndSend(IAsyncResult result)
        {
            try
            {
                var data = result.AsyncState as byte[];
                _socketSession.AcceptSocket.EndSend(result);

                var dataSentHandler = DataSent;
                if (dataSentHandler != null)
                {
                    dataSentHandler.Invoke(this, new SessionEventArgs(new TunnelSession
                    {
//                        Id = _SocketSession.AcceptSocket.RemoteEndPoint,
                        Data = data
                    }));
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