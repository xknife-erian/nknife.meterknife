using System.Linq;
using NKnife.Serial;
using NKnife.Tunnel.Filters;
using NKnife.Util;

namespace NKnife.Socket.Generic.Filters
{
    public class SocketBytesProtocolFilter : BytesProtocolFilter
    {
        public SocketBytesProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => SerialHelper.BytesCompare(item, command));
        }
    }
}