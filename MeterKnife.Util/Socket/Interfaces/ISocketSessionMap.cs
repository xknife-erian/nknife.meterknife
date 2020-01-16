using System;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Tunnel;
using NKnife.Events;

namespace MeterKnife.Util.Socket.Interfaces
{
    public interface ISocketSessionMap : ITunnelSessionMap
    {
        event EventHandler<EventArgs<long>> Removed;

        event EventHandler<EventArgs<SocketSession>> Added;
    }
}