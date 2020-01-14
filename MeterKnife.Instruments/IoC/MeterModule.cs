using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Instruments.Specified;
using Ninject.Activation;
using Ninject.Modules;

namespace MeterKnife.Instruments.IoC
{
    public class MeterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<BaseMeter>().To<DigitalMultimeter>().Named("DigitalMultimeter".ToLower());
            Bind<BaseMeter>().To<DcPowerSupply>().Named("DcPowerSupply".ToLower());
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
