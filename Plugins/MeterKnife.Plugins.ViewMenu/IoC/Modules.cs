using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Plugins.ViewMenu.Loggers;
using Ninject.Modules;

namespace MeterKnife.Plugins.ViewMenu.IoC
{
    public class Modules : NinjectModule
    {
        public override void Load()
        {
            Bind<LoggerView>().ToSelf().InSingletonScope();
        }
    }
}
