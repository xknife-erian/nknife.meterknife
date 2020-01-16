using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace MeterKnife.Util.Base
{
    /// <summary>
    /// 一个存储了三个值（非键值对类型）的类型。
    /// 该结构重写了 == 和 != 操作符。
    /// Gean: 2009-08-24 22:30:51
    /// </summary>
    [Serializable]
    public class Triplet<TA, TB, TC> : ISerializable
    {
        public Triplet(TA first, TB second, TC third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public TA First { get; private set; }
        public TB Second { get; private set; }
        public TC Third { get; private set; }

        #region ISerializable Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("FirstValue", First);
            info.AddValue("SecondValue", Second);
            info.AddValue("ThirdValue", Third);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is DBNull) return false;
            if (obj is Triplet<TA, TB, TC>)
                return Equals((Triplet<TA, TB, TC>) obj);
            return false;
        }

        public bool Equals(Triplet<TA, TB, TC> other)
        {
            if (other == null) return false;
            return First.Equals(other.First) && Second.Equals(other.Second) && Third.Equals(other.Third);
        }

        public override int GetHashCode()
        {
            return unchecked(3*(First.GetHashCode() ^ Second.GetHashCode() ^ Third.GetHashCode()));
        }

        public override string ToString()
        {
            string str = string.Format("[First: {0}][Second: {1}][Third: {2}]", First, Second, Third);
            return str;
        }

        public static bool operator ==(Triplet<TA, TB, TC> lhs, Triplet<TA, TB, TC> rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(Triplet<TA, TB, TC> lhs, Triplet<TA, TB, TC> rhs)
        {
            return !Equals(lhs, rhs);
        }
    }
}