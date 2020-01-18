using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Util.Protocol;
using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Common.Tunnels.CareOne
{
    public abstract class CareOneProtocolHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        protected CareOneProtocolHandler()
        {
            _Id = Guid.NewGuid();
            Commands = new List<byte[]>();
        }

        public sealed override List<byte[]> Commands { get; set; }

        public override void Received(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareTalking))
            {
                _Logger.Warn("Protocol类型有误, 不是CareSaying类型");
                return;
            }
            Received((CareTalking)protocol);
        }

        public abstract void Received(CareTalking protocol);

        private readonly Guid _Id;

        public override bool Equals(object obj)
        {
            if (obj == null) 
                return false;
            if (!(obj is CareOneProtocolHandler))
                return false;
            return Equals((CareOneProtocolHandler) obj);
        }

        protected bool Equals(CareOneProtocolHandler other)
        {
            return _Id.Equals(other._Id);
        }

        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }
    }
}