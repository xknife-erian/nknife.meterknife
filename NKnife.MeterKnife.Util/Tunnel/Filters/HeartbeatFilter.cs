using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NKnife.MeterKnife.Util.Tunnel.Base;
using NKnife.MeterKnife.Util.Tunnel.Common;
using NKnife.MeterKnife.Util.Tunnel.Events;
using Timer = System.Timers.Timer;

namespace NKnife.MeterKnife.Util.Tunnel.Filters
{
    public class HeartbeatFilter : BaseTunnelFilter
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Dictionary<long, HeartBeatSession> _HeartBeartSessionMap = new Dictionary<long, HeartBeatSession>();
        private readonly ManualResetEvent _ReceiveProcessingResetEvent = new ManualResetEvent(false);
        private Timer _BeatingTimer;
        private bool _IsTimerStarted;
        private bool _OnReceiveProcessing;

        public HeartbeatFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000*15;
            EnableStrictMode = false;
            HeartBeatMode = HeartBeatMode.Responsive;
        }

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        /// <summary>
        ///     严格模式开关
        /// </summary>
        /// <returns>
        ///     true  心跳返回内容一定要和HeartBeat类中定义的ReplayOfClient一致才算有心跳响应
        ///     false 心跳返回任何内容均算有心跳相应
        /// </returns>
        public bool EnableStrictMode { get; set; }

        /// <summary>
        ///     Active主动模式，
        ///     主动模式：主动定时向远端发出心跳请求，如果发送失败（收到来自DataConnector的SessionBroken消息）
        ///     或者发送后经过指定时间没有收到心跳响应，则发出HeartBroken消息，Listener会发出KillSession消息，
        ///     DataConnector会杀死对应的Session后，发出SessionBroken消息
        ///     Passive被动模式：不主动发出心跳请求，但连接建立后，经过指定时间没有收到来自远端的心跳请求，
        ///     则发出HeartBroken消息，Listener会发出KillSession消息，DataConnector会杀死对应的Session后，
        ///     发出SessionBroken消息
        ///     不论是主动模式还是被动模式，如果收到心跳请求，都会做出心跳回复
        ///     Responsive应答模式：不主动发出心跳请求，任何时候接收到心跳请求后，进行心跳应答
        /// </summary>
        public HeartBeatMode HeartBeatMode { get; set; }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            if (_OnReceiveProcessing)
            {
                _ReceiveProcessingResetEvent.Reset();
                _ReceiveProcessingResetEvent.WaitOne();
            }

            var todoList = new List<long>(0); //待移除
            foreach (var pair in _HeartBeartSessionMap)
            {
                var endpoint = pair.Key;
                var session = pair.Value;
                if (!session.GetWaitForReply())
                {
                    if (HeartBeatMode == HeartBeatMode.Active) //主动模式需要发出心跳请求，并进入等待状态
                    {
#if DEBUG
                        _logger.Trace(string.Format("{0}向{1}发出心跳请求.", Heartbeat.LocalHeartDescription, session.Id));
#endif
                        session.SetWaitForReply(true); //在PrcoessReceiveData方法里，当收到回复时会回写为false
                        ProcessHeartBeatRequestOrReply(session.Id, Heartbeat.RequestToRemote);
                    }
                    else //被动模式说明当前周期内收到了心跳请求，进入等待状态，再等一个周期
                    {
                        session.SetWaitForReply(true);
                    }
                }
                else
                {
                    todoList.Add(endpoint);
                }
            }

            foreach (var endPoint in todoList)
            {
                _logger.Info(string.Format("{0}心跳检查{1}无响应，从SessionMap中移除之。池中:{2}", Heartbeat.LocalHeartDescription, endPoint, _HeartBeartSessionMap.Count));
                RemoveHeartBeatSessionFromMap(endPoint);
                ProcessHeartBroke(endPoint); //发出心跳中断消息
            }
        }

        private void RemoveHeartBeatSessionFromMap(long endPoint)
        {
            _HeartBeartSessionMap.Remove(endPoint);
        }

        private HeartBeatSession GetHeartBeatSessionFromMap(long endPoint)
        {
            HeartBeatSession result;
            _HeartBeartSessionMap.TryGetValue(endPoint, out result);
            return result;
        }

        private void StartBeatingTimer()
        {
            if (HeartBeatMode != HeartBeatMode.Responsive)
            {
                if (_BeatingTimer == null)
                {
                    _BeatingTimer = new Timer();
                    _BeatingTimer.Elapsed += BeatingTimerElapsed;
                }

                if (!_IsTimerStarted)
                {
                    _IsTimerStarted = true;
                    _BeatingTimer.Interval = Interval;
                    _BeatingTimer.Start();
                    _logger.Info(string.Format("{0}心跳启动。间隔:{1}", Heartbeat.LocalHeartDescription, Interval));
                }
            }
        }

        private void StopBeatingTimer()
        {
            if (HeartBeatMode != HeartBeatMode.Responsive)
            {
                _BeatingTimer.Stop();
                _IsTimerStarted = false;
            }
        }

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            _OnReceiveProcessing = true;

            var data = session.Data;
            var heartSession = GetHeartBeatSessionFromMap(session.Id);

            if (heartSession == null)
            {
                //session字典中不存在
                _logger.Warn(string.Format("{0}检查Session：{1}在心跳字典中不存在，退出", Heartbeat.LocalHeartDescription, session.Id));
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            if (!EnableStrictMode)
            {
                //非严格模式，收到任何数据，均认为心跳正常
                heartSession.SetWaitForReply(false);
#if DEBUG
                _logger.Trace($"{Heartbeat.LocalHeartDescription}收到{session.Id}信息,关闭心跳等待（非严格模式）.");
#endif
            }

            if (Compare(ref data, Heartbeat.RequestFromRemote)) //判断是否收到心跳请求
            {
                //被动模式下，收到了心跳请求，则标记WaitingForReply = false
                //主动模式下，收到心跳请求，则只是回复心跳，是否标记WaitingForReply = false取决于是否严格模式
                if (HeartBeatMode == HeartBeatMode.Passive) //被动模式
                {
                    heartSession.SetWaitForReply(false);
                }
                ProcessHeartBeatRequestOrReply(session.Id, Heartbeat.ReplyToRemote);
#if DEBUG
                _logger.Trace($"{Heartbeat.LocalHeartDescription}收到{session.Id}心跳请求.回复完成.");
#endif
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            if (Compare(ref data, Heartbeat.ReplyFromRemote)) //判断是否收到心跳应答
            {
                //主动模式下，收到了心跳应答，则标记WaitingForReply = false
                //被动模式下，收到了心跳应答（按理不会收到，因为被动模式根本不会发出心跳请求），是否标记WaitingForReply = false取决于是否严格模式
                if (HeartBeatMode == HeartBeatMode.Active) //主动模式
                {
                    heartSession.SetWaitForReply(false);
                }
#if DEBUG
                _logger.Trace($"{Heartbeat.LocalHeartDescription}收到{session.Id}心跳回复.");
#endif
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            _OnReceiveProcessing = false;
            _ReceiveProcessingResetEvent.Set();
            //收到的内容既不是心跳请求也不是心跳应答，则是否标记WaitingForReply = false取决于是否严格模式，处理交给后续的filter
            return true;
        }

        public override void ProcessSessionBroken(long id)
        {
            if (_HeartBeartSessionMap.ContainsKey(id))
            {
                _HeartBeartSessionMap.Remove(id);
            }

            if (_HeartBeartSessionMap.Count == 0)
            {
                //停止心跳timer
                StopBeatingTimer();
            }
        }

        public override void ProcessSessionBuilt(long id)
        {
            if (!_HeartBeartSessionMap.ContainsKey(id))
            {
                var session = new HeartBeatSession
                {
                    Id = id
                };
                session.SetWaitForReply(HeartBeatMode == HeartBeatMode.Passive);

                //被动模式则WaitingForReply=true,从session建立起，就开始等着收到心跳请求了，主动模式不用
                _HeartBeartSessionMap.Add(id, session);
            }
            //第一次监听到Session建立时启动
            StartBeatingTimer();
        }

        /// <summary>
        ///     心跳中断处理
        /// </summary>
        /// <returns></returns>
        public void ProcessHeartBroke(long id)
        {
            //发出杀死Session的指令，session杀死后，
            //filter会收到SessionBroken消息，收到消息后将对应的session移出map
            OnKillSession(this, new SessionEventArgs(_HeartBeartSessionMap[id]));
        }

        public void ProcessHeartBeatRequestOrReply(long sessionId, byte[] requestOrReply)
        {
            OnSendToSession(this, new SessionEventArgs(new TunnelSession
            {
                Id = sessionId,
                Data = requestOrReply
            }));
        }

        /// <summary>
        ///     比较收到的数据中是否有待比较的数据(一般是心跳数据)。如果收到的数据中不光是心跳协议时（粘包时）,会将心跳协议进行剔除。
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="toCompare">待比较的数据(一般是心跳数据)</param>
        /// <returns>当True时,收到的数据中有待比较的数据,反之Flase</returns>
        protected bool Compare(ref byte[] data, byte[] toCompare)
        {
            var srcLength = data.Length;
            var index = data.Find(toCompare);
            if (index < 0)
                return false;
            if (toCompare.Length < data.Length) //当源数据中包含待比较数据以外的数据时，将待比较数据移除
            {
                var tmpData = data.ToArray();
                data = new byte[data.Length - toCompare.Length];
                Buffer.BlockCopy(tmpData, 0, data, 0, index);
                Buffer.BlockCopy(tmpData, index + toCompare.Length, data, index, srcLength - index - toCompare.Length);
            }
            return true;
        }

        internal class HeartBeatSession : TunnelSession
        {
            /// <summary>
            ///     心跳时等待回复
            /// </summary>
            private bool _WaitingForReply;

            public bool GetWaitForReply()
            {
                lock (this)
                {
                    return _WaitingForReply;
                }
            }

            public void SetWaitForReply(bool value)
            {
                _logger.Trace(string.Format("SetWaitForReply = {0}", value));
                lock (this)
                {
                    _WaitingForReply = value;
                }
            }

            protected bool Equals(HeartBeatSession other)
            {
                return Id == other.Id && _WaitingForReply.Equals(other._WaitingForReply);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Id.GetHashCode();
                    hashCode = (hashCode*397);
                    return hashCode;
                }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((HeartBeatSession) obj);
            }
        }
    }

    public enum HeartBeatMode
    {
        Active, //主动模式：主动发出心跳请求，如果心跳间隔内未收到应答，则判断心跳中断，移除连接
        Passive, //被动模式：不主动发出心跳请求，接收到心跳请求后，进行心跳应答，如果心跳间隔内未收到请求，则判断心跳中断，移除连接
        Responsive //应答模式：不主动发出心跳请求，任何时候接收到心跳请求后，进行心跳应答
    }
}