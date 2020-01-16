using Autofac;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Interfaces;
using MeterKnife.Util.Serial.Wrappers;
using MeterKnife.Util.Socket;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Socket.Interfaces;
using MeterKnife.Util.Tunnel.Generic;

namespace MeterKnife.Util.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KnifeSocketServer>().As<ISocketServer>();
            builder.RegisterType<KnifeLongSocketClient>().As<ISocketClient>();

            builder.RegisterType<SocketServerConfig>().Named<SocketConfig>("Server");
            builder.RegisterType<SocketClientConfig>().Named<SocketConfig>("Client");

            builder.RegisterType<SocketSessionMap>().AsSelf();
            builder.RegisterType<SocketSession>().AsSelf();

            builder.RegisterType<StringProtocol>().AsSelf();
            builder.RegisterType<StringProtocolFamily>().AsSelf();

            builder.RegisterType<SerialPortWrapperDotNet>().Named<ISerialPortWrapper>(SerialType.DotNet.ToString());
            builder.RegisterType<SerialPortWrapperWinApi>().Named<ISerialPortWrapper>(SerialType.WinApi.ToString());

            builder.RegisterType<BytesCodec>().SingleInstance();
            builder.RegisterType<BytesProtocolFamily>().SingleInstance();
        }
    }
}