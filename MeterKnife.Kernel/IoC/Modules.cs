using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Kernel.Services;
using Ninject.Modules;

namespace MeterKnife.Kernel.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<BaseCareCommunicationService>().To<CareCommunicationService>().InSingletonScope();
        }
    }
}
