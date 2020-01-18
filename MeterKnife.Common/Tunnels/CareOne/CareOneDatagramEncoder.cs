using MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.CareOne
{
    public class CareOneDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}