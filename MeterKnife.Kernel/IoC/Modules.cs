using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Plugin;
using Ninject.Modules;

namespace MeterKnife.Kernel.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IPluginManager>().To<PluginManager>().InSingletonScope();
        }

        #endregion
    }
}
