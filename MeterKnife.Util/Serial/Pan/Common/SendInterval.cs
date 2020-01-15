using System;

namespace SerialKnife.Pan.Common
{
    /// <summary>发送的间隔
    /// 串口通讯通过发送间隔来控制数据收发时序，
    /// 一个数据包发送前准备时长和发送后超时等候时长均可控
    /// </summary>
    public class SendInterval : IEquatable<SendInterval>
    {
        /// <summary>发送前延迟准备时长
        /// </summary>
        public TimeSpan PreSendInterval { get; set; }

        /// <summary>发送后超时时长
        /// </summary>
        public TimeSpan ReadTimeoutInterval { get; set; }

        /// <summary>读取后等待时长
        /// </summary>
        public TimeSpan AfterReadInterval { get; set; }

        public SendInterval()
        {
            PreSendInterval = new TimeSpan(10);
            ReadTimeoutInterval = new TimeSpan(10);
            AfterReadInterval = new TimeSpan(10);
        }

        #region IEquatable<SendInterval> Members

        public bool Equals(SendInterval other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.PreSendInterval.Equals(PreSendInterval) && other.ReadTimeoutInterval.Equals(ReadTimeoutInterval);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("[Pre:{0},After:{1}]", PreSendInterval.Milliseconds, ReadTimeoutInterval.Milliseconds);
        }

        public override bool Equals(object obj)
        {
            return Equals((SendInterval) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PreSendInterval.GetHashCode()*397) ^ ReadTimeoutInterval.GetHashCode();
            }
        }

        public static bool operator ==(SendInterval left, SendInterval right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SendInterval left, SendInterval right)
        {
            return !Equals(left, right);
        }
    }
}