using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views.MenuItems;
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
            Bind<IAbout>().To<MyAbout>().InSingletonScope();
            Bind<IDropFunctionManager>().To<DropFunctionManager>().InSingletonScope();

            Bind<FileMenuItem>().ToSelf().InSingletonScope();
            Bind<MeasureMenuItem>().ToSelf().InSingletonScope();
            Bind<DataMenuItem>().ToSelf().InSingletonScope();
            Bind<ToolMenuItem>().ToSelf().InSingletonScope();
            Bind<ViewMenuItem>().ToSelf().InSingletonScope();
            Bind<HelpMenuItem>().ToSelf().InSingletonScope();

        }

        #endregion
    }

    public class MyAbout : About
    {
    }
}
