using Autofac;
using NKnife.Protocol.Generic;
using NKnife.Socket.Generic;
using NKnife.Socket.Interfaces;

namespace NKnife.Socket.IoC
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
        }

    }
}