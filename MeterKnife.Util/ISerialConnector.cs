using MeterKnife.Util.Serial.Common;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.Util
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        bool IsInitialized { get; set; }

        SerialConfig SerialConfig { get; set; }
    }
}