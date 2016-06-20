using NKnife.Tunnel.Generic;

namespace MeterKnife.Common.Tunnels.CareOne
{
    public class CareOneDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}