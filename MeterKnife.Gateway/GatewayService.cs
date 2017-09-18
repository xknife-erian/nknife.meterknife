using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Keysights;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Gateway
{
    public class GatewayService : IGatewayService
    {
        private static readonly ILog _logger = LogManager.GetLogger<GatewayService>();
        private readonly IHabited _HabitedDatas = DI.Get<IHabited>();

        private Thread _GatewayCoreThread;
        private AutoResetEvent _AutoReset = new AutoResetEvent(false);

        private void LoadAllGateways()
        {
            foreach (var gateway in _HabitedDatas.Gateways.Keys)
            {
                switch (gateway)
                {
                    case GatewayModel.Aglient82357A:
                    case GatewayModel.Aglient82357B:
                    {
                        var channel = new KeysightChannel();
                        channel.Open();
                        break;
                    }
                    case GatewayModel.CareOne:
                    case GatewayModel.CareTwo:
                    {
                        var channel = DI.Get<SerialChannel>(nameof(GatewayModel.CareOne));
                        channel.Open();
                        break;
                    }
                    case GatewayModel.SerialPort:
                    case GatewayModel.TcpIp:
                    case GatewayModel.USB:
                        break;
                }
            }
            _AutoReset.Reset();
        }

        private void UnLoadAllGateways()
        {
            foreach (var gateway in _HabitedDatas.Gateways.Keys)
            {
                switch (gateway)
                {
                    case GatewayModel.CareOne:
                    case GatewayModel.CareTwo:
                    case GatewayModel.Aglient82357A:
                    case GatewayModel.Aglient82357B:
                    case GatewayModel.SerialPort:
                    case GatewayModel.TcpIp:
                    case GatewayModel.USB:
                        break;
                }
            }
            _AutoReset.Set();
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            _GatewayCoreThread = new Thread(LoadAllGateways) {Name = $"{nameof(GatewayService)}-Thread", IsBackground = true};
            _GatewayCoreThread.Start();
            return true;
        }

        public bool CloseService()
        {
            UnLoadAllGateways();
            return true;
        }

        public int Order { get; } = 10;
        public string Description { get; } = "";

        #endregion
    }
}