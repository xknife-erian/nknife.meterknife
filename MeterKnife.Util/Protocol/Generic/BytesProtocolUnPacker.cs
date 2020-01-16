namespace NKnife.Protocol.Generic
{
    public abstract class BytesProtocolUnPacker : IProtocolUnPacker<byte[]>
    {
        void IProtocolUnPacker<byte[]>.Execute(IProtocol<byte[]> protocol, byte[] data, byte[] command)
        {
            Execute((BytesProtocol)protocol, data, command);
        }

        public abstract void Execute(BytesProtocol protocol, byte[] data, byte[] command);
    }
}
