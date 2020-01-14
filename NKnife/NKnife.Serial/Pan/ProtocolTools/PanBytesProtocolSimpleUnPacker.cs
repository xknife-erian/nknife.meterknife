using System;
using NKnife.Protocol.Generic;

namespace SerialKnife.Pan.ProtocolTools
{
    public class PanBytesProtocolSimpleUnPacker : BytesProtocolUnPacker
    {
        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            if (data == null)
                return;
            if (data.Length == 0)
                return;
            protocol.CommandParam = new byte[data.Length];
            Array.Copy(data, protocol.CommandParam, data.Length);
        }
    }
}
