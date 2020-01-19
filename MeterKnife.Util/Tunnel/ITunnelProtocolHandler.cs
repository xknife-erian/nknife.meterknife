using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Util.Protocol;
using NKnife.MeterKnife.Util.Tunnel.Events;

namespace NKnife.MeterKnife.Util.Tunnel
{
    /// <summary>
    /// Tunnel与Protocol的交互。
    /// 一般来讲，本接口的实现将会被ITunnelFilter调用。
    /// 并且一个Filter可以有多个Handler, 这时候不同Handler可以拥有它自有的Protocol集合的处理能力。
    /// </summary>
    /// <typeparam name="TData">Tunnel传递给Protocol的数据类型</typeparam>
    public interface ITunnelProtocolHandler<TData>
    {
        List<TData> Commands { get; set; }
        ITunnelCodec<TData> Codec { get; set; }
        void Received(long session, IProtocol<TData> protocol);

        event EventHandler<SessionEventArgs> SendToSession;

        event EventHandler<SessionEventArgs> SendToAll;
    }
}