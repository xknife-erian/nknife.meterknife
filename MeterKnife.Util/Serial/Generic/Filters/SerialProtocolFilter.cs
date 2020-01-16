using System.Linq;
using MeterKnife.Util.Tunnel.Filters;
using MeterKnife.Util.Utility;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    public class SerialProtocolFilter : BytesProtocolFilter
    {
        public SerialProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => UtilityByte.Compare(item, command));
        }
    }
}