using NKnife.MeterKnife.Util.Protocol.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareProtocolFamily : BytesProtocolFamily
    {
        public CareProtocolFamily(BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker) 
            : base(bytesProtocolCommandParser, bytesProtocol, bytesProtocolUnPacker, bytesProtocolPacker)
        {
        }

        public CareProtocolFamily(string name, BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker) 
            : base(name, bytesProtocolCommandParser, bytesProtocol, bytesProtocolUnPacker, bytesProtocolPacker)
        {
        }
    }
}
