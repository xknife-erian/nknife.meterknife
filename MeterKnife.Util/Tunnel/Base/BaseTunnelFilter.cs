using System;
using MeterKnife.Util.Tunnel.Events;

namespace MeterKnife.Util.Tunnel.Base
{
    public abstract class BaseTunnelFilter : ITunnelFilter
    {
        public abstract bool ProcessReceiveData(ITunnelSession session);

        public abstract void ProcessSessionBroken(long id);

        public abstract void ProcessSessionBuilt(long id);

        public virtual void ProcessSendToSession(ITunnelSession session)
        {
            //默认啥也不干
        }

        public virtual void ProcessSendToAll(byte[] data)
        {
            //默认啥也不干
        }

        public event EventHandler<SessionEventArgs> SendToSession;
        public event EventHandler<SessionEventArgs> SendToAll;
        public event EventHandler<SessionEventArgs> KillSession;

        protected virtual void OnSendToSession(object sender, SessionEventArgs e)
        {
            var handler = SendToSession;
            if (handler != null)
                handler(this, e);
        }
        protected virtual void OnSendToAll(object sender, SessionEventArgs e)
        {
            var handler = SendToAll;
            if (handler != null)
                handler(this, e);
        }
        protected virtual void OnKillSession(object sender, SessionEventArgs e)
        {
            var handler = KillSession;
            if (handler != null)
                handler(this, e);
        }


    }
}
