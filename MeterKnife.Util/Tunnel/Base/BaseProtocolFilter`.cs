using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NKnife.MeterKnife.Util.Protocol;
using NKnife.MeterKnife.Util.Tunnel.Common;
using NKnife.Util;

namespace NKnife.MeterKnife.Util.Tunnel.Base
{
    public abstract class BaseProtocolFilter<T> : BaseTunnelFilter, ITunnelProtocolFilter<T>
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        protected readonly ConcurrentDictionary<long, DataMonitor> _DataMonitors = new ConcurrentDictionary<long, DataMonitor>();
        protected ITunnelCodec<T> _Codec;
        protected IProtocolFamily<T> _Family;
        protected List<ITunnelProtocolHandler<T>> _Handlers = new List<ITunnelProtocolHandler<T>>();

        /// <summary>
        /// 命令字的比较方法
        /// </summary>
        public Func<IEnumerable<T>, T, bool> CommandCompareFunc { get; set; }

        #region ITunnelProtocolFilter<T>

        public virtual void Bind(ITunnelCodec<T> codec, IProtocolFamily<T> family)
        {
            _Codec = codec;
            _Logger.Info($"{GetType().Name}绑定Codec成功。{_Codec.Decoder.GetType().Name},{_Codec.Encoder.GetType().Name}");

            _Family = family;
            _Logger.Info($"{GetType().Name}绑定协议族[{_Family.FamilyName}]成功。");
        }

        public override void ProcessSessionBroken(long id)
        {
            if (_DataMonitors.TryGetValue(id, out var monitor))
            {
                monitor.IsMonitor = false;
                monitor.ReceiveQueue.AddEvent.Set();
            }
        }

        public override void ProcessSessionBuilt(long id)
        {
            if (!_DataMonitors.TryGetValue(id, out var monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
            //什么也不做
        }

        public override void ProcessSendToAll(byte[] data)
        {
            //什么也不做
        }

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            byte[] src = session.Source;
            byte[] data = session.Data;
            long id = session.Id;
            if (!_DataMonitors.TryGetValue(id, out var monitor))
            {
                //当第一次有相应的客户端连接时，为该客户端创建相应的处理队列
                monitor = new DataMonitor();
                InitializeDataMonitor(id, monitor);
            }
            monitor.ReceiveQueue.Enqueue((session.Relation, src, data));
            return true;
        }

        #endregion

        #region 数据处理

        public virtual void AddHandlers(params ITunnelProtocolHandler<T>[] handlers)
        {
            foreach (var handler in handlers)
            {
                if (handler is BaseProtocolHandler<T> protocolHandler)
                {
                    protocolHandler.SendToSession += OnSendToSession;
                    protocolHandler.SendToAll += OnSendToAll;
                    protocolHandler.Bind(_Codec, _Family);
                    _Handlers.Add(handler);
                    _Logger.Info($"{GetType().Name}增加{handler.GetType().Name}成功.");
                }
            }
        }

        public virtual void RemoveHandler(ITunnelProtocolHandler<T> handler)
        {
            handler.SendToSession -= OnSendToSession;
            handler.SendToAll -= OnSendToAll;
            _Handlers.Remove(handler);
            _Logger.Info($"{GetType().Name}移除{handler.GetType().Name}成功.");
        }

        /// <summary>
        ///     数据包处理。主要处理较异常的情况下的，半包的接包，粘包等现象
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        public virtual IEnumerable<IProtocol<T>> ProcessDataPacket(byte[] dataPacket, ref byte[] unFinished)
        {
            IEnumerable<IProtocol<T>> protocols = null;
            if (!UtilCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = dataPacket.Length;
                dataPacket = unFinished.Concat(dataPacket).ToArray();
                _Logger.Trace($"接包操作:半包:{unFinished.Length},原始包:{srcLen},接包后:{dataPacket.Length}");
            }
            T[] datagram = _Codec.Decoder.Execute(dataPacket, out var done);
            if (UtilCollection.IsNullOrEmpty(datagram))
            {
                _Logger.Trace($"{GetType().Name}处理协议无内容。{dataPacket.Length}");
            }
            else
            {
                //_logger.Debug(string.Format("dataMonitor 处理数据 step start"));
                protocols = ParseProtocols(datagram);
                //_logger.Debug(string.Format("dataMonitor 处理数据 step stop"));
            }

            if (dataPacket.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                unFinished = new byte[dataPacket.Length - done];
                Buffer.BlockCopy(dataPacket, done, unFinished, 0, unFinished.Length);
                _Logger.Trace($"半包数据暂存,数据长度:{unFinished.Length}");
            }
            return protocols;
        }

        protected virtual IEnumerable<IProtocol<T>> ParseProtocols(T[] datagram)
        {
            var protocols = new List<IProtocol<T>>(datagram.Length);
            foreach (T dg in datagram)
            {
                //_logger.Debug(string.Format("dataMonitor 处理数据 step {0}",1));
                T command;
                try
                {
                    //_logger.Debug(string.Format("dataMonitor 处理数据 step {0}", 2));
                    command = _Family.CommandParser.GetCommand(dg);
                }
                catch (Exception e)
                {
                    _Logger.Error($"命令字解析异常:{e.Message},Data:{dg}");
                    continue;
                }

                //_logger.Debug(string.Format("dataMonitor 处理数据 step {0}", 3));

                IProtocol<T> protocol;
                try
                {
                    protocol = _Family.Parse(command, dg);
                }
                catch (ArgumentNullException ex)
                {
                    _Logger.Warn($"协议分装异常。内容:{dg};命令字:{command}。{ex.Message}");
                    continue;
                }
                catch (Exception ex)
                {
                    _Logger.Warn($"协议分装异常。{ex.Message}");
                    continue;
                }

                protocols.Add(protocol);
            }
            return protocols;
        }

        #endregion

        protected virtual void InitializeDataMonitor(long id, DataMonitor dm)
        {
            var task = new Task(ReceiveQueueMonitor, id);
            dm.IsMonitor = true;
            dm.Task = task;
            dm.ReceiveQueue = new ReceiveQueue();
            if (_DataMonitors.TryAdd(id, dm))
            {
                dm.Task.Start();
            }
        }

         private int _TempCount = 0;
        /// <summary>
        ///     核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected virtual void ReceiveQueueMonitor(object obj)
        {
            var id = (long) obj;
            _Logger.Debug($"启动[ReceiveQueue]队列基于-{id}-的的[DataMonitor]监听。");
            var unFinished = new byte[] {};

            try
            {
                if (_DataMonitors.TryGetValue(id, out var dataMonitor))
                {
                    while (dataMonitor.IsMonitor || dataMonitor.ReceiveQueue.Count > 0) //重要，dataMonitor.IsMonitor=false但dataMonitor.ReceiveQueue.Count > 0时，也要继续处理完数据再退出while
                    {
                        if (dataMonitor.ReceiveQueue.Count > 0)
                        {
                            _TempCount += 1;
                            //_Logger.Debug($"dataMonitor 处理数据{_TempCount}");
                            if(!dataMonitor.ReceiveQueue.TryDequeue(out var data))
                                continue;
                            if (UtilCollection.IsNullOrEmpty(data.Item3))
                                continue;
                            IEnumerable<IProtocol<T>> protocols = ProcessDataPacket(data.Item3, ref unFinished);
                            _Logger.Debug($"DM>> [{data.Item1}] {data.Item3.ToHexString()}");
                            if (protocols != null)
                            {
                                foreach (var protocol in protocols)
                                {
                                    // 触发数据基础解析后发生的数据到达事件, 即触发handle
                                    HandlerInvoke(data.Item1, data.Item3, protocol);
                                }
                            }
                        }
                        else
                        {
                            dataMonitor.ReceiveQueue.AddEvent.WaitOne();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.Warn($"监听循环异常结束：{ex}");
            }
            finally
            {
                // 当接收队列停止监听时，移除该客户端数据队列
                bool isRemoved = _DataMonitors.TryRemove(id, out _);
                if (isRemoved)
                {
                    _Logger.Trace($"监听循环结束，从数据队列池中移除该客户端{id}成功，{_DataMonitors.Count}");
                }
            }
        }

        /// <summary>
        ///     触发数据基础解析后发生的数据到达事件
        /// </summary>
        protected virtual void HandlerInvoke(string relation, byte[] source, IProtocol<T> protocol)
        {
            try
            {
                if (_Handlers.Count == 0)
                {
                    _Logger.Warn("Handler集合不应为空.");
                    return;
                }
                if (_Handlers.Count == 1)
                {
                    _Handlers[0].Received(relation, source, protocol);
                }
                else
                {
                    //var hs = _Handlers.ToArray();//防止正在执行循环过程中移除Handler出现的异常
                    foreach (var handler in _Handlers)
                    {
                        //handler Commands.Count为0时，接收处理所有的协议，否则，处理Commands指定的协议
                        if (handler.Commands.Count == 0 || ContainsCommand(handler.Commands, protocol.Command))
                        {
                            handler.Received(relation, source, protocol);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _Logger.Error($"handler调用异常:{e.Message}");
            }
        }

        /// <summary>
        /// 指定的命令字集合中是否包含指定的命令字
        /// </summary>
        /// <param name="list">指定的命令字集合</param>
        /// <param name="command">指定的命令字</param>
        protected abstract bool ContainsCommand(List<T> list, T command);

        protected class DataMonitor
        {
            public bool IsMonitor { get; set; }
            public Task Task { get; set; }
            public ReceiveQueue ReceiveQueue { get; set; }
        }
    }
}