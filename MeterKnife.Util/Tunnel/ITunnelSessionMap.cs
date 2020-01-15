using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelSessionMap : IDictionary<long, ITunnelSession>
    {
    }
}
