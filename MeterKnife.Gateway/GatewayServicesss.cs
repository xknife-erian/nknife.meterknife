using System.Collections.Generic;
using MeterKnife.Interfaces;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace MeterKnife.Gateway
{
    public class GatewayServicesss : IGatewayService
    {
        private List<GatewayModelsss> _GateWayModels = new List<GatewayModelsss>();

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            IChannel<byte[]> channel = new SerialChannel(new SerialConfig(2));
            return true;
        }

        public bool CloseService()
        {
            return false;
        }

        public int Order { get; } = 10;
        public string Description { get; } = "";

        #endregion
    }
}