using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Converts;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
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
        private readonly Dictionary<int, CareOneProtocolHandler> _CareHandlers = new Dictionary<int, CareOneProtocolHandler>();

        private readonly List<int> _PortList = new List<int>();
        private readonly Dictionary<int, SerialProtocolFilter> _ProtocolFilters = new Dictionary<int, SerialProtocolFilter>();
        private readonly Dictionary<int, ISerialConnector> _SerialConnector = new Dictionary<int, ISerialConnector>();

        public override Dictionary<int, CareOneProtocolHandler> CareHandlers
        {
            get { return _CareHandlers; }
        }

        public override bool Initialize()
        {
            StringCollection serialList = PcInterfaces.GetSerialList();
            foreach (string serial in serialList)
            {
                string com = serial.TrimStart(new[] {'C', 'O', 'M'});
                int port = 0;
                if (int.TryParse(com, out port))
                {
                    if (port > 0)
                    {
                        bool onFindCare = true;
                        var handler = new ScpiProtocolHandler();
                        handler.ProtocolRecevied += (s, e) =>
                        {
                            if (onFindCare)
                            {
                                if (e.Item.ToLower().StartsWith("care"))
                                {
                                    OnSerialInitialized(new EventArgs<int>(port));
                                    _CareHandlers.Add(port, handler);
                                }
                            }
                            onFindCare = false;
                        };
                        Build(port, handler);
                        Start(port);
                        _logger.Info(string.Format("串口{0}启动完成,发送寻找Care指令", port));
                        Send(port, GetCareTest());
                    }
                }
            }
            return true;
        }

        public override void Build(int port, params CareOneProtocolHandler[] handlers)
        {
            SerialProtocolFilter filter = BuildConnector(port);
            foreach (CareOneProtocolHandler handler in handlers)
            {
                filter.AddHandlers(handler);
            }
        }

        public override void Destroy()
        {
            foreach (int port in _PortList)
            {
                ISerialConnector connector;
                if (_SerialConnector.TryGetValue(port, out connector))
                {
                    connector.Stop();
                    _SerialConnector.Remove(port);
                    _ProtocolFilters.Remove(port);
                }
            }
            _PortList.Clear();
        }

        public override bool Start(int port)
        {
            ISerialConnector dataConnector;
            if (_SerialConnector.TryGetValue(port, out dataConnector))
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

        public override bool Stop(int port)
        {
            ISerialConnector connector;
            if (_SerialConnector.TryGetValue(port, out connector))
            {
                connector.Stop();
                _logger.Info("Tunnel服务停止成功");
            }
            return true;
        }

        public override void Send(int port, byte[] data)
        {
            ISerialConnector connector;
            if (_SerialConnector.TryGetValue(port, out connector))
            {
                connector.SendAll(data);
            }
        }

        private SerialProtocolFilter BuildConnector(int port)
        {
            if (_PortList.Contains(port))
                return _ProtocolFilters[port];
            _PortList.Add(port);
            var tunnel = DI.Get<ITunnel>();
            var serialProtocolFilter = new SerialProtocolFilter();

            var codec = DI.Get<BytesCodec>();
            codec.CodecName = FAMILY_NAME;
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = FAMILY_NAME;

            serialProtocolFilter.Bind(codec, family);

            tunnel.AddFilters(serialProtocolFilter);

            var dataConnector = DI.Get<ISerialConnector>();
            dataConnector.SerialConfig = new SerialConfig
            {
                BaudRate = 115200,
                ReadBufferSize = 258,
                ReadTimeout = 100
            };
            dataConnector.PortNumber = port; //串口
            _ProtocolFilters.Add(port, serialProtocolFilter);
            _SerialConnector.Add(port, dataConnector);

            tunnel.BindDataConnector(dataConnector); //dataConnector是数据流动的动力
            return serialProtocolFilter;
        }

        #region Test Main

        private static void Main(string[] args)
        {
            const int PORT = 4;
            Console.ResetColor();
            Console.WriteLine("**** START ****************************");

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var server = new CareCommunicationService();
            server.Build(PORT, new TestProtocolHandler());
            server.Start(PORT);

            Thread.Sleep(100);

            const int COUNT = 5;
            Console.WriteLine("--{0}--------------", COUNT);
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < COUNT; i++) //以询查指令进行测试
            {
                for (int j = 209; j < 221; j++)
                {
                    byte[] command = GetA0(UtilityConvert.ConvertTo<byte>(j));
                    server.Send(PORT, command);
                }
            }
            for (int i = 0; i < COUNT*10; i++)
            {
                byte[] command = GetRead(23);
                server.Send(PORT, command);
            }
            sw.Stop();
            Console.WriteLine();
            Console.ReadLine();
        }

        private static byte[] GetRead(byte address)
        {
            return new byte[] {0x08, address, 0x09, 0xAA, 0x00, 0x52, 0x45, 0x41, 0x44, 0x3F, 0x0D, 0x0A};
        }

        private static byte[] GetCareTest()
        {
            return new byte[] {0x08, 0x00, 0x02, 0xA0, 0xD1};
        }

        private static byte[] GetA0(byte subCommand)
        {
            return new byte[] {0x08, 0x00, 0x02, 0xA0, subCommand};
        }

        public class TestProtocolHandler : CareOneProtocolHandler
        {
            public override void Recevied(CareSaying protocol)
            {
                CareSaying saying = protocol;
                _logger.Info(string.Format("{0},Recevied:{1}", protocol.Command.ToHexString(), saying.Content));
            }
        }

        #endregion
    }
}