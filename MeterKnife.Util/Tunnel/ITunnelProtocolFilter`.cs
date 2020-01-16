using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolFilter<TData> : ITunnelFilter
    {
        void AddHandlers(params ITunnelProtocolHandler<TData>[] handlers);

        void RemoveHandler(ITunnelProtocolHandler<TData> handler);

        void Bind(ITunnelCodec<TData> codec, IProtocolFamily<TData> protocolFamily);
    }
}
