using System.Linq;
using NKnife.Tunnel.Filters;
using NKnife.Utility;

namespace SocketKnife.Generic.Filters
{
    public class SocketBytesProtocolFilter : BytesProtocolFilter
    {
        public SocketBytesProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => UtilityByte.Compare(item, command));
        }
    }
}