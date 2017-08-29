using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Base;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.ViewModels
{
    public class InstrumentsDiscoveryViewModel : ViewmodelBaseKnife
    {
        public List<IGatewayDiscover> Discovers { get; set; }

        public InstrumentsDiscoveryViewModel()
        {
            Discovers = new List<IGatewayDiscover>();
            Discovers.Add(DI.Get<IGatewayDiscover>(GatewayModel.Aglient82357A.ToString()));
            Discovers.Add(DI.Get<IGatewayDiscover>(GatewayModel.CareOne.ToString()));
        }
    }
}
