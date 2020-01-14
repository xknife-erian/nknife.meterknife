using System;
using System.Collections.Generic;
using System.Linq;
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
    public class KnifeSocketServer : ISocketServer, IDisposable
    {
        private const int ACCEPT_LISTEN_TIME_INTERVAL = 25; // 侦听论询时间间隔(ms)
        private const int CHECK_SESSION_TABLE_TIME_INTERVAL = 100; // 清理Timer的时间间隔(ms)
        //private const int MAX_SESSION_TIMEOUT = 60; // 1 minutes，不是心跳间隔，但针对长连接也需要有个时间限制，太长时间无动作也要清除，如果为0表示不超时清除

        private static readonly ILog _logger = LogManager.GetLogger<KnifeSocketServer>();

        private readonly ManualResetEvent _CheckAcceptListenResetEvent;
        private readonly ManualResetEvent _CheckSessionTableResetEvent;
        private IPAddress _IpAddress;
        private bool _IsDisposed;
        private bool _IsServerClosed = true;
        private bool _IsServerListenPaused;
        private Socket _ListenSocket;
        private int _Port;
        private int _SessionCount;

        protected SocketSessionMap _SessionMap = new SocketSessionMap();
        protected SocketServerConfig _Config = DI.Get<SocketServerConfig>();

        #region 构造

        public KnifeSocketServer()
        {
            _CheckAcceptListenResetEvent = new ManualResetEvent(true);
            _CheckSessionTableResetEvent = new ManualResetEvent(true);
        }

        ~KnifeSocketServer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            if (!_IsDisposed)
            {
                _IsDisposed = true;
                Close();
                Dispose(true);
                GC.SuppressFinalize(this); // Finalize 不会第二次执行
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) // 对象正在被显示释放, 不是执行 Finalize()
            {
                lock (_SessionMap)
                {
                    _SessionMap.Clear(); // 释放托管资源
                }
            }

            if (_CheckAcceptListenResetEvent != null)
            {
                _CheckAcceptListenResetEvent.Close(); // 释放非托管资源
            }

            if (_CheckSessionTableResetEvent != null)
            {
                _CheckSessionTableResetEvent.Close();
            }
        }

        #endregion

        #region IKnifeSocketServer接口

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;

        public SocketConfig Config
        {
            get { return _Config; }
            set { _Config = (SocketServerConfig) value; }
        }

        public virtual void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }

        public bool Stop()
        {
            Close();
            return true;
        }

        public bool Start()
        {
            _IsServerClosed = true; // 在其它方法中要判断该字段
            _IsServerListenPaused = true;

            Close();

            try
            {
                if (!CreateServerSocket())
                    return false;
                _IsServerClosed = false;

                var checkSessionThread = new Thread(CheckSessionTable) {IsBackground = true};
                checkSessionThread.Start();

                var serverListenThread = new Thread(StartServerListen) {IsBackground = true};
                serverListenThread.Start();
                _IsServerListenPaused = false;

                _logger.Info("socket server启动");
            }
            catch (Exception err)
            {
                _logger.Info(string.Format("socket server启动异常"), err);
            }
            return !_IsServerClosed;
        }

        public void Send(long id, byte[] data)
        {
            SocketSession session = null;
            if (_SessionMap.ContainsKey(id))
            {
                session = _SessionMap[id];
            }
            if (session != null)
            {
                SendDatagram(session, data);
            }
        }

        public void SendAll(byte[] data)
        {
            foreach (var session in _SessionMap.Values())
            {
                SendDatagram(session, data);
            }
        }

        public void KillSession(long id)
        {
            SocketSession session = null;
            if (_SessionMap.ContainsKey(id))
            {
                session = _SessionMap[id];
            }

            if (session != null)
            {
                session.DisconnectType = DisconnectType.Timeout;
                SetSessionInactive(session); // 标记为将关闭、准备断开
            }
        }

        #endregion

        #region Server端处理

        private bool CreateServerSocket()
        {
            try
            {
                var ipEndPoint = new IPEndPoint(_IpAddress, _Port);
                _ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _ListenSocket.Bind(ipEndPoint);
                _ListenSocket.ReceiveBufferSize = Config.ReceiveBufferSize;
                _ListenSocket.SendBufferSize = Config.SendBufferSize;
                _ListenSocket.SendTimeout = Config.SendTimeout;
                _ListenSocket.ReceiveTimeout = Config.ReceiveTimeout;
                _ListenSocket.NoDelay = true;
                _ListenSocket.Listen(16);
                return true;
            }
            catch (Exception err)
            {
                _logger.Warn(string.Format("CreateServerSocket异常:{0}", err), err);
                return false;
            }
        }

        private void CloseServerSocket()
        {
            if (_ListenSocket == null)
            {
                return;
            }

            try
            {
                if (_SessionMap != null && _SessionMap.Count > 0)
                {
                    //lock (_SessionMap)
                    {
                        _SessionMap.Clear();
                    }
                }
                _ListenSocket.Close();
            }
            catch (Exception ex)
            {
                _logger.Warn(string.Format("CloseServerSocket异常:{0}", ex));
            }
            finally
            {
                _ListenSocket = null;
            }
        }

        /// <summary>
        ///     侦听客户端连接请求
        /// </summary>
        private void StartServerListen()
        {
            _CheckAcceptListenResetEvent.Reset();
            Socket clientSocket = null;

            while (!_IsServerClosed)
            {
                if (_IsServerListenPaused) // pause server
                {
                    CloseServerSocket();
                    Thread.Sleep(ACCEPT_LISTEN_TIME_INTERVAL);
                    continue;
                }

                if (_ListenSocket == null)
                {
                    CreateServerSocket();
                    continue;
                }

                try
                {
                    if (_ListenSocket.Poll(ACCEPT_LISTEN_TIME_INTERVAL, SelectMode.SelectRead))
                    {
                        // 频繁关闭、启动时，这里容易产生错误（提示套接字只能有一个）
                        clientSocket = _ListenSocket.Accept();

                        if (clientSocket.Connected)
                        {
                            if (_SessionCount >= Config.MaxConnectCount) // 连接数超过上限
                            {
                                OnSessionRejected(); // 拒绝登录请求
                                CloseClientSocket(clientSocket);
                            }
                            else
                            {
                                AddSession(clientSocket); // 添加到队列中, 并调用异步接收方法
                            }
                        }
                        else // clientSocket is null or connected == false
                        {
                            CloseClientSocket(clientSocket);
                        }
                    }
                }
                catch (Exception) // 侦听连接的异常频繁, 不捕获异常
                {
                    CloseClientSocket(clientSocket);
                }
            }

            _CheckAcceptListenResetEvent.Set();
        }

        /// <summary>
        ///     资源清理线程, 分若干步完成
        /// </summary>
        private void CheckSessionTable()
        {
            _CheckSessionTableResetEvent.Reset();

            while (!_IsServerClosed)
            {
                var sessionIdList = new List<long>();

//                var dataArray = new SocketSession[_SessionMap.Values().Count];
//                _SessionMap.Values().CopyTo(dataArray, 0);

                var dataArray = _SessionMap.Values().ToArray();

                foreach (var session in dataArray)
                {
                    if (_IsServerClosed)
                    {
                        break;
                    }

                    if (session.State == SessionState.Inactive) // 分三步清除一个 Session
                    {
                        ShutdownSession(session); // 第一步: shutdown, 结束异步事件
                    }
                    else if (session.State == SessionState.Shutdown)
                    {
                        CloseSession(session); // 第二步: Close
                    }
                    else if (session.State == SessionState.Closed)
                    {
                        sessionIdList.Add(session.Id);
                        DisconnectSession(session);
                    }
                    else // 正常的会话 Active
                    {
                        CheckSessionTimeout(session); // 判超时，若是则标记
                    }
                    foreach (var id in sessionIdList) // 统一清除
                    {
                        _SessionMap.Remove(id);
                    }

                    sessionIdList.Clear();
                }

                Thread.Sleep(CHECK_SESSION_TABLE_TIME_INTERVAL);
            }

            _CheckSessionTableResetEvent.Set();
        }

        private void Close()
        {
            if (_IsServerClosed)
            {
                return;
            }

            _IsServerClosed = true;
            _IsServerListenPaused = true;

            _CheckAcceptListenResetEvent.WaitOne(); // 等待3个线程
            _CheckSessionTableResetEvent.WaitOne();

            if (_SessionMap != null)
            {
                lock (_SessionMap)
                {
                    foreach (var session in _SessionMap.Values())
                    {
                        CloseSession(session);
                    }
                }
            }

            CloseServerSocket();

            if (_SessionMap != null) // 清空会话列表
            {
                lock (_SessionMap)
                {
                    _SessionMap.Clear();
                }
            }

            _logger.Info("socket server closed");
        }

        /// <summary>
        ///     强制关闭客户端请求时的 Socket
        /// </summary>
        private void CloseClientSocket(Socket socket)
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    Console.Write(e.Data);
                } // 强制关闭, 忽略错误
            }
        }

        #endregion

        #region Session操作

        /// <summary>
        ///     增加一个会话对象
        /// </summary>
        private void AddSession(Socket clientSocket)
        {
            var remoteEndPoint = clientSocket.RemoteEndPoint;
            var session = DI.Get<SocketSession>();
            session.AcceptSocket = clientSocket;
            session.LastSessionTime = DateTime.Now;
            _SessionMap.Add(session);
            _logger.InfoFormat("Server: IP地址:{0}的连接已放入客户端池中。池中:{1}", remoteEndPoint, _SessionMap.Count);

            ReceiveDatagram(session);

            OnSessionConnected(session);
        }

        public void CheckSessionTimeout(SocketSession session)
        {
            if (_Config.MaxSessionTimeout == 0) //该参数为0则不检查
                return;
            var ts = DateTime.Now.Subtract(session.LastSessionTime);
            var elapsedSecond = Math.Abs((int) ts.TotalSeconds);

            if (elapsedSecond > _Config.MaxSessionTimeout) // 超时，则准备断开连接
            {
                session.DisconnectType = DisconnectType.Timeout;
                SetSessionInactive(session); // 标记为将关闭、准备断开

                _logger.Info(string.Format("Session{0}长期没有通讯，状态设置为InActive", session.Id));
            }
        }

        private void ShutdownSession(SocketSession session)
        {
            if (session.State != SessionState.Inactive || session.AcceptSocket == null) // Inactive 状态才能 Shutdown
            {
                return;
            }

            session.State = SessionState.Shutdown;
            try
            {
                session.AcceptSocket.Shutdown(SocketShutdown.Both); // 目的：结束异步事件
            }
            catch (Exception e)
            {
                Console.Write(e.Data);
            }
        }

        private void CloseSession(SocketSession session)
        {
            if (session.State != SessionState.Shutdown || session.AcceptSocket == null) // Shutdown 状态才能 Close
            {
                return;
            }

            session.ResetBuffer();

            try
            {
                session.State = SessionState.Closed;
                session.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Data);
            }
        }

        private void SetSessionInactive(SocketSession session)
        {
            if (session.State == SessionState.Active)
            {
                session.State = SessionState.Inactive;
                session.DisconnectType = DisconnectType.Normal;
            }
        }

        private void DisconnectSession(SocketSession session)
        {
            if (session.DisconnectType == DisconnectType.Normal)
            {
                OnSessionDisconnected(session);
            }
            else if (session.DisconnectType == DisconnectType.Timeout)
            {
                OnSessionTimeout(session);
            }
        }

        #endregion

        #region 异步发送接收

        private void SendDatagram(SocketSession session, byte[] data)
        {
            if (session.State != SessionState.Active)
            {
                return;
            }

            try
            {
                session.AcceptSocket.BeginSend(data, 0, data.Length, SocketFlags.None, EndSendDatagram, session);
            }
            catch (Exception err) // 写 socket 异常，准备关闭该会话
            {
                session.DisconnectType = DisconnectType.Exception;
                session.State = SessionState.Inactive;

                OnSessionSendException(err);
            }
        }

        /// <summary>
        ///     发送数据完成处理函数, iar 为目标客户端 Session
        /// </summary>
        private void EndSendDatagram(IAsyncResult iar)
        {
            var session = iar.AsyncState as SocketSession;
            if (session != null && session.State != SessionState.Active)
            {
                return;
            }

            if (session != null && !session.AcceptSocket.Connected)
            {
                SetSessionInactive(session);
                return;
            }

            try
            {
                if (session != null) 
                    session.AcceptSocket.EndSend(iar);
                iar.AsyncWaitHandle.Close();
            }
            catch (Exception err) // 写 socket 异常，准备关闭该会话
            {
                if (session != null)
                {
                    session.DisconnectType = DisconnectType.Exception;
                    session.State = SessionState.Inactive;
                }
                OnSessionSendException(err);
            }
        }

        private void ReceiveDatagram(SocketSession session)
        {
            if (session.State != SessionState.Active)
            {
                return;
            }

            try // 一个客户端连续做连接 或连接后立即断开，容易在该处产生错误，系统不认为是错误
            {
                // 开始接受来自该客户端的数据
                session.AcceptSocket.BeginReceive(session.ReceiveBuffer, 0, session.ReceiveBufferSize, SocketFlags.None, EndReceiveDatagram, session);
            }
            catch (Exception err) // 读 Socket 异常，准备关闭该会话
            {
                session.DisconnectType = DisconnectType.Exception;
                session.State = SessionState.Inactive;

                OnSessionReceiveException(err);
            }
        }

        private void EndReceiveDatagram(IAsyncResult iar)
        {
            var session = iar.AsyncState as SocketSession;
            if (session == null)
            {
                return;
            }
            if (session.State != SessionState.Active)
            {
                return;
            }

            if (!session.AcceptSocket.Connected)
            {
                SetSessionInactive(session);
                return;
            }

            try
            {
                // Shutdown 时将调用 ReceiveData，此时也可能收到 0 长数据包
                var readBytesLength = session.AcceptSocket.EndReceive(iar);
                iar.AsyncWaitHandle.Close();

                if (readBytesLength == 0)
                {
                    session.DisconnectType = DisconnectType.Normal;
                    session.State = SessionState.Inactive;
                }
                else // 正常数据包
                {
                    session.LastSessionTime = DateTime.Now;
                    // 合并报文，按报文头、尾字符标志抽取报文，将包交给数据处理器
                    var data = new byte[readBytesLength];
                    Array.Copy(session.ReceiveBuffer, 0, data, 0, readBytesLength);
                    PrcoessReceivedData(session.Id, data);
                    ReceiveDatagram(session);
                }
            }
            catch (Exception err) // 读 socket 异常，关闭该会话，系统不认为是错误（这种错误可能太多）
            {
                if (session.State == SessionState.Active)
                {
                    session.DisconnectType = DisconnectType.Exception;
                    session.State = SessionState.Inactive;

                    OnSessionReceiveException(err);
                }
            }
        }

        /// <summary>
        ///     处理接收到的数据，此时e.BytesTransferred > 0
        /// </summary>
        protected virtual void PrcoessReceivedData(long endPoint, byte[] data)
        {
            var handler = DataReceived;
            if (handler != null)
            {
                if (_SessionMap.ContainsKey(endPoint))
                {

                    handler.Invoke(this, new SessionEventArgs(new TunnelSession
                    {
                        Id = endPoint,
                        Data = data
                    }));
                }
                else
                {
                    _logger.Warn(string.Format("SessionMap中未包含指定Key的Session:{0}", endPoint));
                }
            }
        }

        #endregion

        #region Session事件

        protected virtual void OnSessionRejected()
        {
            _logger.Debug("OnSessionRejected");
        }

        protected virtual void OnSessionConnected(SocketSession session)
        {
            Interlocked.Increment(ref _SessionCount);

            var handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(session));
            }
        }

        protected virtual void OnSessionDisconnected(SocketSession session)
        {
            Interlocked.Decrement(ref _SessionCount);

            var handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(session));
            }
        }

        protected virtual void OnSessionTimeout(SocketSession session)
        {
            Interlocked.Decrement(ref _SessionCount);

            var handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(session));
            }
        }

        protected virtual void OnSessionReceiveException(Exception err)
        {
            Interlocked.Decrement(ref _SessionCount);
            _logger.Warn(string.Format("OnSessionReceiveException:{0}", err));
        }

        protected virtual void OnSessionSendException(Exception err)
        {
            Interlocked.Decrement(ref _SessionCount);
            _logger.Warn(string.Format("OnSessionSendException:{0}", err));
        }

        #endregion
    }

    public enum DisconnectType
    {
        Normal, // disconnect normally
        Timeout, // disconnect because of timeout
        Exception // disconnect because of exception
    }

    public enum SessionState
    {
        Active, // state is active
        Inactive, // session is inactive and will be closed
        Shutdown, // session is shutdownling
        Closed // session is closed
    }
}