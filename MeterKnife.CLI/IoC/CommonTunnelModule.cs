using Autofac;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Util.Serial;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.CLI.IoC
{
    public class CommonTunnelModule : Module
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
            builder.RegisterType<SerialPortHold>().As<ISerialPortHold>();
            builder.RegisterType<CareOneDatagramDecoder>().As<IDatagramDecoder<byte[]>>().SingleInstance();
        }

        #endregion
    }
}
