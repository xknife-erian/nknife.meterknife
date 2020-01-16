using System.Linq;
using MeterKnife.Util.Tunnel.Filters;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    public class SerialProtocolFilter : BytesProtocolFilter
    {
        public SerialProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => SerialHelper.BytesCompare(item, command));
        }

        
    }
}