using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Events;
using NKnife.Tunnel;
using SocketKnife.Generic;

namespace SocketKnife.Interfaces
{
    public interface ISocketSessionMap : ITunnelSessionMap
    {
        event EventHandler<EventArgs<long>> Removed;

        event EventHandler<EventArgs<SocketSession>> Added;
    }
}