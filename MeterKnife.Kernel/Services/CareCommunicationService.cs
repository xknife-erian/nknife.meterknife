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
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Scpi;
using NKnife.Serial.Common;
using NKnife.Serial.Generic.Filters;
using NKnife.Serial.Interfaces;
using NKnife.Socket.Generic;
using NKnife.Socket.Generic.Filters;
using NKnife.Socket.Interfaces;
using NKnife.Tunnel;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Util;

namespace MeterKnife.Kernel.Services
{
    public class CareCommunicationService : BaseCareCommunicationService
    {
        private const string FAMILY_NAME = "careone";
        private static readonly ILog _logger = LogManager.GetLogger<CareCommunicationService>();
        private readonly List<CommPort> _CarePortList = new List<CommPort>();
        private readonly Dictionary<CommPort, IDataConnector> _Connectors = new Dictionary<CommPort, IDataConnector>();

        private readonly Dictionary<CommPort, BytesProtocolFilter> _Filters =
            new Dictionary<CommPort, BytesProtocolFilter>();

        private readonly Dictionary<CommPort, bool> _IsTaskContinueds = new Dictionary<CommPort, bool>();
        private readonly CommPortCommandMap _LoopCommandMap = new CommPortCommandMap();
        private readonly Dictionary<CommPort, ScpiCommandQueue> _Queues = new Dictionary<CommPort, ScpiCommandQueue>();

        public override bool Initialize()
        {
            Cares = new List<CommPort>();
            var careService = new CareService();
            careService.SerialFinder(this);
            DI.Get<IMeterKernel>().Collected += (s, e) =>
            {
                if (!e.IsCollected && _LoopCommandMap.ContainsKey(e.CarePort))
                {
                    _LoopCommandMap.Remove(e.CarePort, e.ScpiGroupKey);
                }
            };
            IsInitialized = true;
            return true;
        }

        public override void Bind(CommPort commPort, params CareOneProtocolHandler[] handlers)
        {
            BytesProtocolFilter filter = null;
            if (!_Filters.TryGetValue(commPort, out filter))
            {
                switch (commPort.TunnelType)
                {
                    case TunnelType.Tcpip:
                    {
                        BuildConnector(commPort, new SocketBytesProtocolFilter());
                        var dataConnector = _Connectors[commPort] as ISocketClient;
                        if (dataConnector != null)
                        {
                            dataConnector.Config = new SocketClientConfig();
                            IPEndPoint ip = commPort.GetIpEndPoint();
                            dataConnector.Configure(ip.Address, ip.Port);
                            Start(commPort);
                        }
                        break;
                    }
                    case TunnelType.Serial:
                    default:
                    {
                        BuildConnector(commPort, new SerialProtocolFilter());
                        var dataConnector = _Connectors[commPort] as ISerialConnector;
                        if (dataConnector != null)
                        {
                            int[] serialport = commPort.GetSerialPortInfo();
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
                filter = _Filters[commPort];
            }
            foreach (CareOneProtocolHandler handler in handlers)
            {
                if (filter != null)
                    filter.AddHandlers(handler);
            }
        }

        protected virtual void BuildConnector(CommPort commPort, BytesProtocolFilter filter)
        {
            if (_CarePortList.Contains(commPort))
                return;
            _CarePortList.Add(commPort);
            var sb = new StringBuilder("PortList:");
            foreach (CommPort port in _CarePortList)
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

            var dataConnector = DI.Get<IDataConnector>(commPort.TunnelType.ToString());
            _Filters.Add(commPort, filter); //增加协议过滤器
            _Connectors.Add(commPort, dataConnector);

            //建立针对端口的指令队列
            var queue = new ScpiCommandQueue();
            _Queues.Add(commPort, queue);
            _IsTaskContinueds.Add(commPort, true); //队列循环监听
            StartQueueTask(commPort, dataConnector, queue);

            tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力
            _logger.Info(string.Format("PortList:{0},Filters:{1},Connectors:{2}", _CarePortList.Count, _Filters.Count,
                _Connectors.Count));
        }

        #region Remove,Destroy,Start,Stop

        public override void Remove(CommPort tunnelPort, CareOneProtocolHandler handler)
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
            foreach (CommPort port in _CarePortList)
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

        public override bool Start(CommPort commPort)
        {
            IDataConnector dataConnector;
            if (_Connectors.TryGetValue(commPort, out dataConnector))
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

        public override bool Stop(CommPort commPort)
        {
            _IsTaskContinueds[commPort] = false;
            Thread.Sleep(200);
            IDataConnector connector;
            if (_Connectors.TryGetValue(commPort, out connector))
            {
                connector.Stop();
                _logger.Info("Tunnel服务停止成功");
            }
            return true;
        }

        #endregion

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
                _LoopCommandMap.Add(commPort, commandArrayKey, careItems);
                EnqueueCommand(commPort, ScpiCommandQueue.Item.NullCommand());
            });
        }

        private void EnqueueCommand(CommPort commPort, params ScpiCommandQueue.Item[] careItems)
        {
            foreach (ScpiCommandQueue.Item careItem in careItems)
            {
                _Queues[commPort].Enqueue(careItem);
            }
        }

        protected virtual void StartQueueTask(CommPort commPort, IDataConnector dataConnector, ScpiCommandQueue queue)
        {
            Task.Factory.StartNew(() =>
            {
                while (_IsTaskContinueds[commPort])
                {
                    if (queue.Count <= 0)
                    {
                        //当队列中无指令时，监测是否有循环指令等待发送
                        while (_LoopCommandMap.HasCommand(commPort))
                        {
                            var keys = _LoopCommandMap.GetScpiGroupKeys(commPort);
                            try
                            {
                                foreach (var key in keys)
                                {
                                    //一个端口可能有多个指令组，一般是多台仪器（每仪器有一个GPIB地址）
                                    //每仪器对应一个指令组
                                    //一个指令组下的多条指令，指令的延迟在SendCommand函数中发生
                                    if (_LoopCommandMap.ContainsKey(commPort, key))
                                    {
                                        var items = _LoopCommandMap[commPort, key];
                                        foreach (var careItem in items)
                                        {
                                            SendCommand(dataConnector, careItem);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                _logger.Warn(string.Format("集合循环停止:{0}", e.Message), e);
                                break;
                            }
                            //当WaitOne时进入了循环，虽然Queue里有了数据，但信号未接收到，需判断
                            if (queue.Count > 0)
                                break;
                        }
                        queue.AddEvent.WaitOne();
                    }

                    if (!queue.TryDequeue(out ScpiCommandQueue.Item cmd))
                        continue;
                    if (cmd == null || cmd.GpibAddress < 0)
                        continue;
                    SendCommand(dataConnector, cmd);
                }
                _logger.Debug(string.Format("退出{0}命令队列循环", commPort));
            });
        }

        protected static void SendCommand(IDataConnector dataConnector, ScpiCommandQueue.Item cmd)
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