using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace MeterKnife.Cares.Protocols
{
    public class CareTalking : BytesProtocol
    {
        public short GpibAddress { get; set; }
        public byte[] ScpiBytes { get; set; }
        public string Scpi { get; set; }
    }
}
