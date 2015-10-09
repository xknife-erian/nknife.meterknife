using System;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;

namespace MeterKnife.Common.Tunnels
{
    public class ScpiProtocolHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<ScpiProtocolHandler>();

        public ScpiProtocolHandler()
        {
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override void Recevied(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                OnProtocolRecevied(new EventArgs<CareTalking>(protocol));
            }
        }

        public event EventHandler<EventArgs<CareTalking>> ProtocolRecevied;

        protected virtual void OnProtocolRecevied(EventArgs<CareTalking> e)
        {
            EventHandler<EventArgs<CareTalking>> handler = ProtocolRecevied;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
