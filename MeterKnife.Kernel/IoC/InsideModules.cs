using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Kernel.Services;

namespace MeterKnife.Kernel.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MeterKernel>().As<IMeterKernel>().SingleInstance();
            builder.RegisterType<CareTemperatureService>().As<ITemperatureService>().SingleInstance();
            builder.RegisterType<CareCommunicationService>().As<BaseCareCommunicationService>().SingleInstance();
        }
    }
}
