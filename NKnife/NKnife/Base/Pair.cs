using System;
using System.Text;

namespace NKnife.Base
{
    /// <summary>
    /// 一个存储了一对值（非键值对类型）的类型。
    /// 移植自System.Data.Common.Utils的一个内部类。
    /// </summary>
    /// <typeparam name="TFirst">The type of the first.</typeparam>
    /// <typeparam name="TSecond">The type of the second.</typeparam>
    [Serializable]
    public struct Pair<TFirst, TSecond> : IEquatable<Pair<TFirst, TSecond>>
    {
        public static Pair<TFirst, TSecond> Build(TFirst first, TSecond second)
        {
            return new Pair<TFirst, TSecond>
                       {
                           First = first,
                           Second = second
                       };
        }

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <value>The first.</value>
        public TFirst First { get; set; }

        /// <summary>
        /// Gets the second.
        /// </summary>
        /// <value>The second.</value>
        public TSecond Second { get; set; }

        #region IEquatable<Pair<TFirst,TSecond>> Members

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Pair<TFirst, TSecond> other)
        {
            return Equals(other.First, First) && Equals(other.Second, Second);
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Pair<TFirst, TSecond> && Equals((Pair<TFirst, TSecond>) obj);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<");
            builder.Append(First.ToString());
            builder.Append(", " + Second);
            builder.Append(">");
            return builder.ToString();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (First.GetHashCode()*397) ^ Second.GetHashCode();
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
        {
            return !Equals(left, right);
        }
    }
}