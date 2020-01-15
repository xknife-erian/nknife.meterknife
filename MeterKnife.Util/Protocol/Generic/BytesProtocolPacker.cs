using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Protocol.Generic
{
    public abstract class BytesProtocolPacker : IProtocolPacker<byte[]>
    {
        byte[] IProtocolPacker<byte[]>.Combine(IProtocol<byte[]> content)
        {
            return Combine((BytesProtocol)content);
        }

        public abstract byte[] Combine(BytesProtocol content);
    }
}
