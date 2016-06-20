using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ninject.Activation;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;

namespace MeterKnife.Workbench.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IAbout>().To<About>().InSingletonScope();
            Bind<Form>().To<MainWorkbench>().InSingletonScope().Named("main");
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
