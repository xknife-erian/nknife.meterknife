using MeterKnife.Util.Events;
using MeterKnife.Util.Tunnel.Common;

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