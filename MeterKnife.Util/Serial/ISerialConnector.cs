using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.Util.Serial
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        bool IsInitialized { get; set; }

        SerialConfig SerialConfig { get; set; }
    }
}