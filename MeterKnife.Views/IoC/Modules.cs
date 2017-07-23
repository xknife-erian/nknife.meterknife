﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views.Menus;
using Ninject.Modules;
using NKnife.Interface;
using NKnife.Wrapper;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.IoC
{
    public class Modules : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            Bind<IAbout>().To<MyAbout>().InSingletonScope();
            Bind<IWorkbench>().To<Workbench>().InSingletonScope();

            Bind<DockPanel>().To<BenchDockPanel>().InSingletonScope();

            Bind<FileMenuItem>().ToSelf().InSingletonScope();
            Bind<MeasureMenuItem>().ToSelf().InSingletonScope();
            Bind<DataMenuItem>().ToSelf().InSingletonScope();
            Bind<ToolMenuItem>().ToSelf().InSingletonScope();
            Bind<ViewMenuItem>().ToSelf().InSingletonScope();
            Bind<HelpMenuItem>().ToSelf().InSingletonScope();
            Bind<IDropFunctionManager>().To<DropFunctionManager>().InSingletonScope();
        }

        #endregion
    }

    public class MyAbout : About
    {
    }
}
