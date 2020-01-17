using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;

namespace MeterKnife.Common.Tunnels
{
    public class CareConfigHandler : CareOneProtocolHandler
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

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
            _Logger.Trace(message: string.Format("{1}^{2}:{0}", protocol.Scpi, protocol.MainCommand.ToHexString(), protocol.SubCommand.ToHexString()));
            if (!string.IsNullOrEmpty(protocol.Scpi))
            {
                OnCareSetting(new EventArgs<CareTalking>(protocol));
            }
        }

        public event EventHandler<EventArgs<CareTalking>> CareSetting;

        protected virtual void OnCareSetting(EventArgs<CareTalking> e)
        {
            CareSetting?.Invoke(this, e);
        }
    }
}
