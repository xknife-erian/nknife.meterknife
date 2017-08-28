using MeterKnife.Models;

namespace MeterKnife.Interfaces.Gateways
{
    public interface IGatewayDiscover
    {
        string GatewayName { get; set; }
        Instrument Instrument { get; }
    }
}
