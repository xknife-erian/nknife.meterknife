using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerFilter : KnifeSocketFilter, ISocketServerFilter
    {
        public Func<KnifeSocketSessionMap> SessionMapGetter { get; protected set; }

        public void Bind(Func<KnifeSocketSessionMap> sessionMapGetter)
        {
            SessionMapGetter = sessionMapGetter;
            OnBoundGetter();
        }

        public event EventHandler<SocketSessionEventArgs> ClientCome;

        public event EventHandler<ConnectionBrokenEventArgs> ClientBroken;

        protected internal virtual void OnClientCome(SocketSessionEventArgs e)
        {
            EventHandler<SocketSessionEventArgs> handler = ClientCome;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnClientBroken(ConnectionBrokenEventArgs e)
        {
            EventHandler<ConnectionBrokenEventArgs> handler = ClientBroken;
            if (handler != null)
                handler(this, e);
        }
    }
}
