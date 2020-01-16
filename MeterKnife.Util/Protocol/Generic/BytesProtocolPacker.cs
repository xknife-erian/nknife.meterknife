namespace MeterKnife.Util.Protocol.Generic
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
