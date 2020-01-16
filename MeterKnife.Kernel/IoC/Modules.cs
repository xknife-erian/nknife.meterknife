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
    public class Modules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IMeterKernel>().As<MeterKernel>().SingleInstance();
            builder.RegisterType<BaseCareCommunicationService>().As<CareCommunicationService>().SingleInstance();
            builder.RegisterType<ITemperatureService>().As<CareTemperatureService>().SingleInstance();
        }
    }
}
