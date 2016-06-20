using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Activation;
using Ninject.Modules;
using NKnife.IoC;

namespace MeterKnife.Instruments.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<DigitMultiMeterView>().To<DigitMultiMeterView>().When(Request);
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}
