namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolUnPacker : IProtocolUnPacker<string>
    {
        void IProtocolUnPacker<string>.Execute(IProtocol<string> protocol, string data,string command)
        {
            Execute((StringProtocol)protocol, data,command);
        }

        public abstract void Execute(StringProtocol protocol, string data,string command);
    }
}