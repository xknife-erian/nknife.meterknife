using System;
using System.Net;
using SocketKnife.Common;

namespace SocketKnife.Events
{
    public class ConnectionBrokenEventArgs : EventArgs
    {
        public ConnectionBrokenEventArgs(EndPoint endPoint, BrokenCause brokenCause)
        {
            EndPoint = endPoint;
            BrokenCause = brokenCause;
        }

        public EndPoint EndPoint { get; set; }
        public BrokenCause BrokenCause { get; set; }
    }
}