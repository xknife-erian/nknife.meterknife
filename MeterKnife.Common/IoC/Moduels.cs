using Ninject.Modules;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.UserData;
using NKnife.IoC;

namespace MeterKnife.Common.IoC
{
    public class Moduels : NinjectModule
    {
        public override void Load()
        {
            Bind<MeterKnifeUserData>().To<MeterKnifeUserData>().InSingletonScope();
        }
    }
}
