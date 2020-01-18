using MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.CareOne
{
    public class CareOneCodec : BytesCodec
    {
        public CareOneCodec(BytesDatagramDecoder decoder, BytesDatagramEncoder encoder)
            : base(decoder, encoder)
        {
        }
    }
}
