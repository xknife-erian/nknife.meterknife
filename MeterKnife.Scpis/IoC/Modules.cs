using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace MeterKnife.Scpis.IoC
{
    public class Modules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IScpiInfoGetter>().As<ScpiInfoGetter>().SingleInstance();
        }
    }
}
