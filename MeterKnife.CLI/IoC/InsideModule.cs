using Autofac;
using NKnife.MeterKnife.CLI.Commands;

namespace NKnife.MeterKnife.CLI.IoC
{
    public class InsideModule : Module
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
            builder.RegisterType<CareOneCliCommand>().AsSelf().SingleInstance();
        }

        #endregion
    }
}
