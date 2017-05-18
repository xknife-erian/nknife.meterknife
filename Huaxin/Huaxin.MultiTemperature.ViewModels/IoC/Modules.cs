using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;

namespace Huaxin.MultiTemperature.ViewModels.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IIdGenerator>().To<IdGenerator>().InSingletonScope();
            Bind<Kernel>().To<Kernel>().InSingletonScope();
        }

        #endregion
    }
}
