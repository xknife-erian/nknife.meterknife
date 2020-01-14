using System;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientFilter : KnifeSocketFilter, ISocketClientFilter
    {
        public Func<KnifeSocketSession> SessionGetter { get; protected set; }

        public void Bind(Func<KnifeSocketSession> sessionGetter)
        {
            SessionGetter = sessionGetter;
            OnBoundGetter();
        }

        public event EventHandler<ConnectingEventArgs> Connecting;

        public event EventHandler<ConnectedEventArgs> Connected;

        public event EventHandler<ConnectionBrokenEventArgs> ConnectionBroken;

        protected internal virtual void OnConnecting(ConnectingEventArgs e)
        {
            EventHandler<ConnectingEventArgs> handler = Connecting;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnConnected(ConnectedEventArgs e)
        {
            EventHandler<ConnectedEventArgs> handler = Connected;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnConnectionBroken(ConnectionBrokenEventArgs e)
        {
            EventHandler<ConnectionBrokenEventArgs> handler = ConnectionBroken;
            if (handler != null)
                handler(this, e);
        }
    }
}