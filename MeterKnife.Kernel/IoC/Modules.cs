using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using Ninject;
using Ninject.Modules;

namespace MeterKnife.Kernel.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IKernels>().To<Kernels>().InSingletonScope();
            Bind<IHabitedDatas>().To<HabitedDatas>().InSingletonScope();

            Bind<IExtenderProvider>().To<ExtenderProvider>();
            Bind<IPluginService>().To<PluginsService>().InSingletonScope();
        }

        #endregion
    }
}
