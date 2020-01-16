using System;
using MeterKnife.Util.Events;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.Util.Socket.Interfaces
{
    public interface ISocketSessionMap : ITunnelSessionMap
    {
        event EventHandler<EventArgs<long>> Removed;

        event EventHandler<EventArgs<SocketSession>> Added;
    }
}