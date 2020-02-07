using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench.IoC
{
    public class InsideModules : Module
    {
        #region Overrides of Module

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AppManager>().As<IAppManager>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<TrayMenuStrip>().AsSelf().SingleInstance();
            builder.RegisterType<AppTrayService>().As<IAppTrayService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<Workbench>().As<IWorkbench>().SingleInstance();
        }

        #endregion
    }
}
