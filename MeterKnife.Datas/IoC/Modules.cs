using MeterKnife.Common.Interfaces;
using Ninject.Modules;

namespace MeterKnife.Datas.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IMeterDataService>().To<MeterDataService>().InSingletonScope();
        }
    }
}
