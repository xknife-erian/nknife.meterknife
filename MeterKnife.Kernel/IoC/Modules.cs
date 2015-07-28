using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Kernel.Services;
using Ninject.Modules;

namespace MeterKnife.Kernel.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<IMeterKernel>().To<MeterKernel>().InSingletonScope();
            Bind<BaseCareCommunicationService>().To<CareCommunicationService>().InSingletonScope();
            Bind<ITemperatureService>().To<TemperatureService>().InSingletonScope();
        }
    }
}
