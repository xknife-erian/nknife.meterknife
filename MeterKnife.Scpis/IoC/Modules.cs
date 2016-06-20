using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;

namespace MeterKnife.Scpis.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IScpiInfoGetter>().To<ScpiInfoGetter>().InSingletonScope();
        }
    }
}
