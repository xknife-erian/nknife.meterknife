using NKnife.Tunnel.Generic;

namespace MeterKnife.Kernel.Tunnels.CareOne
{
    public class CareOneDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}