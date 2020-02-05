using NKnife.Events;
using NKnife.MeterKnife.Util.Tunnel.Common;

namespace NKnife.MeterKnife.Util.Tunnel.Events
{
    public class SessionEventArgs : EventArgs<TunnelSession>
    {
        public SessionEventArgs(TunnelSession session)
            : base(session)
        {
        }
    }
}