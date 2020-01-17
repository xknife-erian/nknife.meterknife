using System.Linq;
using MeterKnife.Util.Protocol.Generic;

namespace MeterKnife.Util.Serial.Generic.Tools
{
    public class FirstByteCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            if (datagram != null && datagram.Any())
            {
                return new[] {datagram[0]};
            }
            return new byte[] {};
        }
    }
}
