using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Common.Util;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Scpi;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Generic.Filters;
using MeterKnife.Util.Serial.Interfaces;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Socket.Generic.Filters;
using MeterKnife.Util.Socket.Interfaces;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Filters;
using MeterKnife.Util.Tunnel.Generic;
using NKnife.Util;

namespace MeterKnife.Kernel.Services
{
    public class CareCommunicationService : BaseCareCommunicationService
    {
        private const string FAMILY_NAME = "careone";
        private static readonly ILog _Logger = LogManager.GetLogger<CareCommunicationService>();
        private readonly List<CommPort> _carePortList = new List<CommPort>();

        private readonly BytesCodec _codec;
        private readonly Dictionary<CommPort, IDataConnector> _connectors = new Dictionary<CommPort, IDataConnector>();
        private readonly IDataConnector _dataConnector; // = DI.Get<IDataConnector>(commPort.TunnelType.ToString());
        private readonly BytesProtocolFamily _family;

        private readonly Dictionary<CommPort, BytesProtocolFilter> _filters =
            new Dictionary<CommPort, BytesProtocolFilter>();

        private readonly Dictionary<CommPort, bool> _isTaskContinueds = new Dictionary<CommPort, bool>();
        private readonly IMeterKernel _kernel;
        private readonly CommPortCommandMap _loopCommandMap = new CommPortCommandMap();
        private readonly Dictionary<CommPort, ScpiCommandQueue> _queues = new Dictionary<CommPort, ScpiCommandQueue>();
        private readonly ITunnel _tunnel;


        public CareCommunicationService(IMeterKernel kernel, ITunnel tunnel,
            BytesCodec codec, BytesProtocolFamily family, IDataConnector dataConnector)
        {
            _kernel = kernel;
            _tunnel = tunnel;
            _codec = codec;
            _family = family;
            _dataConnector = dataConnector;
        }

        public override bool Initialize()
        {
            Cares = new List<CommPort>();
            var careService = new CareService();
            careService.SerialFinder(this);
            _kernel.Collected += (s, e) =>
            {
                if (!e.IsCollected && _loopCommandMap.ContainsKey(e.CarePort)) _loopCommandMap.Remove(e.CarePort, e.ScpiGroupKey);
            };
            IsInitialized = true;
            return true;
        }

