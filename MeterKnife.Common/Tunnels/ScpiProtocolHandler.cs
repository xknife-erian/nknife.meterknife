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

        public override void Recevied(CareSaying protocol)
        {
            _logger.Trace(string.Format("SCPI:{0}", protocol.Content.TrimEnd('\n')));
            OnProtocolRecevied(new EventArgs<string>(protocol.Content));
        }

        public event EventHandler<EventArgs<string>> ProtocolRecevied;

        protected virtual void OnProtocolRecevied(EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = ProtocolRecevied;
            if (handler != null)
                handler(this, e);
        }
    }
}
