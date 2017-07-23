using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;
using Ninject.Modules;

namespace MeterKnife.Gateway.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IGatewayService>().To<GateWayService>().InSingletonScope();
        }

        #endregion
    }
}
