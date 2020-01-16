using System;
using System.Threading;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;

namespace MeterKnife.Common.Tunnels
{
    public class ScpiProtocolHandler : CareOneProtocolHandler
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

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
