using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;

namespace Huaxin.MultiTemperature.ViewModels.IoC
{
    public class ViewModelModules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<WorkbenchViewModel>().ToSelf().InSingletonScope();

            Bind<CompanyAndMeterInfoViewModel>().ToSelf().InSingletonScope();
            Bind<RealTimePlotViewViewModel>().ToSelf().InSingletonScope();
            Bind<ProjectAndDatasViewModel>().ToSelf().InSingletonScope();
        }

        #endregion
    }
}
