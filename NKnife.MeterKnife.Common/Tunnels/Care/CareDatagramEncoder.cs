using NKnife.MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    public class CareDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}