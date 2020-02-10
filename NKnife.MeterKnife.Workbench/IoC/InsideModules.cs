using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;

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

            builder.RegisterType<TrayMenuStrip>().AsSelf().SingleInstance();

            builder.RegisterType<AppManager>().As<IAppManager>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<Workbench>().As<IWorkbench>().SingleInstance();

            var assembly = typeof(Workbench).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && (t.Name.EndsWith("Dialog")))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && (t.Name.EndsWith("View")))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            assembly = typeof(ViewModelHelper).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && (t.Name.EndsWith("ViewModel")))
                .AsImplementedInterfaces()
                .AsSelf();
        }

        #endregion
    }
}
