﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Common.Util;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Wrapper;
using ScpiKnife;
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
        private readonly Dictionary<CarePort, BytesProtocolFilter> _Filters = new Dictionary<CarePort, BytesProtocolFilter>();
        private readonly Dictionary<CarePort, IDataConnector> _Connectors = new Dictionary<CarePort, IDataConnector>();
        private readonly Dictionary<CarePort, CommandQueue> _Queues = new Dictionary<CarePort, CommandQueue>();
        private readonly Dictionary<CarePort, Task> _Tasks = new Dictionary<CarePort, Task>();
        private readonly Dictionary<CarePort, bool> _IsTaskContinueds = new Dictionary<CarePort, bool>();

        public override bool Initialize()
        {
            Cares = new List<CarePort>();
            var careService = new CareService();
            careService.SerialFinder(this);
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
                            var serialport = carePort.GetSerialPort();
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

        public override void Remove(CarePort tunnelPort, CareOneProtocolHandler handler)
        {
            BytesProtocolFilter filter;
            if (_Filters.TryGetValue(tunnelPort, out filter))
                filter.RemoveHandler(handler);
        }

        /// <summary>
        /// 销毁服务中的所有对象
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
                    _Tasks.Remove(port);
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

        public override void Send(CarePort carePort, short gpib, ScpiCommand scpiCommand)
        {
            var careItem = new CommandQueue.CareItem();
            careItem.ScpiCommand = scpiCommand;
            careItem.GpibAddress = gpib;
            Send(carePort, gpib, careItem);
        }

        public override void Send(CarePort carePort, short gpib, CommandQueue.CareItem careItem)
        {
            _Queues[carePort].Enqueue(careItem);
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
            _IsTaskContinueds.Add(carePort, true);
            var queue = new CommandQueue();
            _Queues.Add(carePort, queue);
            var task = StartQueueTask(carePort, dataConnector, queue);
            _Tasks.Add(carePort, task);

            tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力
            _logger.Info(string.Format("PortList:{0},Filters:{1},Connectors:{2}", _CarePortList.Count, _Filters.Count, _Connectors.Count));
        }

        protected virtual Task StartQueueTask(CarePort carePort, IDataConnector dataConnector, CommandQueue queue)
        {
            var task = new Task(() =>
            {
                while (_IsTaskContinueds[carePort])
                {
                    if (queue.Count <= 0)
                        queue.AutoResetEvent.WaitOne();
                    SendCommand(dataConnector, queue);
                }
            });
            task.Start();
            return task;
        }

        protected static void SendCommand(IDataConnector dataConnector, CommandQueue queue)
        {
            var cmd = queue.Dequeue();
            var data = cmd.ScpiCommand.GenerateProtocol(cmd.GpibAddress);
            if (data.Length != 0)
            {
                dataConnector.SendAll(data);
                queue.Sleep((int)cmd.ScpiCommand.Interval);
                //TODO:队列的间隔可提前终止
            }
        }
    }
}