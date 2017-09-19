using NKnife.Tunnel.Generic;

namespace MeterKnife.Cares.Protocols
{
    public class CareOneDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}