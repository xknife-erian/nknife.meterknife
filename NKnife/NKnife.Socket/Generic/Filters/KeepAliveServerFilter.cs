using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Generic.Filters
{
    public class KeepAliveServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true; 

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected override void OnBoundGetter()
        {
            if (SessionMapGetter != null)
            {
                KnifeSocketSessionMap map = SessionMapGetter.Invoke();
                map.Removed += SessionMap_OnRemoved;
            }
        }

        private void SessionMap_OnRemoved(object sender, EventArgs<EndPoint> e)
        {
            ClearByEndPoint(e.Item);
        }

        protected internal override void OnClientBroken(ConnectionBrokenEventArgs e)
        {
            base.OnClientBroken(e);
            ClearByEndPoint(e.EndPoint);
        }

        private void ClearByEndPoint(EndPoint endPoint)
        {
            DataMonitor dataMonitor;
            if (_DataMonitors.TryRemove(endPoint, out dataMonitor))
            {
                dataMonitor.IsMonitor = false;
                dataMonitor.ReceiveQueue.AutoResetEvent.Set();
                _logger.Trace(string.Format("客户端:{0}的数据监听器池循环开关被关闭并移除。{1}", endPoint, _DataMonitors.Count));
            }
            ReceiveQueue receiveQueue;
            if (_ReceiveQueueMap.TryRemove(endPoint, out receiveQueue))
            {
                receiveQueue.Clear();
                _logger.Trace(string.Format("客户端:{0}从ReceiveQueue池中被移除。{1}", endPoint, _ReceiveQueueMap.Count));
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, ref byte[] data)
        {
            EndPoint endPoint = session.Source;
            ReceiveQueue receive = null;
            if (!_ReceiveQueueMap.TryGetValue(endPoint, out receive))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                receive = new ReceiveQueue();
                _ReceiveQueueMap.TryAdd(endPoint, receive);
                InitializeDataMonitor(new KeyValuePair<EndPoint, ReceiveQueue>(endPoint, receive));
            }
            receive.Enqueue(data);
        }

        #region 数据处理

        private readonly ConcurrentDictionary<EndPoint, DataMonitor> _DataMonitors =
            new ConcurrentDictionary<EndPoint, DataMonitor>();

        private readonly ConcurrentDictionary<EndPoint, ReceiveQueue> _ReceiveQueueMap =
            new ConcurrentDictionary<EndPoint, ReceiveQueue>();

        protected virtual void InitializeDataMonitor(KeyValuePair<EndPoint, ReceiveQueue> pair)
        {
            var task = new Task(ReceiveQueueMonitor, pair);
            var dm = new DataMonitor { IsMonitor = true, ReceiveQueue = pair.Value, Task = task };
            _DataMonitors.TryAdd(pair.Key, dm);
            task.Start();
        }

        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var pair = (KeyValuePair<EndPoint, ReceiveQueue>) obj;
            _logger.Debug(string.Format("启动基于{0}的ReceiveQueue队列的监听。", pair.Key));
            ReceiveQueue receiveQueue = pair.Value;

            var unFinished = new byte[] {};
            DataMonitor dataMonitor;
            EndPoint endPoint = pair.Key;
            if (_DataMonitors.TryGetValue(endPoint, out dataMonitor))
            {
                while (dataMonitor.IsMonitor)
                {
                    if (receiveQueue.Count > 0)
                    {
                        byte[] data = receiveQueue.Dequeue();
                        unFinished = ProcessDataPacket(data, unFinished, DataDecoder, endPoint);
                    }
                    else
                    {
                        receiveQueue.AutoResetEvent.WaitOne();
                    }
                }
            }
            // 当接收队列停止监听时，移除该客户端数据队列
            bool isRemoved = _DataMonitors.TryRemove(endPoint, out dataMonitor);
            if (isRemoved)
                _logger.Trace(string.Format("监听循环结束，从数据队列池中移除该客户端{0}成功，{1}", endPoint, _DataMonitors.Count));
        }

        protected virtual int DataDecoder(EndPoint endpoint, byte[] data)
        {
            int done;
            StringProtocolFamily family = _FamilyGetter.Invoke();
            KnifeStringCodec codec = _CodecGetter.Invoke();
            string[] datagram = codec.StringDecoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _logger.Debug("协议消息无内容。");
                return done;
            }
            IEnumerable<StringProtocol> protocols = ProtocolParse(family, datagram);
            foreach (StringProtocol protocol in protocols)
            {
                // 触发数据基础解析后发生的数据到达事件
                HandlerInvoke(endpoint, protocol);
            }
            return done;
        }

        protected virtual IEnumerable<StringProtocol> ProtocolParse(StringProtocolFamily family, string[] datagram)
        {
            var protocols = new List<StringProtocol>(datagram.Length);
            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg)) continue;
                string command = "";
                try
                {
                    command = _FamilyGetter.Invoke().CommandParser.GetCommand(dg);
                }
                catch (Exception e)
                {
                    _logger.Error(string.Format("命令字解析异常:{0},Data:{1}", e.Message, dg), e);
                    continue;
                }
                _logger.Trace(string.Format("Server.OnDataComeIn::命令字:{0},数据包:{1}", command, dg));

                StringProtocol protocol;
                try
                {
                    protocol = family.Parse(command,dg);
                }
                catch (ArgumentNullException ex)
                {
                    _logger.Warn(string.Format("协议分装异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("协议分装异常。内容:{0};命令字:{1}。{2}", dg, command, ex.Message), ex);
                    continue;
                }
                protocols.Add(protocol);
            }
            return protocols;
        }

        /// <summary>
        ///     // 触发数据基础解析后发生的数据到达事件
        /// </summary>
        protected virtual void HandlerInvoke(EndPoint endpoint, StringProtocol protocol)
        {
            IList<KnifeSocketProtocolHandler> handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap sessionMap = SessionMapGetter.Invoke();
            KnifeSocketSession session;
            if (!sessionMap.TryGetValue(endpoint, out session))
            {
                _logger.Warn(string.Format("SessionMap中未找到指定的客户端:{0}", endpoint));
            }
            try
            {
                if (handlers == null || handlers.Count == 0)
                {
                    Debug.Fail(string.Format("Handler集合不应为空."));
                    return;
                }
                if (handlers.Count == 1)
                {
                    handlers[0].Recevied(session, protocol);
                }
                else
                {
                    foreach (KnifeSocketProtocolHandler handler in handlers)
                    {
                        if (handler.Commands.Contains(protocol.Command))
                            handler.Recevied(session, protocol);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("handler调用异常:{0}", e.Message), e);
            }
        }

        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Task Task { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }

        #endregion
    }
}