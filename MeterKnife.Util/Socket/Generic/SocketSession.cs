using System;
using NKnife.Tunnel.Common;

namespace NKnife.Socket.Generic
{
    /// <summary>
    ///     仅用于Socket协议
    /// </summary>
    public class SocketSession : TunnelSession
    {
        public SocketSession()
        {
            ReceiveBufferSize = 16 * 1024;
            ResetBuffer();
        }

        public int ReceiveBufferSize { get; set; }
        public System.Net.Sockets.Socket AcceptSocket { get; set; }
        public SessionState State { get; set; }
        public DisconnectType DisconnectType { get; set; }
        public byte[] ReceiveBuffer { get; set; }
        public DateTime LastSessionTime { get; set; }

        public void ResetBuffer()
        {
            ReceiveBuffer = new byte[ReceiveBufferSize];
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (Id.GetHashCode());
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SocketSession) obj);
        }
    }
}