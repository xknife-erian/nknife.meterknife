using NKnife.Util;

namespace NKnife.MeterKnife.Util.Tunnel.Common
{
    /// <summary>
    ///     携带数据的封装。数据类型为byte[]。该封装在Handler和Filter里被循环处理。
    ///     如果SessionId的类型可以为EndPoint，可用于socket, http等网络协议，
    ///     如果SessionId的类型为int,可用于Serial Port等串口协议,用int串口号做标记
    /// </summary>
    public class TunnelSession : ITunnelSession
    {
        /// <summary>
        ///     如果SessionId的类型可以为EndPoint，可用于socket, http等网络协议，
        ///     如果SessionId的类型为int,可用于Serial Port等串口协议,用int串口号做标记
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     这次数据产生的发生源，即发送的数据。问什么，答什么……
        /// </summary>
        public byte[] Source { get; set; }

        /// <summary>
        ///     原始数据。内容在原始状态时所使用的数据形式。
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        ///     本次通讯的关联
        /// </summary>
        public string Relation { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 17) ^ Id.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            var o = ((TunnelSession) obj);
            return Id == o.Id && Relation.Equals(o.Relation);//TODO:
        }
    }
}