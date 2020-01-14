using System;
using System.Collections.Generic;
using System.Linq;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Filters;
using NKnife.Utility;

namespace SerialKnife.Generic.Filters
{
    public class SerialProtocolFilter : BytesProtocolFilter
    {
        public SerialProtocolFilter()
        {
            CommandCompareFunc = (list, command) => list.Any(item => UtilityByte.Compare(item, command));
        }
    }
}