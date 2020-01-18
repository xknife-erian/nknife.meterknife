using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Scpi;
using MeterKnife.Util.Serial;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Generic.Filters;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Filters;
using MeterKnife.Util.Tunnel.Generic;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.CareOne;
using NKnife.Util;

namespace NKnife.MeterKnife.Logic.Services
{
    public class CareCommService : BaseAntCommService
    {
        private const string FAMILY_NAME = "careone";
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly List<Slot> _carePortList = new List<Slot>();

        private readonly BytesCodec _codec;
        private readonly Dictionary<Slot, IDataConnector> _connectors = new Dictionary<Slot, IDataConnector>();
        private readonly IDataConnector _dataConnector; // = DI.Get<IDataConnector>(slot.TunnelType.ToString());
        private readonly BytesProtocolFamily _family;

        private readonly Dictionary<Slot, BytesProtocolFilter> _filters =
            new Dictionary<Slot, BytesProtocolFilter>();

        private readonly Dictionary<Slot, bool> _isTaskContinue = new Dictionary<Slot, bool>();
        private readonly IGlobal _global;
        private readonly CommPortCommandMap _loopCommandMap = new CommPortCommandMap();
        private readonly Dictionary<Slot, ScpiCommandQueue> _queues = new Dictionary<Slot, ScpiCommandQueue>();
        private readonly ITunnel _tunnel;

        public CareCommService(IGlobal global, ITunnel tunnel,
            BytesCodec codec, BytesProtocolFamily family, IDataConnector dataConnector)
        {
            _global = global;
            _tunnel = tunnel;
            _codec = codec;
            _family = family;
            _dataConnector = dataConnector;
            Initialize();
        }

        private void Initialize()
        {
            var careService = new CareFinder();
            careService.SerialFinder(this);
            _global.Collected += (s, e) =>
            {
                if (!e.IsCollected && _loopCommandMap.ContainsKey(e.CarePort)) 
                    _loopCommandMap.Remove(e.CarePort, e.ScpiGroupKey);
            };
        }


        /// <summary>
        ///     绑定一个指定端口的通讯服务
        /// </summary>
        /// <param name="slot">指定的端口</param>
        /// <param name="handlers">协议处理的handler</param>
        public override void Bind(Slot slot, params CareOneProtocolHandler[] handlers)
        {
            if (!_filters.TryGetValue(slot, out var filter))
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
                        BuildConnector(slot, new SerialProtocolFilter());
                        if (_connectors[slot] is ISerialConnector dataConnector)
                        {
                            var serialport = slot.GetSerialPortInfo();
                            dataConnector.SerialConfig = new SerialConfig
                            {
                                BaudRate = serialport[1],
                                ReadBufferSize = 258,
                                ReadTimeout = 100 * 10
                            };
                            dataConnector.PortNumber = serialport[0]; //串口
                        }

                        break;
                    }
                }

                filter = _filters[slot];
            }

            foreach (var handler in handlers)
            {
                filter?.AddHandlers(handler);
            }
        }

        protected virtual void BuildConnector(Slot slot, BytesProtocolFilter filter)
        {
            if (_carePortList.Contains(slot))
                return;
            _carePortList.Add(slot);
            var sb = new StringBuilder("PortList:");
            foreach (var port in _carePortList)
                sb.Append(port).Append(';');
            _Logger.Info(sb);

            _codec.CodecName = FAMILY_NAME;
            _family.FamilyName = FAMILY_NAME;

            filter.Bind(_codec, _family);

            _tunnel.AddFilters(filter);

            _filters.Add(slot, filter); //增加协议过滤器
            _connectors.Add(slot, _dataConnector);

            //建立针对端口的指令队列
            var queue = new ScpiCommandQueue();
            _queues.Add(slot, queue);
            _isTaskContinue.Add(slot, true); //队列循环监听
            StartQueueTask(slot, _dataConnector, queue);

            _tunnel.BindDataConnector(_dataConnector); //dataConnector是数据流动的动力
            _Logger.Info($"PortList:{_carePortList.Count},Filters:{_filters.Count},Connectors:{_connectors.Count}");
        }

        public override void SendCommands(Slot slot, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() => EnqueueCommand(slot, careItems));
        }

        public override void SendLoopCommands(Slot slot, string commandArrayKey, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() =>
            {
                _loopCommandMap.Add(slot, commandArrayKey, careItems);
                EnqueueCommand(slot, ScpiCommandQueue.Item.NullCommand());
            });
        }

        private void EnqueueCommand(Slot slot, params ScpiCommandQueue.Item[] careItems)
        {
            foreach (var careItem in careItems) _queues[slot].Enqueue(careItem);
        }

        protected virtual void StartQueueTask(Slot slot, IDataConnector dataConnector, ScpiCommandQueue queue)
        {
            Task.Factory.StartNew(() =>
            {
                while (_isTaskContinue[slot])
                {
                    if (queue.Count <= 0)
                    {
                        //当队列中无指令时，监测是否有循环指令等待发送
                        while (_loopCommandMap.HasCommand(slot))
                        {
                            var keys = _loopCommandMap.GetScpiGroupKeys(slot);
                            try
                            {
                                foreach (var key in keys)
                                {
                                    //一个端口可能有多个指令组，一般是多台仪器（每仪器有一个GPIB地址）
                                    //每仪器对应一个指令组
                                    //一个指令组下的多条指令，指令的延迟在SendCommand函数中发生
                                    if (_loopCommandMap.ContainsKey(slot, key))
                                    {
                                        var items = _loopCommandMap[slot, key];
                                        foreach (var careItem in items) 
                                            SendCommand(dataConnector, careItem);
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

                        queue.AddEvent.WaitOne();
                    }

                    if (!queue.TryDequeue(out var cmd))
                        continue;
                    if (cmd == null || cmd.GpibAddress < 0)
                        continue;
                    SendCommand(dataConnector, cmd);
                }

                _Logger.Debug($"退出{slot}命令队列循环");
            });
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

        public override void Remove(Slot tunnelPort, CareOneProtocolHandler handler)
        {
            if (_filters.TryGetValue(tunnelPort, out var filter))
                filter.RemoveHandler(handler);
        }

        /// <summary>
        ///     销毁服务中的所有对象
        /// </summary>
        public override void Destroy()
        {
            foreach (var port in _carePortList)
            {
                if (_connectors.TryGetValue(port, out var connector))
                {
                    connector.Stop();
                    _connectors.Remove(port);
                    _filters.Remove(port);
                    _queues.Remove(port);
                }
            }

            _carePortList.Clear();
        }

        public override bool Start(Slot slot)
        {
            if (_connectors.TryGetValue(slot, out var dataConnector))
                try
                {
                    dataConnector.Start();
                    _Logger.Info("Tunnel服务启动成功");
                    return true;
                }
                catch (Exception e)
                {
                    _Logger.Error(message: $"Tunnel服务失败:{e}");
                    return false;
                }

            return false;
        }

        public override bool Stop(Slot slot)
        {
            _isTaskContinue[slot] = false;
            Thread.Sleep(200);
            if (_connectors.TryGetValue(slot, out var connector))
            {
                connector.Stop();
                _Logger.Info("Tunnel服务停止成功");
            }

            return true;
        }

        #endregion
    }
}