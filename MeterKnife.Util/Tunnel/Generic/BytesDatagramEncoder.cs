using NKnife.Tunnel.Base;

namespace NKnife.Tunnel.Generic
{
    public abstract class BytesDatagramEncoder : BaseDatagramEncoder<byte[]>
    {
        public abstract override byte[] Execute(byte[] data);
    }
}
