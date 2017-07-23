using MeterKnife.Interfaces;
using Ninject.Modules;

namespace MeterKnife.App.Tray.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IAppTrayService>().To<AppTrayService>().InSingletonScope();
        }

        #endregion
    }
}
