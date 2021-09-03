using NKnife.MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareCodec : BytesCodec
    {
        public CareCodec(BytesDatagramDecoder decoder, BytesDatagramEncoder encoder)
            : base(decoder, encoder)
        {
            CodecName = CareProtocolFamily.FAMILY_NAME;
        }
    }
}
