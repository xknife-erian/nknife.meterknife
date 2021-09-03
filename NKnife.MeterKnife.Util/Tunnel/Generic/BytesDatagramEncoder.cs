using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Util.Tunnel.Generic
{
    public abstract class BytesDatagramEncoder : BaseDatagramEncoder<byte[]>
    {
        public abstract override byte[] Execute(byte[] data);
    }
}
