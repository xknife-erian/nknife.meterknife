using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Instruments.Agilent;
using Ninject.Modules;

namespace MeterKnife.Instruments.IoC
{
    public class MeterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<BaseMeter>().To<Ag34401A>().Named("34401A");
        }
    }
}
