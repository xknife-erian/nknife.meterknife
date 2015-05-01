using MeterKnife.Kernel.Interfaces;
using MeterKnife.Kernel.Tunnels;
using MeterKnife.Kernel.Tunnels.CareOne;
using MonitorKnife.Common.DataModels;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Generic;
using SerialKnife;
using SerialKnife.Interfaces;

namespace MeterKnife.Kernel.IoC
{
    public class TunnelModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerialConnector>().To<SerialPortDataConnector>();

            Bind<BytesCodec>().To<CareOneCodec>();
            Bind<BytesProtocolCommandParser>().To<CareOneProtocolCommandParser>().InSingletonScope();
            Bind<BytesDatagramDecoder>().To<CareOneDatagramDecoder>().InSingletonScope().Named("careone");
            Bind<BytesDatagramEncoder>().To<CareOneDatagramEncoder>().InSingletonScope().Named("careone");
            Bind<BytesProtocol>().To<CareSaying>();

            Bind<BytesProtocolPacker>().To<CareOneProtocolPacker>().InSingletonScope();
            Bind<BytesProtocolUnPacker>().To<CareOneProtocolUnPacker>().InSingletonScope();

            Bind<ITunnelService>().To<TunnelService>().InSingletonScope();
        }
    }
}