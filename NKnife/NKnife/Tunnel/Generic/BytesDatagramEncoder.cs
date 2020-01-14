using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Generic
{
    public abstract class BytesDatagramEncoder : BaseDatagramEncoder<byte[]>
    {
        public abstract override byte[] Execute(byte[] data);
    }
}
