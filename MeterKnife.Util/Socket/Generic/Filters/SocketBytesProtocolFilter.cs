using System.Linq;
using MeterKnife.Util.Tunnel.Filters;
using MeterKnife.Util.Utility;

namespace MeterKnife.Util.Socket.Generic.Filters
{
    public class SocketBytesProtocolFilter : BytesProtocolFilter
    {
        public SocketBytesProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => UtilityByte.Compare(item, command));
        }
    }
}