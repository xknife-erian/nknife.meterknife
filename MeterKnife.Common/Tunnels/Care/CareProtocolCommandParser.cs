using NKnife.MeterKnife.Util.Protocol.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareProtocolCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            return new[] {datagram[3], datagram[4]};
        }
    }
}