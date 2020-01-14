using NKnife.Tunnel;
using SerialKnife.Common;

namespace SerialKnife.Interfaces
{
    public interface ISerialConnector : IDataConnector
    {
        int PortNumber { get; set; }

        bool IsInitialized { get; set; }

        SerialType SerialType { get; set; }

        SerialConfig SerialConfig { get; set; }
    }
}