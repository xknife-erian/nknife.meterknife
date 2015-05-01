using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.Tunnels;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;

namespace MeterKnife.Common.Base
{
    public abstract class BaseCareCommunicationService : ITunnelService<byte[]>
    {
        void ITunnelService<byte[]>.Build(int port, params BaseProtocolHandler<byte[]>[] handlers)
        {
            Build(port, handlers.Cast<CareOneProtocolHandler>().ToArray());
        }

        public abstract void Build(int port, params CareOneProtocolHandler[] handlers);

        public abstract void Destroy(int port);
        public abstract bool Start(int port);
        public abstract bool Stop(int port);
        public abstract void Send(int port, byte[] data);
    }
}
