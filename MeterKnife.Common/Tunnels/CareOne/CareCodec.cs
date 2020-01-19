using NKnife.MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.CareOne
{
    public class CareCodec : BytesCodec
    {
        public CareCodec(BytesDatagramDecoder decoder, BytesDatagramEncoder encoder)
            : base(decoder, encoder)
        {
        }
    }
}
