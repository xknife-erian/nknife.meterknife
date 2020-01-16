using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MeterKnife.Common.Winforms.Dialogs;
using NKnife.WinTool.SerialProtocolDebugger.Views.Dialogs;

namespace MeterKnife.Common.Winforms.IoC
{
    public class DialogModule : Module
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
            builder.RegisterType<AboutDialog>().AsSelf();
            builder.RegisterType<AddMeterDialog>().AsSelf();
            builder.RegisterType<AddMeterLiteDialog>().AsSelf();
            builder.RegisterType<CareParameterDialog>().AsSelf();
            builder.RegisterType<CommandBuildDialog>().AsSelf();
            builder.RegisterType<DataPathSetterDialog>().AsSelf();
            builder.RegisterType<ExportFileSelectorDialog>().AsSelf();
            builder.RegisterType<ExportProgressDialog>().AsSelf();
            builder.RegisterType<FilterSettingDialog>().AsSelf();
            builder.RegisterType<InterfaceSelectorDialog>().AsSelf();
            builder.RegisterType<OfflineCollectParameterDialog>().AsSelf();
            builder.RegisterType<SerialPortSelectorDialog>().AsSelf();
        }

        #endregion
    }
}
