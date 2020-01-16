using System;
using System.Net;
using MeterKnife.Util.Socket.Common;

namespace MeterKnife.Util.Socket.Events
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