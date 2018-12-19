using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using MeterKnife.Cares;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Gateway
{
    public class GatewayService : IGatewayService
    {
        private static readonly ILog Logger = LogManager.GetLogger<GatewayService>();
        private readonly IUserHabits _habitedDatas = DI.Get<IUserHabits>();

        private Thread _gatewayCoreThread;
        private readonly AutoResetEvent _autoReset = new AutoResetEvent(false);



        private void LoadAllGateways()
        {
            foreach (var gateway in _habitedDatas.Gateways.Keys)
            {
                switch (gateway)
                {
                    case GatewayModel.Aglient82357A:
                    case GatewayModel.Aglient82357B:
                    {
                        //var channel = new KeysightChannel();
                        //channel.Open();
                        break;
                    }
                    case GatewayModel.CareOne:
                    case GatewayModel.CareTwo:
                    {
                        var config = new SerialConfig(10);
                        var channel = new CareOneSerialChannel(config);
                        channel.Open();
                        break;
                    }
                    case GatewayModel.SerialPort:
                    case GatewayModel.TcpIp:
                    case GatewayModel.Usb:
                        break;
                }
            }
            _autoReset.Reset();
        }

        private void UnLoadAllGateways()
        {
            foreach (var gateway in _habitedDatas.Gateways.Keys)
            {
                switch (gateway)
                {
                    case GatewayModel.CareOne:
                    case GatewayModel.CareTwo:
                    case GatewayModel.Aglient82357A:
                    case GatewayModel.Aglient82357B:
                    case GatewayModel.SerialPort:
                    case GatewayModel.TcpIp:
                    case GatewayModel.Usb:
                        break;
                }
            }
            _autoReset.Set();
            Logger.Info($"{_gatewayCoreThread.Name}线程AutoReset.Set.");
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            _gatewayCoreThread = new Thread(LoadAllGateways) {Name = $"{nameof(GatewayService)}-Thread", IsBackground = true};
            _gatewayCoreThread.Start();
            Logger.Info($"{_gatewayCoreThread.Name}线程启动.");
            return true;
        }

        public bool CloseService()
        {
            UnLoadAllGateways();
            return true;
        }

        public int Order { get; } = 10;
        public string Description { get; } = "全局的测量途径服务";

        #endregion
    }
}