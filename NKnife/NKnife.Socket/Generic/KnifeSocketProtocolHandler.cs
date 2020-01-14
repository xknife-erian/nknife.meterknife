using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketProtocolHandler : IProtocolHandler<EndPoint, Socket, string>
    {

        protected KnifeSocketProtocolHandler()
        {
            _Id = Guid.NewGuid().ToString("N").ToUpper();
        }

        #region Equals

        private readonly string _Id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KnifeSocketProtocolHandler)obj);
        }

        protected bool Equals(KnifeSocketProtocolHandler other)
        {
            return string.Equals(_Id, other._Id);
        }

        public override int GetHashCode()
        {
            return (_Id != null ? _Id.GetHashCode() : 0);
        }

        #endregion

        protected Action<KnifeSocketSession, byte[]> _WriteBaseMethod;
        protected Action<KnifeSocketSession, StringProtocol> _WriteProtocolMethod;

        public List<string> Commands { get; set; }

        void IProtocolHandler<EndPoint, Socket, string>.Recevied(ITunnelSession<EndPoint, Socket> session, IProtocol<string> protocol)
        {
            Recevied((KnifeSocketSession)session, (StringProtocol)protocol);
        }

        void IProtocolHandler<EndPoint, Socket, string>.Write(ITunnelSession<EndPoint, Socket> session, byte[] data)
        {
            Write((KnifeSocketSession)session, data);
        }

        void IProtocolHandler<EndPoint, Socket, string>.Write(ITunnelSession<EndPoint, Socket> session, IProtocol<string> data)
        {
            Write((KnifeSocketSession)session, (StringProtocol)data);
        }

        public virtual void Bind(Action<KnifeSocketSession, byte[]> sendMethod)
        {
            _WriteBaseMethod = sendMethod;
        }

        public virtual void Bind(Action<KnifeSocketSession, StringProtocol> sendMethod)
        {
            _WriteProtocolMethod = sendMethod;
        }

        public abstract void Recevied(KnifeSocketSession session, StringProtocol protocol);

        public virtual void Write(KnifeSocketSession session, byte[] data)
        {
            _WriteBaseMethod.Invoke(session, data);
        }

        public virtual void Write(KnifeSocketSession session, StringProtocol data)
        {
            _WriteProtocolMethod.Invoke(session, data);
        }
    }
}