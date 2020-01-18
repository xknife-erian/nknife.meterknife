﻿using Autofac;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Logic;
using NKnife.MeterKnife.Logic.Services;

namespace NKnife.MeterKnife.CLI.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logic.Global>().As<IGlobal>().SingleInstance();

            builder.RegisterType<CareService>().As<BaseSlotService>();
            builder.RegisterType<CareTemperatureGetter>().As<ITemperatureGetter>().SingleInstance();
        }
    }
}
