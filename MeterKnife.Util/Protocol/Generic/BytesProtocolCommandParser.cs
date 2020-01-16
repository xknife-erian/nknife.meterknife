namespace NKnife.Protocol.Generic
{
    public abstract class BytesProtocolCommandParser : IProtocolCommandParser<byte[]>
    {
        public abstract byte[] GetCommand(byte[] datagram);
    }
}
