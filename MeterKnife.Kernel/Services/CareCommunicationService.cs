using System;
using System.Collections.Generic;
using System.Net;
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
using MeterKnife.Kernel.Common;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SerialKnife.Common;
using SerialKnife.Generic.Filters;
using SerialKnife.Interfaces;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace MeterKnife.Kernel.Services
{
    public class CareCommunicationService : BaseCareCommunicationService
    {
        private const string FAMILY_NAME = "careone";
        private static readonly ILog _logger = LogManager.GetLogger<CareCommunicationService>();
        private readonly List<CarePort> _CarePortList = new List<CarePort>();
        private readonly Dictionary<CarePort, IDataConnector> _Connectors = new Dictionary<CarePort, IDataConnector>();

        private readonly Dictionary<CarePort, BytesProtocolFilter> _Filters =
            new Dictionary<CarePort, BytesProtocolFilter>();

        private readonly Dictionary<CarePort, bool> _IsTaskContinueds = new Dictionary<CarePort, bool>();
        private readonly LoopCommandMap _LoopCommandMap = new LoopCommandMap();
        private readonly Dictionary<CarePort, CommandQueue> _Queues = new Dictionary<CarePort, CommandQueue>();

        public override bool Initialize()
        {
            Cares = new List<CarePort>();
            var careService = new CareService();
            careService.SerialFinder(this);
            DI.Get<IMeterKernel>().Collected += (s, e) =>
            {
                if (!e.IsCollected && _LoopCommandMap.ContainsKey(e.CarePort))
                {
                    Dictionary<string, CommandQueue.CareItem[]> scmap = _LoopCommandMap[e.CarePort];
                    //根据指定端口的指令组的Key停止采集指令循环
                    if (scmap.ContainsKey(e.ScpiGroupKey))
                        scmap.Remove(e.ScpiGroupKey);
                }
            };
            IsInitialized = true;
            return true;
        }

        public override void Bind(CarePort carePort, params CareOneProtocolHandler[] handlers)
        {
            BytesProtocolFilter filter = null;
            if (!_Filters.TryGetValue(carePort, out filter))
            {
                switch (carePort.TunnelType)
                {
                    case TunnelType.Tcpip:
                    {
                        BuildConnector(carePort, new SocketBytesProtocolFilter());
                        var dataConnector = _Connectors[carePort] as ISocketClient;
                        if (dataConnector != null)
                        {
                            dataConnector.Config = new SocketClientConfig();
                            IPEndPoint ip = carePort.GetIpEndPoint();
                            dataConnector.Configure(ip.Address, ip.Port);
                            Start(carePort);
                        }
                        break;
                    }
                    case TunnelType.Serial:
                    default:
                    {
                        BuildConnector(carePort, new SerialProtocolFilter());
                        var dataConnector = _Connectors[carePort] as ISerialConnector;
                        if (dataConnector != null)
                        {
                            int[] serialport = carePort.GetSerialPortInfo();
                            dataConnector.SerialConfig = new SerialConfig
                            {
                                BaudRate = serialport[1],
                                ReadBufferSize = 258,
                                ReadTimeout = 100*10
                            };
                            dataConnector.PortNumber = serialport[0]; //串口
                        }
                        break;
                    }
                }
                filter = _Filters[carePort];
            }
            foreach (CareOneProtocolHandler handler in handlers)
            {
                if (filter != null)
                    filter.AddHandlers(handler);
            }
        }

        protected virtual void BuildConnector(CarePort carePort, BytesProtocolFilter filter)
        {
            if (_CarePortList.Contains(carePort))
                return;
            _CarePortList.Add(carePort);
            var sb = new StringBuilder("PortList:");
            foreach (CarePort port in _CarePortList)
                sb.Append(port).Append(';');
            _logger.Info(sb);

            //启动数据管道
            var tunnel = DI.Get<ITunnel>();

            var codec = DI.Get<BytesCodec>();
            codec.CodecName = FAMILY_NAME;
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;

            filter.Bind(codec, family);

            tunnel.AddFilters(filter);

            var dataConnector = DI.Get<IDataConnector>(carePort.TunnelType.ToString());
            _Filters.Add(carePort, filter); //增加协议过滤器
            _Connectors.Add(carePort, dataConnector);

            //建立针对端口的指令队列
            var queue = new CommandQueue();
            _Queues.Add(carePort, queue);
            _IsTaskContinueds.Add(carePort, true); //队列循环监听
            StartQueueTask(carePort, dataConnector, queue);

            tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力
            _logger.Info(string.Format("PortList:{0},Filters:{1},Connectors:{2}", _CarePortList.Count, _Filters.Count,
                _Connectors.Count));
        }

        #region Remove,Destroy,Start,Stop

        public override void Remove(CarePort tunnelPort, CareOneProtocolHandler handler)
        {
            BytesProtocolFilter filter;
            if (_Filters.TryGetValue(tunnelPort, out filter))
                filter.RemoveHandler(handler);
        }

        /// <summary>
        ///     销毁服务中的所有对象
        /// </summary>
        public override void Destroy()
        {
            foreach (CarePort port in _CarePortList)
            {
                IDataConnector connector;
                if (_Connectors.TryGetValue(port, out connector))
                {
                    connector.Stop();
                    _Connectors.Remove(port);
                    _Filters.Remove(port);
                    _Queues.Remove(port);
                }
            }
            _CarePortList.Clear();
        }

        public override bool Start(CarePort carePort)
        {
            IDataConnector dataConnector;
            if (_Connectors.TryGetValue(carePort, out dataConnector))
            {
                try
                {
                    dataConnector.Start();
                    _logger.Info("Tunnel服务启动成功");
                    return true;
                }
                catch (Exception e)
                {
                    _logger.Error(string.Format("Tunnel服务失败:{0}", e.Message), e);
                    return false;
                }
            }
            return false;
        }

        public override bool Stop(CarePort carePort)
        {
            IDataConnector connector;
            if (_Connectors.TryGetValue(carePort, out connector))
            {
                connector.Stop();
                _logger.Info("Tunnel服务停止成功");
            }
            return true;
        }

        #endregion

        public override void SendCommands(CarePort carePort, params CommandQueue.CareItem[] careItems)
        {
            if (UtilityCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() => EnqueueCommand(carePort, careItems));
        }

        public override void SendLoopCommands(CarePort carePort, string commandArrayKey, params CommandQueue.CareItem[] careItems)
        {
            if (UtilityCollection.IsNullOrEmpty(careItems))
                return;
            Task.Factory.StartNew(() =>
            {
                _LoopCommandMap.Add(carePort, commandArrayKey, careItems);
                EnqueueCommand(carePort, CommandQueue.CareItem.NullCommand());
            });
        }

        private void EnqueueCommand(CarePort carePort, params CommandQueue.CareItem[] careItems)
        {
            foreach (CommandQueue.CareItem careItem in careItems)
            {
                _Queues[carePort].Enqueue(careItem);
            }
        }

        protected virtual void StartQueueTask(CarePort carePort, IDataConnector dataConnector, CommandQueue queue)
        {
            Task.Factory.StartNew(() =>
            {
                while (_IsTaskContinueds[carePort])
                {
                    if (queue.Count <= 0)
                    {
                        try
                        {
                            //当队列中无指令时，监测是否有循环指令等待发送
                            while (_LoopCommandMap.ContainsKey(carePort))
                            {
                                Dictionary<string, CommandQueue.CareItem[]> commandMap = _LoopCommandMap[carePort];
                                if (commandMap.Values.Count <= 0)
                                    break;
                                //一个端口可能有多个指令组，一般是多台仪器（每仪器有一个GPIB地址）
                                //每仪器对应一个指令组
                                foreach (var careItemses in commandMap.Values)
                                {
                                    //一个指令组下的多条指令，指令的延迟在SendCommand函数中发生
                                    foreach (CommandQueue.CareItem careItem in careItemses)
                                    {
                                        SendCommand(dataConnector, careItem);
                                    }
                                }
                                //当WaitOne时进入了循环，虽然Queue里有了数据，但信号未接收到，需判断
                                if (queue.Count > 0)
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.Debug(string.Format("集合循环停止:{0}", e.Message), e);
                        }
                        queue.AutoResetEvent.WaitOne();
                    }
                    CommandQueue.CareItem cmd = queue.Dequeue();
                    if (cmd == null || cmd.GpibAddress < 0)
                        continue;
                    SendCommand(dataConnector, cmd);
                }
                _logger.Debug(string.Format("退出{0}命令队列循环", carePort));
            });
        }

        protected static void SendCommand(IDataConnector dataConnector, CommandQueue.CareItem cmd)
        {
            try
            {
                byte[] data = cmd.IsCare
                    ? CommandUtil.GenerateProtocol(cmd)
                    : cmd.ScpiCommand.GenerateProtocol(cmd.GpibAddress);
                _logger.Trace(string.Format("SendCommand:{0}", data.ToHexString()));
                if (data.Length != 0)
                {
                    dataConnector.SendAll(data);
                    if (cmd.IsCare)
                    {
                        Thread.Sleep(cmd.Interval);
                    }
                    else
                    {
                        Thread.Sleep((int)cmd.ScpiCommand.Interval);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("向采集器发送指令(SendCommand)时出现异常:{0}", e.Message), e);
            }
        }
    }
}