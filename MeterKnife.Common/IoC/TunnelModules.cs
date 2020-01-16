using Autofac;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Serial;
using MeterKnife.Util.Socket;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Common;
using MeterKnife.Util.Tunnel.Generic;

namespace MeterKnife.Common.IoC
{
    public class TunnelModules : Module
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
            builder.RegisterType<ITunnel>().As<KnifeTunnel>().SingleInstance();

            builder.RegisterType<IDataConnector>().Named<SerialPortDataConnector>("Serial");
            builder.RegisterType<IDataConnector>().Named<KnifeLongSocketClient>("Tcpip");

            builder.RegisterType<BytesCodec>().As<CareOneCodec>();
            builder.RegisterType<BytesProtocolCommandParser>().As<CareOneProtocolCommandParser>().SingleInstance();
            builder.RegisterType<BytesDatagramDecoder>().Named<CareOneDatagramDecoder>("careone").SingleInstance();
            builder.RegisterType<BytesDatagramEncoder>().Named<CareOneDatagramEncoder>("careone").SingleInstance();
            builder.RegisterType<BytesProtocol>().As<CareTalking>();

            builder.RegisterType<BytesProtocolPacker>().As<CareOneProtocolPacker>().SingleInstance();
            builder.RegisterType<BytesProtocolUnPacker>().As<CareOneProtocolUnPacker>().SingleInstance();
        }

        #endregion

    }
}