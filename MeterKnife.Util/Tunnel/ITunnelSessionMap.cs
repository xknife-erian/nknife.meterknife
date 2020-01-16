using System.Collections.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelSessionMap : IDictionary<long, ITunnelSession>
    {
    }
}
