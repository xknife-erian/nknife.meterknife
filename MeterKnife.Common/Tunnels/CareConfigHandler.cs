using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;

namespace MeterKnife.Common.Tunnels
{
    public class CareConfigHandler : CareOneProtocolHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareConfigHandler>();

        public CareConfigHandler()
        {
            Commands.Add(new byte[] { 0xA0, 0xD0 });
            Commands.Add(new byte[] { 0xA0, 0xD1 });
            Commands.Add(new byte[] { 0xA0, 0xD2 });
            Commands.Add(new byte[] { 0xA0, 0xD3 });
            Commands.Add(new byte[] { 0xA0, 0xD4 });
            Commands.Add(new byte[] { 0xA0, 0xD5 });
            Commands.Add(new byte[] { 0xA0, 0xD6 });
            Commands.Add(new byte[] { 0xA0, 0xD7 });
            //Commands.Add(new byte[] { 0xA0, 0xD8 });
            Commands.Add(new byte[] { 0xA0, 0xD9 });
            Commands.Add(new byte[] { 0xA0, 0xDA });
            Commands.Add(new byte[] { 0xA0, 0xDB });
            Commands.Add(new byte[] { 0xA0, 0xDC });
            Commands.Add(new byte[] { 0xA0, 0xDD });
            Commands.Add(new byte[] { 0xA0, 0xDE });
            Commands.Add(new byte[] { 0xA0, 0xDF });
        }

        public override void Received(CareTalking protocol)
        {
            _logger.Trace(string.Format("{1}^{2}:{0}", protocol.Scpi, protocol.MainCommand.ToHexString(), protocol.SubCommand.ToHexString()));
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                OnCareConfigging(new EventArgs<CareTalking>(protocol));
            }
        }

        public event EventHandler<EventArgs<CareTalking>> CareConfigging;

        protected virtual void OnCareConfigging(EventArgs<CareTalking> e)
        {
            EventHandler<EventArgs<CareTalking>> handler = CareConfigging;
            if (handler != null)
                handler(this, e);
        }
    }
}