        public override void Bind(CommPort commPort, params CareOneProtocolHandler[] handlers)
        {
            BytesProtocolFilter filter = null;
            if (!_filters.TryGetValue(commPort, out filter))
            {
                switch (commPort.TunnelType)
                {
                    case TunnelType.Tcpip:
                    {
                        BuildConnector(commPort, new SocketBytesProtocolFilter());
                        var dataConnector = _connectors[commPort] as ISocketClient;
                        if (dataConnector != null)
                        {
                            dataConnector.Config = new SocketClientConfig();
                            var ip = commPort.GetIpEndPoint();
                            dataConnector.Configure(ip.Address, ip.Port);
                            Start(commPort);
                        }

                        break;
                    }
                    case TunnelType.Serial:
                    default:
                    {
                        BuildConnector(commPort, new SerialProtocolFilter());
                        var dataConnector = _connectors[commPort] as ISerialConnector;
                        if (dataConnector != null)
                        {
                            var serialport = commPort.GetSerialPortInfo();
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

                filter = _filters[commPort];
            }

            foreach (var handler in handlers)
                if (filter != null)
                    filter.AddHandlers(handler);
        }

        protected virtual void BuildConnector(CommPort commPort, BytesProtocolFilter filter)
        {
            if (_carePortList.Contains(commPort))
                return;
            _carePortList.Add(commPort);
            var sb = new StringBuilder("PortList:");
            foreach (var port in _carePortList)
                sb.Append(port).Append(';');
            _Logger.Info(sb);

            _codec.CodecName = FAMILY_NAME;
            _family.FamilyName = FAMILY_NAME;

            filter.Bind(_codec, _family);

            _tunnel.AddFilters(filter);

            _filters.Add(commPort, filter); //增加协议过滤器
            _connectors.Add(commPort, _dataConnector);

            //建立针对端口的指令队列
            var queue = new ScpiCommandQueue();
            _queues.Add(commPort, queue);
            _isTaskContinueds.Add(commPort, true); //队列循环监听
            StartQueueTask(commPort, _dataConnector, queue);

            _tunnel.BindDataConnector(_dataConnector); //dataConnector是数据流动的动力
            _Logger.Info(string.Format("PortList:{0},Filters:{1},Connectors:{2}", _carePortList.Count, _filters.Count,
                _connectors.Count));
        }

        public override void SendCommands(CommPort commPort, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() => EnqueueCommand(commPort, careItems));
        }

        public override void SendLoopCommands(CommPort commPort, string commandArrayKey, params ScpiCommandQueue.Item[] careItems)
        {
            if (UtilCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() =>
            {
                _loopCommandMap.Add(commPort, commandArrayKey, careItems);
                EnqueueCommand(commPort, ScpiCommandQueue.Item.NullCommand());
            });
        }

        private void EnqueueCommand(CommPort commPort, params ScpiCommandQueue.Item[] careItems)
        {
            foreach (var careItem in careItems) _queues[commPort].Enqueue(careItem);
        }

        protected virtual void StartQueueTask(CommPort commPort, IDataConnector dataConnector, ScpiCommandQueue queue)
        {
            Task.Factory.StartNew(() =>
            {
                while (_isTaskContinueds[commPort])
                {
                    if (queue.Count <= 0)
                    {
                        //当队列中无指令时，监测是否有循环指令等待发送
                        while (_loopCommandMap.HasCommand(commPort))
                        {
                            var keys = _loopCommandMap.GetScpiGroupKeys(commPort);
                            try
                            {
                                foreach (var key in keys)
                                    //一个端口可能有多个指令组，一般是多台仪器（每仪器有一个GPIB地址）
                                    //每仪器对应一个指令组
                                    //一个指令组下的多条指令，指令的延迟在SendCommand函数中发生
                                    if (_loopCommandMap.ContainsKey(commPort, key))
                                    {
                                        var items = _loopCommandMap[commPort, key];
                                        foreach (var careItem in items) SendCommand(dataConnector, careItem);
                                    }
                            }
                            catch (Exception e)
                            {
                                _Logger.Warn(string.Format("集合循环停止:{0}", e.Message), e);
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

                _Logger.Debug(string.Format("退出{0}命令队列循环", commPort));
            });
        }

        protected static void SendCommand(IDataConnector dataConnector, ScpiCommandQueue.Item cmd)
        {
            try
            {
                var data = cmd.IsCare
                    ? CommandUtil.GenerateProtocol(cmd)
                    : cmd.ScpiCommand.GenerateProtocol(cmd.GpibAddress);

                _Logger.Trace(string.Format("SendCommand:{0}", data.ToHexString()));

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
                _Logger.Warn(string.Format("向采集器发送指令(SendCommand)时出现异常:{0}", e.Message), e);
            }
        }

        #region Remove,Destroy,Start,Stop

        public override void Remove(CommPort tunnelPort, CareOneProtocolHandler handler)
        {
            BytesProtocolFilter filter;
            if (_filters.TryGetValue(tunnelPort, out filter))
                filter.RemoveHandler(handler);
        }

        /// <summary>
        ///     销毁服务中的所有对象
        /// </summary>
        public override void Destroy()
        {
            foreach (var port in _carePortList)
            {
                IDataConnector connector;
                if (_connectors.TryGetValue(port, out connector))
                {
                    connector.Stop();
                    _connectors.Remove(port);
                    _filters.Remove(port);
                    _queues.Remove(port);
                }
            }

            _carePortList.Clear();
        }

        public override bool Start(CommPort commPort)
        {
            IDataConnector dataConnector;
            if (_connectors.TryGetValue(commPort, out dataConnector))
                try
                {
                    dataConnector.Start();
                    _Logger.Info("Tunnel服务启动成功");
                    return true;
                }
                catch (Exception e)
                {
                    _Logger.Error(string.Format("Tunnel服务失败:{0}", e.Message), e);
                    return false;
                }

            return false;
        }

        public override bool Stop(CommPort commPort)
        {
            _isTaskContinueds[commPort] = false;
            Thread.Sleep(200);
            IDataConnector connector;
            if (_connectors.TryGetValue(commPort, out connector))
            {
                connector.Stop();
                _Logger.Info("Tunnel服务停止成功");
            }

            return true;
        }

        #endregion
    }
}