using Autofac;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Common.Tunnels.Care;
using NKnife.MeterKnife.Common.Tunnels.Handlers;
using NKnife.MeterKnife.Util.Protocol.Generic;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Filters;
using NKnife.MeterKnife.Util.Tunnel.Generic;

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
            builder.RegisterType<SerialPortHold>().As<ISerialPortHold>();
            builder.RegisterType<SerialPortDataConnector>().As<IDataConnector>();
            
            builder.RegisterType<CareTunnel>().As<ITunnel>();
            builder.RegisterType<CareCodec>().As<BytesCodec>(); 
            builder.RegisterType<CareDatagramDecoder>().As<BytesDatagramDecoder>();
            builder.RegisterType<CareDatagramEncoder>().As<BytesDatagramEncoder>();
            builder.RegisterType<CareProtocolFilter>().As<BytesProtocolFilter>();
            builder.RegisterType<CareProtocolFamily>().As<BytesProtocolFamily>();
            builder.RegisterType<CareProtocolCommandParser>().As<BytesProtocolCommandParser>();
            builder.RegisterType<CareProtocolPacker>().As<BytesProtocolPacker>();
            builder.RegisterType<CareProtocolUnPacker>().As<BytesProtocolUnPacker>();

            builder.RegisterType<CareTalking>().As<BytesProtocol>();
            builder.RegisterType<CareConfigHandler>().AsSelf();
            builder.RegisterType<CareTemperatureHandler>().AsSelf();
            builder.RegisterType<DUTProtocolHandler>().AsSelf();
        }

        #endregion
    }
}
