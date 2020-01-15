using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Protocol.Generic
{
    public abstract class BytesProtocolCommandParser : IProtocolCommandParser<byte[]>
    {
        public abstract byte[] GetCommand(byte[] datagram);
    }
}
