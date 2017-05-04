using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huaxin.MultiTemperature.App.ViewModels;
using Huaxin.MultiTemperature.App.Views;
using Ninject.Modules;

namespace Huaxin.MultiTemperature.App.IoC
{
    public class ViewModelModules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<WorkbenchViewModel>().ToSelf().InSingletonScope();
            Bind<RealTimePlotViewViewModel>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
