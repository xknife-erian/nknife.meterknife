using System;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Tunnels.Handlers
{
    public class FirstProtocolHandler : CareOneProtocolHandler
    {
        public override void Recevied(CareSaying protocol)
        {
            if (protocol.Content.ToLower().StartsWith("care"))
            {
                OnIsCare();
            }
        }

        public event EventHandler IsCare;

        protected virtual void OnIsCare()
        {
            EventHandler handler = IsCare;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }
    }
}
