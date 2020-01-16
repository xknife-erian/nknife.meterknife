using System.Linq;
using NKnife.Tunnel.Filters;
using NKnife.Util;

namespace NKnife.Serial.Generic.Filters
{
    public class SerialProtocolFilter : BytesProtocolFilter
    {
        public SerialProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => SerialHelper.BytesCompare(item, command));
        }

        
    }
}