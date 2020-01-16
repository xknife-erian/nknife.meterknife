using MeterKnife.Util.Tunnel.Common;
using NKnife.Events;

namespace MeterKnife.Util.Tunnel.Events
{
    public class SessionEventArgs : EventArgs<TunnelSession>
    {
        public SessionEventArgs(TunnelSession session)
            : base(session)
        {
        }
    }
}