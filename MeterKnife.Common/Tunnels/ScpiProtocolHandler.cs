using System;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;

namespace MeterKnife.Common.Tunnels
{
    public class ScpiProtocolHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<ScpiProtocolHandler>();

        public override void Recevied(CareTalking protocol)
        {
            _logger.Trace(string.Format("{1}^{2}:{0}", protocol.Scpi, protocol.MainCommand.ToHexString(), protocol.SubCommand.ToHexString()));
            OnProtocolRecevied(new EventArgs<CareTalking>(protocol));
        }

        public event EventHandler<EventArgs<CareTalking>> ProtocolRecevied;

        protected virtual void OnProtocolRecevied(EventArgs<CareTalking> e)
        {
            EventHandler<EventArgs<CareTalking>> handler = ProtocolRecevied;
            if (handler != null)
                handler(this, e);
        }
    }
}
