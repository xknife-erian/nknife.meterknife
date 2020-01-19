using System;
using NKnife.Events;
using NKnife.MeterKnife.Common.Tunnels.CareOne;

namespace NKnife.MeterKnife.Common.Tunnels
{
    public class ScpiProtocolHandler : CareProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public ScpiProtocolHandler()
        {
            Commands.Add(new byte[] { 0xAA, 0x00 });
            Commands.Add(new byte[] { 0xAB, 0x00 });
            //---
            Commands.Add(new byte[] { 0xAA, 0x01 });
        }

        public override void Received(CareTalking protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                OnProtocolReceived(new EventArgs<CareTalking>(protocol));
            }
        }

        public event EventHandler<EventArgs<CareTalking>> ProtocolReceived;

        protected virtual void OnProtocolReceived(EventArgs<CareTalking> e)
        {
            ProtocolReceived?.Invoke(this, e);
        }

    }
}
