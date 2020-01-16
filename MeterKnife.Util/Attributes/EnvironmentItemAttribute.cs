using System;

namespace MeterKnife.Util.Attributes
{
    /// <summary>应用程序启动时需加载的各种服务(环境)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class EnvironmentItemAttribute : Attribute, IEquatable<EnvironmentItemAttribute>, IComparable<EnvironmentItemAttribute>
    {
        public EnvironmentItemAttribute(int order, string description)
        {
            Order = order;
            Description = description;
        }

        /// <summary>启动的顺序，值越大将越先启动
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; private set; }

        /// <summary>服务的描述
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private set; }

        public int CompareTo(EnvironmentItemAttribute other)
        {
            return other.Order - Order;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EnvironmentItemAttribute) obj);
        }

        #region Equality members

        public bool Equals(EnvironmentItemAttribute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Order == other.Order;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ Order;
            }
        }

        public static bool operator ==(EnvironmentItemAttribute left, EnvironmentItemAttribute right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EnvironmentItemAttribute left, EnvironmentItemAttribute right)
        {
            return !Equals(left, right);
        }

        #endregion

    }
}