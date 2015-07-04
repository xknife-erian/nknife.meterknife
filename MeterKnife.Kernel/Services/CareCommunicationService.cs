﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Wrapper;
using SerialKnife.Common;
using SerialKnife.Generic.Filters;
using SerialKnife.Interfaces;

namespace MeterKnife.Kernel.Services
{
    public class CareCommunicationService : BaseCareCommunicationService
    {
        private const string FAMILY_NAME = "careone";
        private static readonly ILog _logger = LogManager.GetLogger<CareCommunicationService>();

        private readonly List<CarePort> _CarePortList = new List<CarePort>();
        private readonly Dictionary<CarePort, BytesProtocolFilter> _ProtocolFilters = new Dictionary<CarePort, BytesProtocolFilter>();
        private readonly Dictionary<CarePort, IDataConnector> _SerialConnector = new Dictionary<CarePort, IDataConnector>();

        public override bool Initialize()
        {
            Cares = new List<CarePort>();
            SerialFinder();
            IsInitialized = true;
            return true;
        }

        /// <summary>
        ///     在串口中寻找Care
        /// </summary>
        protected virtual void SerialFinder()
        {
            var resetEvent = new AutoResetEvent(true);
            StringCollection serialList = PcInterfaces.GetSerialList();
            foreach (string serial in serialList)
            {
                resetEvent.Set();
                string com = serial.ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                int port = 0;
                if (!int.TryParse(com, out port))
                    continue;
                if (port <= 0)
                    continue;
                CarePort carePort = CarePort.Build(TunnelType.Serial, port.ToString());

                bool onFindCare = true;
                var handler = new CareConfigHandler();
                handler.CareConfigging += (s, e) =>
                {
                    if (onFindCare)
                    {
                        if (e.Item.Scpi.ToLower().StartsWith("care"))
                        {
                            resetEvent.Set();
                            OnSerialInitialized(new EventArgs<CarePort>(carePort));
                            Cares.Add(carePort);
                        }
                    }
                    onFindCare = false;
                };

                Bind(carePort, handler);
                Start(carePort);

                _logger.Info(string.Format("串口{0}启动完成,发送寻找Care指令", port));
                Send(carePort, CareTalking.CareGetter());
                if (resetEvent.WaitOne(100))
                {
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    byte[] timebs = Encoding.ASCII.GetBytes(time);
                    _logger.Info(string.Format("Set Care Time:{0}", time));
                    Send(carePort, CareTalking.CareSetter(0xD9, timebs));
                    Thread.Sleep(200);
                }
                Remove(carePort, handler);
            }
        }

        public override void Bind(CarePort carePort, params CareOneProtocolHandler[] handlers)
        {
            BytesProtocolFilter filter = null;
            switch (carePort.TunnelType)
            {
                case TunnelType.Socket:
                case TunnelType.Serial:
                default:
                {
                    if (!_ProtocolFilters.TryGetValue(carePort, out filter))
                    {
                        BuildSerialConnector(carePort);
                        filter = _ProtocolFilters[carePort];
                    }
                    break;
                }
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
            if (_ProtocolFilters.TryGetValue(tunnelPort, out filter))
                filter.RemoveHandler(handler);
        }

        public override void Destroy()
        {
            foreach (CarePort port in _CarePortList)
            {
                IDataConnector connector;
                if (_SerialConnector.TryGetValue(port, out connector))
                {
                    connector.Stop();
                    _SerialConnector.Remove(port);
                    _ProtocolFilters.Remove(port);
                }
            }
            _CarePortList.Clear();
        }

        public override bool Start(CarePort carePort)
        {
            IDataConnector dataConnector;
            if (_SerialConnector.TryGetValue(carePort, out dataConnector))
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
            if (_SerialConnector.TryGetValue(carePort, out connector))
            {
                connector.Stop();
                _logger.Info("Tunnel服务停止成功");
            }
            return true;
        }

        public override void Send(CarePort carePort, byte[] data)
        {
            IDataConnector connector;
            if (_SerialConnector.TryGetValue(carePort, out connector))
            {
                connector.SendAll(data);
                _logger.Trace(string.Format("To:{0}", data.ToHexString()));
            }
        }

        protected virtual void BuildSerialConnector(CarePort carePort)
        {
            if (_CarePortList.Contains(carePort))
                return;
            _CarePortList.Add(carePort);

            //启动串口数据管道
            var tunnel = DI.Get<ITunnel>();
            var filter = new SerialProtocolFilter();

            var codec = DI.Get<BytesCodec>();
            codec.CodecName = FAMILY_NAME;
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;

            filter.Bind(codec, family);

            tunnel.AddFilters(filter);

            var dataConnector = DI.Get<ISerialConnector>();
            dataConnector.SerialConfig = new SerialConfig
            {
                BaudRate = 115200,
                ReadBufferSize = 258,
                ReadTimeout = 100*10
            };
            dataConnector.PortNumber = carePort.GetSerialPort(); //串口
            _ProtocolFilters.Add(carePort, filter); //增加协议过滤器
            _SerialConnector.Add(carePort, dataConnector);

            tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力
        }
    }
}