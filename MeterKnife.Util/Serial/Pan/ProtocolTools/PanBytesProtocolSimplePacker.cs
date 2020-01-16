using System;
using MeterKnife.Util.Protocol.Generic;

namespace MeterKnife.Util.Serial.Pan.ProtocolTools
{
    public class PanBytesProtocolSimplePacker : BytesProtocolPacker
    {
        public override byte[] Combine(BytesProtocol content)
        {
            int len = content.Command.Length;
            if (content.CommandParam != null)
            {
                len += content.CommandParam.Length;
            }
            var result = new byte[len];
            Array.Copy(content.Command, 0, result, 0, content.Command.Length);
            if (content.CommandParam != null)
            {
                Array.Copy(content.CommandParam, 0, result, content.Command.Length, content.CommandParam.Length);
            }
            return result;
        }
    }
}
