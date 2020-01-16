using Autofac;
using MeterKnife.Util.Protocol.Generic;
using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Serial.Interfaces;
using MeterKnife.Util.Serial.Wrappers;
using MeterKnife.Util.Socket;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Socket.Interfaces;

namespace MeterKnife.Util.IoC
{
    public class DefaultModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ISocketServer>().As<KnifeSocketServer>();
            builder.RegisterType<ISocketClient>().As<KnifeLongSocketClient>();

            builder.RegisterType<SocketConfig>().Named<SocketServerConfig>("Server");
            builder.RegisterType<SocketConfig>().Named<SocketClientConfig>("Client");

            builder.RegisterType<SocketSessionMap>().As<SocketSessionMap>();
            builder.RegisterType<SocketSession>().As<SocketSession>();

            builder.RegisterType<StringProtocol>().As<StringProtocol>();
            builder.RegisterType<StringProtocolFamily>().As<StringProtocolFamily>();

            builder.RegisterType<ISerialPortWrapper>().Named<SerialPortWrapperDotNet>(SerialType.DotNet.ToString());
            builder.RegisterType<ISerialPortWrapper>().Named<SerialPortWrapperWinApi>(SerialType.WinApi.ToString());
        }
    }
}