using System;
using System.Net;
using NKnife.Protocol.Generic;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerProtocolHandler : KnifeSocketProtocolHandler
    {
        public virtual KnifeSocketSessionMap SessionMap { get; set; }

        public virtual void WriteAll(byte[] data)
        {
            foreach (var session in SessionMap.Values())
            {
                Write(session, data);
            }
        }

        public virtual void WriteAll(StringProtocol protocol)
        {
            foreach (var session in SessionMap.Values())
            {
                Write(session, protocol);
            }
        }

        /// <summary>
        /// Õë¶ÔEndPointµÄ·¢ËÍ
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="data"></param>
        public virtual void Write(EndPoint endPoint, byte[] data)
        {
            foreach (var session in SessionMap.Values())
            {
                if(session.Source.ToString().Equals(endPoint.ToString()))
                    Write(session, data);
            }
        }

        public virtual void Write(EndPoint endPoint, StringProtocol protocol)
        {
            foreach (var session in SessionMap.Values())
            {
                if (session.Source.ToString().Equals(endPoint.ToString()))
                    Write(session, protocol);
            }
        }
    }
}