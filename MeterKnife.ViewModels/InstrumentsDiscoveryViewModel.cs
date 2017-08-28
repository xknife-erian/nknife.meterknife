using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Base;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.ViewModels
{
    public class InstrumentsDiscoveryViewModel : ViewmodelBaseKnife
    {
        public List<IGatewayDiscover> Discovers { get; set; }
    }
}
