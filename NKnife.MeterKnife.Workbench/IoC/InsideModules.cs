using Autofac;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Debugs;
using NKnife.MeterKnife.Workbench.Menus;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.IoC
{
    public class InsideModules : Module
    {
        #region Overrides of Module

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TrayMenuStrip>().AsSelf().SingleInstance();

            builder.RegisterType<AppManager>().As<IAppManager>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<DialogProvider>().As<IDialogProvider>().SingleInstance();

            builder.RegisterType<Workbench>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterType<DataMenuItem>().AsSelf().SingleInstance();
            builder.RegisterType<MeasureMenuItem>().AsSelf().SingleInstance();

            builder.RegisterType<DebuggerManager>().AsSelf().SingleInstance();

            var assembly = typeof(Workbench).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("Dialog"))
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("View"))
                .AsImplementedInterfaces()
                .AsSelf();
            assembly = typeof(ViewModelHelper).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsAbstract && t.Name.EndsWith("ViewModel"))
                .AsImplementedInterfaces()
                .AsSelf();
        }

        #endregion
    }
}