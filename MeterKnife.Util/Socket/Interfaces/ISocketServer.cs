using System.Net;
using NKnife.Socket.Generic;
using NKnife.Tunnel;

namespace NKnife.Socket.Interfaces
{
    public interface ISocketServer : IDataConnector
    {
        SocketConfig Config { get; set; }
        void Configure(IPAddress ipAddress, int port);
    }
}