using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;
using Ninject.Modules;

namespace MeterKnife.DemoApplication.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IMeterKernel>().To<DemoMeterKernel>().InSingletonScope();
        }
    }
}
