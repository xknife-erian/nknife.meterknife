using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Channels.Channels.Serials;

namespace MeterKnife.Cares
{
    public class CareOneChannel : SerialChannel
    {
        public CareOneChannel(SerialConfig serialConfig) 
            : base(serialConfig)
        {
        }
    }
}
