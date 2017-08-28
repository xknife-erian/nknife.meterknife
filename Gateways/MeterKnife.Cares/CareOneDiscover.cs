using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Cares
{
    public class CareOneDiscover : IGatewayDiscover
    {
        public GatewayModel GatewayModel { get; set; }

        public List<Instrument> Instruments { get; } = new List<Instrument>();

        public event EventHandler Discovered;

        protected virtual void OnDiscovered()
        {
            Discovered?.Invoke(this, EventArgs.Empty);
        }

        public void BeginDiscover()
        {
        }
    }
}