using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Instruments.Specified;

namespace MeterKnife.Instruments.IoC
{
    public class MeterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseMeter>().Named<DigitalMultimeter>("DigitalMultimeter".ToLower());
            builder.RegisterType<BaseMeter>().Named<DcPowerSupply>("DcPowerSupply".ToLower());
        }
    }
}
