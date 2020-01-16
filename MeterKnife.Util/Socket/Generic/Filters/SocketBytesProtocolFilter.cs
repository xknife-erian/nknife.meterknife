using System.Linq;
using MeterKnife.Util.Serial;
using MeterKnife.Util.Tunnel.Filters;

namespace MeterKnife.Util.Socket.Generic.Filters
{
    public class SocketBytesProtocolFilter : BytesProtocolFilter
    {
        public SocketBytesProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => SerialHelper.BytesCompare(item, command));
        }
    }
}