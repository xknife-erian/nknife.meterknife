using Autofac;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.CareOne;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.Logic.IoC
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
            builder.RegisterType<CareTunnel>().As<ITunnel>();
            builder.RegisterType<SerialPortHold>().As<ISerialPortHold>();
            builder.RegisterType<CareOneDatagramDecoder>().As<IDatagramDecoder<byte[]>>().SingleInstance();

            builder.RegisterType<CareConfigHandler>().AsSelf().SingleInstance();
            builder.RegisterType<CareTemperatureHandler>().AsSelf().SingleInstance();
            builder.RegisterType<ScpiProtocolHandler>().AsSelf().SingleInstance();
        }

        #endregion
    }
}
