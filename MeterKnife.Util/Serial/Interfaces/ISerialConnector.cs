using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.Util.Serial.Interfaces
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        bool IsInitialized { get; set; }

        SerialType SerialType { get; set; }

        SerialConfig SerialConfig { get; set; }
    }
}