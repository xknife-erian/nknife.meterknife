using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using NKnife.Events;
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

        public abstract bool Initialize();

        public event EventHandler<EventArgs<int>> SerialInitialized;

        protected virtual void OnSerialInitialized(EventArgs<int> e)
        {
            EventHandler<EventArgs<int>> handler = SerialInitialized;
            if (handler != null)
                handler(this, e);
        }

        public abstract void Build(int port, params CareOneProtocolHandler[] handlers);

        public abstract void Destroy();
        public abstract bool Start(int port);
        public abstract bool Stop(int port);
        public abstract void Send(int port, byte[] data);

        public abstract Dictionary<int, CareOneProtocolHandler> CareHandlers { get; }
    }
}
