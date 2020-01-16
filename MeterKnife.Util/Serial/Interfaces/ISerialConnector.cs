using NKnife.Serial.Common;
using NKnife.Tunnel;

namespace NKnife.Serial.Interfaces
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        bool IsInitialized { get; set; }

        SerialType SerialType { get; set; }

        SerialConfig SerialConfig { get; set; }
    }
}