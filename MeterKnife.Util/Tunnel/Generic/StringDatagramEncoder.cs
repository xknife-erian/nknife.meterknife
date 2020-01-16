using NKnife.Tunnel.Base;

namespace NKnife.Tunnel.Generic
{
    public abstract class StringDatagramEncoder : BaseDatagramEncoder<string>
    {
        public abstract override byte[] Execute(string replay);
    }
}