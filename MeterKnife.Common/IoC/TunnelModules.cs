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
using Ninject.Activation;
using Ninject.Modules;

namespace MeterKnife.Common.IoC
{
    public class TunnelModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITunnel>().To<KnifeTunnel>().When(Request);

            Bind<IDataConnector>().To<SerialPortDataConnector>().Named("Serial");
            Bind<IDataConnector>().To<KnifeLongSocketClient>().Named("Tcpip");

            Bind<BytesCodec>().To<CareOneCodec>();
            Bind<BytesProtocolCommandParser>().To<CareOneProtocolCommandParser>().InSingletonScope();
            Bind<BytesDatagramDecoder>().To<CareOneDatagramDecoder>().InSingletonScope().Named("careone");
            Bind<BytesDatagramEncoder>().To<CareOneDatagramEncoder>().InSingletonScope().Named("careone");
            Bind<BytesProtocol>().To<CareTalking>();

            Bind<BytesProtocolPacker>().To<CareOneProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<CareOneProtocolUnPacker>().InSingletonScope();
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }

    }
}