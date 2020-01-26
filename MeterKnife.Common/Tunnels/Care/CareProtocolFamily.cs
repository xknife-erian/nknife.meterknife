using NKnife.MeterKnife.Util.Protocol.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareProtocolFamily : BytesProtocolFamily
    {
        public const string FAMILY_NAME = "care";

        public CareProtocolFamily(BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker) 
            : base(bytesProtocolCommandParser, bytesProtocol, bytesProtocolUnPacker, bytesProtocolPacker)
        {
            FamilyName = FAMILY_NAME;
        }

        public CareProtocolFamily(string name, BytesProtocolCommandParser bytesProtocolCommandParser, BytesProtocol bytesProtocol, BytesProtocolUnPacker bytesProtocolUnPacker, BytesProtocolPacker bytesProtocolPacker) 
            : base(name, bytesProtocolCommandParser, bytesProtocol, bytesProtocolUnPacker, bytesProtocolPacker)
        {
            FamilyName = FAMILY_NAME;
        }
    }
}
