using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.CareOne;
using NKnife.MeterKnife.Util.Protocol.Generic;
using NKnife.MeterKnife.Util.Scpi;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Util.Serial.Generic.Filters;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Base;
using NKnife.MeterKnife.Util.Tunnel.Filters;
using NKnife.MeterKnife.Util.Tunnel.Generic;
using NKnife.Util;
using NLog;

namespace NKnife.MeterKnife.Logic.Services
{
    /// <summary>
    ///     软件整体的硬件插槽的管理服务器
    /// </summary>
    public sealed class AntService : ISlotService
    {
        private const string FAMILY_NAME = "care";
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly IGlobal _global;
        private readonly ITunnel _tunnel;
        private readonly BytesCodec _codec;
        private readonly BytesProtocolFamily _family;

        private readonly Dictionary<Slot, SlotBox> _boxMap = new Dictionary<Slot, SlotBox>(1);

        public AntService(IGlobal global, ITunnel tunnel, BytesCodec codec, BytesProtocolFamily family)
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
        /// <param name="dataConnector">端口连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        void ISlotService.Bind(Slot slot, IDataConnector dataConnector, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Bind(slot, dataConnector, handlers.Cast<CareProtocolHandler>().ToArray());
        }

        /// <summary>
        ///     绑定一个指定端口的通讯服务。可以多次绑定。
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="dataConnector">端口连接器</param>
        /// <param name="handlers">协议处理的handler</param>
        private void Bind(Slot slot, IDataConnector dataConnector, params CareProtocolHandler[] handlers)
        {
            switch (slot.TunnelType)
            {
                case TunnelType.Tcpip:
                {
                    //TODO: socket 暂时未移植
//                        BuildConnector(slot, new SocketBytesProtocolFilter());
//                        var dataConnector = _connectors[slot] as ISocketClient;
//                        if (dataConnector != null)
//                        {
//                            dataConnector.Config = new SocketClientConfig();
//                            var ip = slot.GetIpEndPoint();
//                            dataConnector.Configure(ip.Address, ip.Port);
//                            Start(slot);
//                        }
//
                    break;
                }
                case TunnelType.Serial:
                default:
                {
                    BuildConnector(slot, dataConnector, new SerialProtocolFilter());
                    if (dataConnector is ISerialConnector con)
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
            var box = new SlotBox
            {
                Filter = filter, //增加协议过滤器
                Connector = dataConnector, //建立针对端口的指令队列
                Queue = new ScpiCommandQueue(), //队列循环监听
                QueueListenState = true
            };
            _global.Collected += (s, e) =>
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
        /// <param name="careItems">即将发送的命令组</param>
        public void SendCommands(Slot slot, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() => EnqueueCommand(slot, careItems));
        }

        /// <summary>
        ///     向指定端口发送将要循环使用的Scpi命令组
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <param name="commandArrayKey">命令组的Key</param>
        /// <param name="careItems">即将发送的命令组</param>
        public void SendLoopCommands(Slot slot, string commandArrayKey, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() =>
            {
                _boxMap[slot].LoopQueueMap.Add(slot, commandArrayKey, careItems);
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
                _Logger.Info($"SlotBox-LoopQueue 监听启动...");
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
                    ? CareScpiHelper.GenerateProtocol(cmd)
                    : cmd.ScpiCommand.GenerateProtocol(cmd.GpibAddress);

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

        #region Remove,Destroy,Start,Stop

        /// <summary>
        ///     启动指定端口的串口服务
        /// </summary>
        /// <param name="slot">指定端口</param>
        /// <returns>启动是否成功</returns>
        public bool Start(Slot slot)
        {
            if (!_boxMap.ContainsKey(slot))
            {
                _Logger.Warn($"硬件插槽{slot}未准备好...");
                return false;
            }

            var dataConnector = _boxMap[slot].Connector;
            try
            {
                dataConnector.Start();
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

        private class SlotBox
        {
            public bool QueueListenState { get; set; }
            public IDataConnector Connector { get; set; }
            public BytesProtocolFilter Filter { get; set; } = new SerialProtocolFilter();
            public ScpiCommandQueue Queue { get; set; }
            public SlotCommandMap LoopQueueMap { get; set; } = new SlotCommandMap();
        }
    }
}