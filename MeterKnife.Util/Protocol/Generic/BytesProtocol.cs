using System.Collections.Generic;
using NKnife.IoC;

namespace NKnife.Protocol.Generic
{
    public class BytesProtocol : IProtocol<byte[]>
    {
        public BytesProtocol()
            : this(string.Empty, new byte[]{})
        {
        }

        protected BytesProtocol(string family, byte[] command)
        {
            Infomations = new Dictionary<string, byte[]>(0);
            Tags = new List<object>(0);
            Family = family;
            Command = command;
        }


        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        public byte[] Command { get; set; }

        public byte[] CommandParam { get; set; }

        public Dictionary<string, byte[]> Infomations { get; private set; }

        public List<object> Tags { get; set; }

        public virtual BytesProtocol NewInstance()
        {
            var protocol = DI.Get<BytesProtocol>();
            protocol.Family = Family;
            protocol.Command = Command;
            return protocol;
        }

        IProtocol<byte[]> IProtocol<byte[]>.NewInstance()
        {
            return NewInstance();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BytesProtocol)obj);
        }

        protected bool Equals(BytesProtocol other)
        {
            return string.Equals(Family, other.Family) &&
                   Equals(Command, other.Command) &&
                   Equals(CommandParam, other.CommandParam) &&
                   Equals(Tags, other.Tags) &&
                   Equals(Infomations, other.Infomations);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Command != null ? Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Family != null ? Family.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
