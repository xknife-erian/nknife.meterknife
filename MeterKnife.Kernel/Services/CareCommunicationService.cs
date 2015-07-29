﻿using System;
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
                var scmap = _LoopCommandMap[e.CarePort];
                //根据指定端口的指令组的Key停止采集指令循环
                if (scmap.ContainsKey(e.ScpiGroupKey))
                    scmap.Remove(e.ScpiGroupKey);
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
                            var ip = carePort.GetIpEndPoint();
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
                            var serialport = carePort.GetSerialPortInfo();
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
            foreach (var handler in handlers)
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
            foreach (var port in _CarePortList)
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
            foreach (var port in _CarePortList)
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

        public override void Send(CarePort carePort, bool isLooping, params CommandQueue.CareItem[] careItems)
        {
            if (UtilityCollection.IsNullOrEmpty(careItems))
                return;
            var key = GetGpibKey(carePort, careItems[0].GpibAddress);
            Task.Factory.StartNew(() =>
            {
                if (!isLooping)
                {
                    EnqueueCommand(carePort, careItems);
                }
                else
                {
                    _LoopCommandMap.Add(carePort, key, careItems);
                }
            });
        }

        private static string GetGpibKey(CarePort carePort, int gpib)
        {
            return string.Format("{0}--{1}", carePort, gpib);
        }

        private void EnqueueCommand(CarePort carePort, params CommandQueue.CareItem[] careItems)
        {
            foreach (var careItem in careItems)
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
                        //当队列中无指令时，监测是否有循环指令等待发送
                        Dictionary<string, CommandQueue.CareItem[]> commandMap;
                        if (_LoopCommandMap.TryGetValue(carePort, out commandMap))
                        {
                            foreach (var careItems in commandMap.Values)
                            {
                                foreach (var careItem in careItems)
                                {
                                    SendCommand(dataConnector, careItem);
                                }
                            }
                        }
                        queue.AutoResetEvent.WaitOne();
                    }
                    var cmd = queue.Dequeue();
                    if (cmd == null)
                        return;
                    SendCommand(dataConnector, cmd);
                }
                _logger.Info(string.Format("退出{0}命令队列循环", carePort));
            });
        }

        protected static void SendCommand(IDataConnector dataConnector, CommandQueue.CareItem cmd)
        {
            var data = cmd.IsCare
                ? CommandUtil.GenerateProtocol(cmd)
                : cmd.ScpiCommand.GenerateProtocol(cmd.GpibAddress);
            _logger.Trace(string.Format("SendCommand:{0}", data.ToHexString()));
            if (data.Length != 0)
            {
                dataConnector.SendAll(data);
                if (cmd.IsCare)
                    Thread.Sleep(cmd.Interval);
                else
                    Thread.Sleep((int) cmd.ScpiCommand.Interval);
            }
        }
    }
}