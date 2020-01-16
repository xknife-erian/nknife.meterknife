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
            builder.RegisterType<KnifeTunnel>().As<ITunnel>().SingleInstance();

            builder.RegisterType<SerialPortDataConnector>().As<IDataConnector>();
            builder.RegisterType<SerialPortDataConnector>().Named<IDataConnector>("Serial");
            builder.RegisterType<KnifeLongSocketClient>().Named<IDataConnector>("Tcpip");

            builder.RegisterType<CareOneCodec>().As<BytesCodec>();
            builder.RegisterType<CareOneProtocolCommandParser>().As<BytesProtocolCommandParser>().SingleInstance();
            builder.RegisterType<CareOneDatagramDecoder>().As<BytesDatagramDecoder>().SingleInstance();
            builder.RegisterType<CareOneDatagramDecoder>().Named<BytesDatagramDecoder>("careone").SingleInstance();
            builder.RegisterType<CareOneDatagramEncoder>().As<BytesDatagramEncoder>().SingleInstance();
            builder.RegisterType<CareOneDatagramEncoder>().Named<BytesDatagramEncoder>("careone").SingleInstance();
            builder.RegisterType<CareTalking>().As<BytesProtocol>();

            builder.RegisterType<CareOneProtocolPacker>().As<BytesProtocolPacker>().SingleInstance();
            builder.RegisterType<CareOneProtocolUnPacker>().As<BytesProtocolUnPacker>().SingleInstance();

            builder.RegisterType<CareTemperatureHandler>().As<CareTemperatureHandler>().SingleInstance();
        }

        #endregion

    }
}