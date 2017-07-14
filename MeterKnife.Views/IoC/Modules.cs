using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;

namespace MeterKnife.Views.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IAbout>().To<MyAbout>();
        }

        #endregion
    }

    public class MyAbout : About
    {
    }
}
