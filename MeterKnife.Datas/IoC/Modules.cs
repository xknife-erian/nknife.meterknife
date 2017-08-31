using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Datas.Dpi;
using MeterKnife.Interfaces;
using Ninject.Modules;
using NKnife.Interface;

namespace MeterKnife.Datas.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IDatabaseService>().To<DatabaseService>().InSingletonScope();
        }

        #endregion
    }
}
