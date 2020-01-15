using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic;

namespace SocketKnife.Interfaces
{
    public interface ISocketServer : IDataConnector
    {
        SocketConfig Config { get; set; }
        void Configure(IPAddress ipAddress, int port);
    }
}