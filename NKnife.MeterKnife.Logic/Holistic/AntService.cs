using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.Jobs;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Handlers;
using NKnife.MeterKnife.Util.Protocol.Generic;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Filters;
using NKnife.MeterKnife.Util.Tunnel.Generic;
using NLog;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Holistic
{
    public sealed class AntService : IAntService
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly ITunnel _tunnel;
        private readonly BytesProtocolFilter _filter;

        private readonly Dictionary<Slot, IDataConnector> _connMap = new Dictionary<Slot, IDataConnector>(1);
        private readonly Dictionary<string, JobManager> _jobMap = new Dictionary<string, JobManager>();

        public AntService(ITunnel tunnel, BytesCodec codec, BytesProtocolFamily family, BytesProtocolFilter filter,
            DUTProtocolHandler dutHandler, CareTemperatureHandler tempHandler, CareConfigHandler configHandler)
        {
            _tunnel = tunnel;
            _filter = filter;
            _filter.Bind(codec, family);
            _filter.AddHandlers(dutHandler, tempHandler, configHandler);
        }

        #region Implementation of IAntService

        /// <summary>
        ///     绑定一个指定插槽的通讯服务
        /// </summary>
        /// <param name="slots">指定的插槽与该插槽的连接器</param>
        public void Bind(params (Slot, IDataConnector)[] slots)
        {
            foreach (var tuple in slots)
            {
                var slot = tuple.Item1;
                var connector = tuple.Item2;

                if (!_connMap.ContainsKey(slot))
                {
                    _connMap.Add(slot, connector);
                    switch (slot.SlotType)
                    {
                        case SlotType.Tcpip:
                            break;
                        case SlotType.Serial:
                        {
                            _tunnel.AddFilters(_filter);
                            _tunnel.BindDataConnector(connector); //dataConnector是数据流动的动力
                            if (connector is ISerialConnector c)
                            {
                                var portInfo = slot.GetSerialPortInfo();
                                c.SerialConfig = portInfo.Item2;
                                c.PortNumber = portInfo.Item1; //串口
                            }

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     移除指定的插槽
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        public void UnBind(Slot slot)
        {
            if (_connMap.ContainsKey(slot))
            {
                _connMap.Remove(slot);
            }
        }

        /// <summary>
        ///     启动指定的工程
        /// </summary>
        /// <param name="engineering">指定的工程</param>
        /// <returns>启动是否成功</returns>
        public Task<bool> StartAsync(Engineering engineering)
        {
            if (!_jobMap.TryGetValue(engineering.Id, out var jobManager))
            {
                jobManager = new JobManager {Pool = new ScpiCommandPool {IsOverall = true}};
                foreach (var pool in engineering.CommandPools)
                {
                    foreach (var command in pool)
                    {
                        if (command.Slot == null || !_connMap.ContainsKey(command.Slot))
                        {
                            _Logger.Warn($"未Binding的Slot与对应的IDataConnector,跳过{command}\r\n{command.Slot}");
                            continue;
                        }

                        var connector = _connMap[command.Slot];
                        command.Run += job =>
                        {
                            SendCommand(connector, command);
                            return true;
                        };
                        jobManager.Pool.Add(command);
                        connector.Start();
                    }
                }

                _jobMap.Add(engineering.Id, jobManager);
            }

            Task.Factory.StartNew(() => jobManager.Run());
            return Task.Factory.StartNew(() => true);
        }

        /// <summary>
        ///     启动指定的工程
        /// </summary>
        /// <param name="engineering">指定的工程</param>
        /// <returns>启动是否成功</returns>
        public bool Pause(Engineering engineering)
        {
            if (_jobMap.ContainsKey(engineering.Id))
                _jobMap[engineering.Id].Pause();
            return true;
        }

        /// <summary>
        ///     停止指定的工程
        /// </summary>
        /// <param name="engineering">指定的工程</param>
        /// <returns>停止是否成功</returns>
        public bool Stop(Engineering engineering)
        {
            if (_jobMap.ContainsKey(engineering.Id))
                _jobMap[engineering.Id].Break();
            return true;
        }

        #endregion

        private static void SendCommand(IDataConnector connector, ScpiCommand cmd)
        {
            if (cmd == null)
            {
                _Logger.Warn("即将发送的指令为空。");
                return;
            }
            byte[] data;
            try
            {
                if (cmd.Tag != null && cmd.Tag is CareCommand careCommand)
                {
                    data = careCommand.GenerateCareProtocol();
                }
                else
                {
                    if (cmd.Scpi == null)
                    {
                        _Logger.Warn("即将发送的指令未设置SCPI。");
                        return;
                    }
                    data = cmd.Scpi.GenerateCareProtocol(cmd.GpibAddress);
                }
            }
            catch (Exception e)
            {
                _Logger.Warn($"组装发送指令(SendCommand)时出现异常:{e.Message}");
                return;
            }

            _Logger.Trace($"<- {data.ToHexString()}");
            try
            {
                if (data?.Length != 0)
                {
                    connector.SendAll(data, cmd.DUT.Id, cmd.Interval);
                }
            }
            catch (Exception e)
            {
                _Logger.Warn($"向接驳器发送指令(SendCommand)时出现异常:{e.Message}");
            }
        }
    }

    /*
    /// <summary>
    ///     软件整体的硬件插槽的管理服务器
    /// </summary>
    public sealed class AntService1 : ISlotService
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<Slot, SlotProcessor> _processorMap = new Dictionary<Slot, SlotProcessor>(1);

        private readonly IKernel _global;
        private readonly ITunnel _tunnel;

        public AntService1(IKernel global, ITunnel tunnel)
        {
            _global = global;
            _tunnel = tunnel;
        }

        #region Implementation of ISlotService

        /// <summary>
        ///     绑定一个指定插槽的通讯服务（在本软件里仅考虑多台Care的情况）
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="processor">指定的插槽连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        public void Bind(Slot slot, SlotProcessor processor, params CareProtocolHandler[] handlers)
        {
            _processorMap.Add(slot, processor);
            switch (slot.SlotType)
            {
                case SlotType.Tcpip:
                    break;
                case SlotType.Serial:
                default:
                {
                    _tunnel.AddFilters(processor.Filter);
                    _tunnel.BindDataConnector(processor.Connector); //dataConnector是数据流动的动力
                    if (processor.Connector is ISerialConnector c)
                    {
                        var portInfo = slot.GetSerialPortInfo();
                        c.SerialConfig = new SerialConfig
                        {
                            BaudRate = portInfo[1],
                            ReadBufferSize = 258,
                            ReadTimeout = 100 * 10
                        };
                        c.PortNumber = portInfo[0]; //串口
                    }
                    break;
                }
            }

            foreach (var handler in handlers)
                processor.Filter.AddHandlers(handler);
        }

        /// <summary>
        ///     移除指定插槽的一个处理器
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="handler">处理器</param>
        public void Remove(Slot slot, CareProtocolHandler handler)
        {
            var processor = _processorMap[slot];
            processor.Filter.RemoveHandler(handler);
        }

        /// <summary>
        ///     销毁服务
        /// </summary>
        public void Destroy()
        {
            foreach (var slotProcessor in _processorMap)
            {
                slotProcessor.Value.JobManager.Break();
                slotProcessor.Value.Connector.Stop();
            }

            _processorMap.Clear();
        }

        /// <summary>
        ///     启动指定插槽的服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>启动是否成功</returns>
        public bool StartAsync(Slot slot)
        {
            var processor = _processorMap[slot];
            processor.Connector.StartAsync();
            processor.JobManager.Run();
            return true;
        }

        /// <summary>
        ///     停止指定插槽的服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>停止是否成功</returns>
        public bool Stop(Slot slot)
        {
            var processor = _processorMap[slot];
            processor.JobManager.Break();
            return true;
        }

        /// <summary>
        ///     向指定插槽发送Scpi命令组
        /// </summary>
        /// <param name="slot">指定插槽</param>
        /// <param name="cmdArray">即将发送的命令组</param>
        public void SendCommands(Slot slot, params CareCommand[] cmdArray)
        {
            var processor = _processorMap[slot];
            foreach (var cmd in cmdArray)
            {
                cmd.Run += job =>
                {
                    SendCommand(processor.Connector, cmd);
                    return true;
                };
            }
            processor.JobManager.Pool.AddRange(cmdArray);
        }

        private static void SendCommand(IDataConnector dataConnector, CareCommand cmd)
        {
            try
            {
                var data = cmd.IsCare
                    ? CareCommandHelper.GenerateCareProtocol(cmd)
                    : cmd.SCPI.GenerateCareProtocol(cmd.GpibAddress);

                _Logger.Trace($"< SendCommand:{data.ToHexString()}");

                if (data.Length != 0) dataConnector.SendAll(data);
            }
            catch (Exception e)
            {
                _Logger.Warn($"向采集器发送指令(SendCommand)时出现异常:{e.Message}");
            }
        }

        #endregion

        /*
        private const string FAMILY_NAME = "care";
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly IKernel _global;
        private readonly ITunnel _tunnel;
        private readonly BytesCodec _codec;
        private readonly BytesProtocolFamily _family;

        private readonly Dictionary<Slot, SlotProcessor> _boxMap = new Dictionary<Slot, SlotProcessor>(1);

        public AntServiceOld(IKernel global, ITunnel tunnel, BytesCodec codec, BytesProtocolFamily family)
        {
            _global = global;
            _codec = codec;
            _family = family;
            _tunnel = tunnel;
        }

        #region ISlotService

        /// <summary>
        ///     绑定一个指定端口的通讯服务
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="connector">端口连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        void ISlotService.Bind(Slot slot, IDataConnector connector, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(slot, connector, handlers.Cast<CareProtocolHandler>().ToArray());
        }

        /// <summary>
        ///     绑定一个指定端口的通讯服务。可以多次绑定。
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="connector">端口连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        private void Bind(Slot slot, IDataConnector connector, params CareProtocolHandler[] handlers)
        {
            switch (slot.SlotType)
            {
                case SlotType.Tcpip:
                {
                    //TODO: socket 暂时未移植
//                        BuildConnector(slot, new SocketBytesProtocolFilter());
//                        var connector = _connectors[slot] as ISocketClient;
//                        if (connector != null)
//                        {
//                            connector.Config = new SocketClientConfig();
//                            var ip = slot.GetIpEndPoint();
//                            connector.Configure(ip.Address, ip.Port);
//                            StartAsync(slot);
//                        }
//
                    break;
                }
                case SlotType.Serial:
                default:
                {
                    BuildConnector(slot, connector, new SerialProtocolFilter());
                    if (connector is ISerialConnector con)
                    {
                        var portInfo = slot.GetSerialPortInfo();
                        con.SerialConfig = new SerialConfig
                        {
                            BaudRate = portInfo[1],
                            ReadBufferSize = 258,
                            ReadTimeout = 100 * 10
                        };
                        con.PortNumber = portInfo[0]; //串口
                    }

                    break;
                }
            }

            foreach (var handler in handlers) 
                _boxMap[slot].Filter.AddHandlers(handler);
        }

        private void BuildConnector(Slot slot, IDataConnector dataConnector, BytesProtocolFilter filter)
        {
            if (_boxMap.ContainsKey(slot))
                return;
            var box = new SlotProcessor
            {
                Filter = filter, //增加协议过滤器
                Connector = dataConnector, //建立针对端口的指令队列
                Queue = new ScpiCommandQueue(), //队列循环监听
                QueueListenState = true
            };
            _global.Acquired += (s, e) =>
            {
                if (!e.IsCollected && box.LoopQueueMap.ContainsKey(e.Slot))
                    box.LoopQueueMap.Remove(e.Slot, e.ScpiGroupKey);
            };
            _boxMap.Add(slot, box);
            _codec.CodecName = FAMILY_NAME;
            _family.FamilyName = FAMILY_NAME;

            filter.Bind(_codec, _family);
            _tunnel.AddFilters(filter);
            _tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力

            StartQueueTask(slot, dataConnector, _boxMap[slot].Queue);
        }

        /// <summary>
        ///     向指定端口发送Scpi命令组
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <param name="cmdArray">即将发送的命令组</param>
        public void SendCommands(Slot slot, params ScpiCommandQueue.Item[] cmdArray)
        {
            if (UtilCollection.IsNullOrEmpty(cmdArray))
                return;
            Task.Factory.StartNew(() => EnqueueCommand(slot, cmdArray));
        }

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <param name="cmdArrayKey">命令组的Key</param>
        /// <param name="cmdArray">即将发送的命令组</param>
        public void SendLoopCommands(Slot slot, string cmdArrayKey, params ScpiCommandQueue.Item[] cmdArray)
        {
            if (UtilCollection.IsNullOrEmpty(cmdArray))
                return;
            Task.Factory.StartNew(() =>
            {
                _boxMap[slot].LoopQueueMap.Add(slot, cmdArrayKey, cmdArray);
            });
        }

        private void EnqueueCommand(Slot slot, params ScpiCommandQueue.Item[] cmdItems)
        {
            foreach (var careItem in cmdItems)
            {
                _boxMap[slot].Queue.Enqueue(careItem);
            }
        }

        private void StartQueueTask(Slot slot, IDataConnector dataConnector, ScpiCommandQueue queue)
        {
            void Function()
            {
                var slotBox = _boxMap[slot];
                _Logger.Info($"SlotProcessor-LoopQueue 监听启动...");
                while (slotBox.QueueListenState)
                {
                    if (queue.Count <= 0)
                    {
                        //当队列中无指令时，监测是否有循环指令等待发送
                        while (slotBox.LoopQueueMap.HasCommand(slot))
                        {
                            var keys = slotBox.LoopQueueMap.GetScpiGroupKeys(slot);
                            try
                            {
                                foreach (var key in keys)
                                {
                                    //一个端口可能有多个指令组，一般是多台仪器（每仪器有一个GPIB地址）
                                    //每仪器对应一个指令组
                                    //一个指令组下的多条指令，指令的延迟在SendCommand函数中发生
                                    if (slotBox.LoopQueueMap.ContainsKey(slot, key))
                                    {
                                        var items = slotBox.LoopQueueMap[slot, key];
                                        foreach (var queueItem in items)
                                            SendCommand(dataConnector, queueItem);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                _Logger.Warn($"集合循环停止:{e.Message}");
                                break;
                            }

                            //当WaitOne时进入了循环，虽然Queue里有了数据，但信号未接收到，需判断
                            if (queue.Count > 0)
                                break;
                        }

                        _Logger.Trace("1------");
                        queue.AddEvent.WaitOne();
                        _Logger.Trace("2------");
                    }

                    if (!queue.TryDequeue(out var scpiCmd))
                        continue;
                    if (scpiCmd == null || scpiCmd.GpibAddress < 0)
                        continue;
                    SendCommand(dataConnector, scpiCmd);
                }

                _Logger.Debug($"退出{slot}命令队列循环");
            }

            Task.Factory.StartNew(Function);
        }

        private static void SendCommand(IDataConnector dataConnector, ScpiCommandQueue.Item cmd)
        {
            try
            {
                var data = cmd.IsCare
                    ? CareCommandHelper.GenerateCareProtocol(cmd)
                    : cmd.ScpiCommand.GenerateCareProtocol(cmd.GpibAddress);

                _Logger.Trace($"SendCommand:{data.ToHexString()}");

                if (data.Length != 0)
                {
                    dataConnector.SendAll(data);
                    if (cmd.IsCare)
                        Thread.Sleep(cmd.Interval);
                    else
                        Thread.Sleep((int) cmd.ScpiCommand.Interval);
                }
            }
            catch (Exception e)
            {
                _Logger.Warn($"向采集器发送指令(SendCommand)时出现异常:{e.Message}");
            }
        }

        #region Remove,Destroy,StartAsync,Stop

        /// <summary>
        ///     启动指定端口的串口服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>启动是否成功</returns>
        public bool StartAsync(Slot slot)
        {
            if (!_boxMap.ContainsKey(slot))
            {
                _Logger.Warn($"硬件插槽{slot}未准备好...");
                return false;
            }

            var dataConnector = _boxMap[slot].Connector;
            try
            {
                dataConnector.StartAsync();
                _Logger.Info("Tunnel服务启动成功");
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error($"Tunnel服务失败:{e}");
                return false;
            }
        }

        /// <summary>
        ///     停止批定端口的串口服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>停止是否成功</returns>
        public bool Stop(Slot slot)
        {
            _boxMap[slot].QueueListenState = false;
            Thread.Sleep(200);
            _boxMap[slot].Connector.Stop();
            _Logger.Info("Tunnel服务停止成功");
            return true;
        }

        /// <summary>
        ///     销毁服务中的所有对象
        /// </summary>
        public void Destroy()
        {
            foreach (var slot in _boxMap.Keys)
            {
                var part = _boxMap[slot];
                part.Connector.Stop();
            }

            _boxMap.Clear();
        }

        /// <summary>
        ///     移除指定插槽的一个处理器
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        /// <param name="handler">处理器</param>
        public void Remove(Slot slot, BaseProtocolHandler<byte[]> handler)
        {
            _boxMap[slot].Filter.RemoveHandler(handler);
        }

        #endregion

        #endregion

        private class SlotProcessor
        {
            public JobManager JobManager { get; set; }
            public bool QueueListenState { get; set; }
            public IDataConnector Connector { get; set; }
            public BytesProtocolFilter Filter { get; set; } = new SerialProtocolFilter();
            public ScpiCommandQueue Queue { get; set; }
            public SlotCommandMap LoopQueueMap { get; set; } = new SlotCommandMap();
        }
        */
    /*}
    */

}