using NKnife.Events;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs : EventArgs<TunnelSession>
    {
        public SessionEventArgs(TunnelSession session)
            : base(session)
        {
        }
    }
}