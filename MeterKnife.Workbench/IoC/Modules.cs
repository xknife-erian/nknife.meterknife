using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ninject.Modules;

namespace MeterKnife.Workbench.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<Form>().To<MainWorkbench>().InSingletonScope().Named("main");
        }
    }
}
