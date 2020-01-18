using System;
using NKnife.MeterKnife.Util.Electronics.Collections;

namespace NKnife.MeterKnife.Util.Electronics
{
    /// <summary>描述一个电阻
    /// </summary>
    public class Resistance : ICloneable, IComparable, IComparable<Resistance>
    {
        /// <summary>电阻的阻值，单位欧姆(Ω)。
        /// </summary>
        public double Value { get; set; }

        /// <summary>电阻值的允许偏差
        /// </summary>
        public float Tolerance { get; set; }

        /// <summary>电阻的额定功率
        /// </summary>
        public float RatedPower { get; set; }

        /// <summary>电阻的最大工作电压
        /// </summary>
        public float MaximalVoltage { get; set; }

        /// <summary>当前实例电阻可分解为的电阻的组合
        /// </summary>
        public Resistances Group { get; private set; }

        /// <summary>创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>
        ///     作为此实例副本的新对象。
        /// </returns>
        public object Clone()
        {
            var newObj = new Resistance
            {
                Value = Value,
                Tolerance = Tolerance, 
                RatedPower = RatedPower,
                MaximalVoltage = MaximalVoltage,
                Group = Group
            };
            return newObj;
        }

        /// <summary>将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
        /// </summary>
        /// <param name="obj">与此实例进行比较的对象。</param>
        /// <returns>
        ///     一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此实例小于 <paramref name="obj" />。零此实例等于 <paramref name="obj" />。大于零此实例大于
        ///     <paramref
        ///         name="obj" />
        ///     。
        /// </returns>
        /// <exception cref="System.ArgumentException">参数类型错误.</exception>
        public int CompareTo(object obj)
        {
            if (!(obj is Resistance))
                throw new ArgumentException("参数类型错误.");
            var o = (Resistance) obj;
            return CompareTo(o);
        }

        /// <summary>将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(Resistance other)
        {
            return (int)(other.Value - Value);
        }

        /// <summary>Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(char flag)
        {
            switch (flag)
            {
                case 'O': //Ω
                case 'K':
                case 'M':
                default:
                    return ToString();
            }
        }
    }
}