using Ninject.Activation;
using Ninject.Modules;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife.IoC
{
    public class DefaultModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ISocketServer>().To<KnifeSocketServer>();
            Bind<ISocketClient>().To<KnifeLongSocketClient>();

            Bind<SocketConfig>().To<SocketServerConfig>().Named("Server");
            Bind<SocketConfig>().To<SocketClientConfig>().Named("Client");

            Bind<SocketSessionMap>().To<SocketSessionMap>().When(Request);
            Bind<SocketSession>().To<SocketSession>().When(Request);

            Bind<StringProtocol>().To<StringProtocol>().When(Request);
            Bind<StringProtocolFamily>().To<StringProtocolFamily>().When(Request);
        }

        private bool Request(IRequest request)
        {
            return request.IsUnique;
        }
    }
}