using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MeterKnife.Electronics
{
    /// <summary>电阻对象的集合。
    /// 本集合实现了排序，当新的电阻对象被添加进集合后，集合会自动按阻值大小进行排序。
    /// 阻值大的电阻对象索引小，反之，索引大。
    /// </summary>
    public class Resistances : IList<Resistance>
    {
        private readonly List<Resistance> _resistances;

        public Resistances()
            : this(4, CircuitType.Series)
        {
        }

        public Resistances(int capacity, CircuitType circuitType)
        {
            _resistances = new List<Resistance>(capacity);
            CircuitType = circuitType;
        }

        /// <summary>当前集合中的电阻的电路类型
        /// </summary>
        public CircuitType CircuitType { get; set; }

        /// <summary>当前集合中的电阻的等效电阻值
        /// </summary>
        public double EquivalentValue
        {
            get
            {
                switch (CircuitType)
                {
                    case CircuitType.Series:
                        return SeriesEquivalentValue();
                    case CircuitType.Paralleling:
                        return ParallelingEquivalentValue();
                    default:
                        return 0;
                }
            }
        }

        /// <summary>并联的等效值
        /// </summary>
        private double ParallelingEquivalentValue()
        {
            //总电阻的倒数等于各分电阻的倒数之和
            double sum;
            if (_resistances.Count > 0)
                sum = _resistances.Sum(res => 1/res.Value);
            else
                return 0;
            return 1/sum;
        }

        /// <summary>串联的等效值
        /// </summary>
        private double SeriesEquivalentValue()
        {
            double sum = 0;
            if (_resistances.Count > 0)
                sum += _resistances.Sum(res => res.Value);
            return sum;
        }

        public IEnumerator<Resistance> GetEnumerator()
        {
            return _resistances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Resistance item)
        {
            _resistances.Add(item);
            _resistances.Sort();
        }

        public void Clear()
        {
            _resistances.Clear();
        }

        public bool Contains(Resistance item)
        {
            return _resistances.Contains(item);
        }

        public void CopyTo(Resistance[] array, int arrayIndex)
        {
            _resistances.CopyTo(array, arrayIndex);
            _resistances.Sort();
        }

        public bool Remove(Resistance item)
        {
            return _resistances.Remove(item);
        }

        public int Count
        {
            get { return _resistances.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(Resistance item)
        {
            return _resistances.IndexOf(item);
        }

        public void Insert(int index, Resistance item)
        {
            _resistances.Insert(index, item);
            _resistances.Sort();
        }

        public void RemoveAt(int index)
        {
            _resistances.RemoveAt(index);
        }

        public Resistance this[int index]
        {
            get { return _resistances[index]; }
            set
            {
                _resistances[index] = value;
                _resistances.Sort();
            }
        }
    }
}