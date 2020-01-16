using System;
using System.Net;
using NKnife.Socket.Common;

namespace NKnife.Socket.Events
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