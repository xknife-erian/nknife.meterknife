using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Channels.Serials;

namespace MeterKnife.Cares
{
    public class CareOneSerialChannel : SerialChannel
    {
        public CareOneSerialChannel(SerialConfig serialConfig) 
            : base(serialConfig)
        {
        }
    }
}
