using System.Collections.Generic;
using System.Diagnostics;
using Common.Logging;
using MonitorKnife.Common.DataModels;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace MeterKnife.Kernel.Tunnels
{
    public abstract class CareOneProtocolHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareOneProtocolHandler>();

        protected CareOneProtocolHandler()
        {
            Commands = new List<byte[]>();
        }

        public override sealed List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareSaying))
            {
                Debug.Assert(false, "Protocol类型有误, 不是CareSaying类型");
                _logger.Warn("Protocol类型有误, 不是CareSaying类型");
                return;
            }

            Recevied((CareSaying)protocol);
        }

        public abstract void Recevied(CareSaying protocol);
    }


}