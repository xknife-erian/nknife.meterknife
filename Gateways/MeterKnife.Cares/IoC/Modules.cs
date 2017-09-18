using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Gateways;
using Ninject.Modules;
using NKnife.Channels.Channels.Serials;

namespace MeterKnife.Cares.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IGatewayDiscover>().To<CareOneDiscover>().InSingletonScope().Named(nameof(GatewayModel.CareOne));
        }

        #endregion
    }
}
