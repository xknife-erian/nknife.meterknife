namespace MeterKnife.Util.Protocol.Generic
{
    public abstract class StringProtocolPacker : IProtocolPacker<string>
    {
        string IProtocolPacker<string>.Combine(IProtocol<string> content)
        {
            return Combine((StringProtocol) content);
        }

        public abstract string Combine(StringProtocol protocol);
    }
}
