using System.Net;
using MeterKnife.Util.Socket.Generic;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.Util.Socket.Interfaces
{
    public interface ISocketClient : IDataConnector
    {
        SocketConfig Config { get; set; }
        void Configure(IPAddress ipAddress, int port);

    }
}