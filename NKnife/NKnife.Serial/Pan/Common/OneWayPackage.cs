using System;
using SerialKnife.Common;

namespace SerialKnife.Pan.Common
{
    /// <summary>只发送不接收信息包（单向）
    /// </summary>
    public class OneWayPackage : PackageBase, IEquatable<OneWayPackage>
    {
        public OneWayPackage(ushort port, byte[] dataToSend, SendInterval sendInterval)
            : base(port, dataToSend, sendInterval)
        {
        }

        #region IEquatable<OneWayPackage> Members，及相关重写

        public bool Equals(OneWayPackage other)
        {
            return other.Port.Equals(Port) && other.DataToSend.Equals(DataToSend);
        }

        public override bool Equals(object obj)
        {
            return Equals((OneWayPackage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Port.GetHashCode()*397) ^ DataToSend.GetHashCode();
            }
        }

        public static bool operator ==(OneWayPackage left, OneWayPackage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OneWayPackage left, OneWayPackage right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}