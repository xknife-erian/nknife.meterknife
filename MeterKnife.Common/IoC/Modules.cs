using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using Ninject.Modules;

namespace MeterKnife.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<TrayMenuStrip>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
