using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolFilter<TData> : ITunnelFilter
    {
        void AddHandlers(params ITunnelProtocolHandler<TData>[] handlers);

        void RemoveHandler(ITunnelProtocolHandler<TData> handler);

        void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily);
    }
}
