using System;
using NKnife.Events;
using NKnife.Socket.Generic;
using NKnife.Tunnel;

namespace NKnife.Socket.Interfaces
{
    public interface ISocketSessionMap : ITunnelSessionMap
    {
        event EventHandler<EventArgs<long>> Removed;

        event EventHandler<EventArgs<SocketSession>> Added;
    }
}