using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huaxin.MultiTemperature.App.Views;
using Ninject.Modules;

namespace Huaxin.MultiTemperature.App.IoC
{
    public class PagesModules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<HomePage>().ToSelf().InSingletonScope();
            Bind<MeterPointPage>().ToSelf().InSingletonScope();
            Bind<MeterDatasPage>().ToSelf().InSingletonScope();
            Bind<OptionAndToolsPage>().ToSelf().InSingletonScope();
            Bind<RealTimePlotViewPage>().ToSelf().InSingletonScope();
            Bind<ProjectAndDatasPage>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
