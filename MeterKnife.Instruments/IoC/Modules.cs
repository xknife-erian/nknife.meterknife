using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace MeterKnife.Instruments.IoC
{
    public class Modules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DigitMultiMeterView>().As<DigitMultiMeterView>();
        }

    }
}
