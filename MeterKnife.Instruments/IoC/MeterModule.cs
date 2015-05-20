using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Instruments.Common;
using MeterKnife.Instruments.Specified.Agilent;
using MeterKnife.Instruments.Specified.Keithley;
using Ninject.Activation;
using Ninject.Modules;

namespace MeterKnife.Instruments.IoC
{
    public class MeterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<BaseMeter>().To<ScpiMeter>().When(Request);
            Bind<BaseMeter>().To<Ag34401A>().Named("34401A");
            Bind<BaseMeter>().To<K2000>().Named("K2000");
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
