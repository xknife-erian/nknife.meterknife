using Ninject.Activation;
using Ninject.Modules;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.UserData;

namespace NKnife.IoC
{
    public class ConfiguringModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserApplicationData>().To<UserApplicationData>().When(Request).InSingletonScope();
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
