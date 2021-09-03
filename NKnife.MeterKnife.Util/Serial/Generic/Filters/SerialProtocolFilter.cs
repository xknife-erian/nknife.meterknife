using System.Linq;
using NKnife.MeterKnife.Util.Tunnel.Filters;

namespace NKnife.MeterKnife.Util.Serial.Generic.Filters
{
    public class SerialProtocolFilter : BytesProtocolFilter
    {
        public SerialProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => SerialHelper.BytesCompare(item, command));
        }
    }
}