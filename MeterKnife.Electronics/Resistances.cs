using System.Collections;
using System.Collections.Generic;
using NKnife.Electronics;
using System.Linq;

namespace MeterKnife.Electronics
{
    /// <summary>电阻对象的集合。
    /// 本集合实现了排序，当新的电阻对象被添加进集合后，集合会自动按阻值大小进行排序。
    /// 阻值大的电阻对象索引小，反之，索引大。
    /// </summary>
    public class Resistances : IList<NKnife.Electronics.Resistance>
    {
        protected List<NKnife.Electronics.Resistance> _Resistances;

        public Resistances()
            : this(4, CircuitType.Series)
        {
        }

        public Resistances(int capacity, CircuitType circuitType)
        {
            _Resistances = new List<NKnife.Electronics.Resistance>(capacity);
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
            if (_Resistances.Count > 0)
                sum = _Resistances.Sum(res => 1/res.Value);
            else
                return 0;
            return 1/sum;
        }

        /// <summary>串联的等效值
        /// </summary>
        private double SeriesEquivalentValue()
        {
            double sum = 0;
            if (_Resistances.Count > 0)
                sum += _Resistances.Sum(res => res.Value);
            return sum;
        }

        public IEnumerator<NKnife.Electronics.Resistance> GetEnumerator()
        {
            return _Resistances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(NKnife.Electronics.Resistance item)
        {
            _Resistances.Add(item);
            _Resistances.Sort();
        }

        public void Clear()
        {
            _Resistances.Clear();
        }

        public bool Contains(NKnife.Electronics.Resistance item)
        {
            return _Resistances.Contains(item);
        }

        public void CopyTo(NKnife.Electronics.Resistance[] array, int arrayIndex)
        {
            _Resistances.CopyTo(array, arrayIndex);
            _Resistances.Sort();
        }

        public bool Remove(NKnife.Electronics.Resistance item)
        {
            return _Resistances.Remove(item);
        }

        public int Count
        {
            get { return _Resistances.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(NKnife.Electronics.Resistance item)
        {
            return _Resistances.IndexOf(item);
        }

        public void Insert(int index, NKnife.Electronics.Resistance item)
        {
            _Resistances.Insert(index, item);
            _Resistances.Sort();
        }

        public void RemoveAt(int index)
        {
            _Resistances.RemoveAt(index);
        }

        public NKnife.Electronics.Resistance this[int index]
        {
            get { return _Resistances[index]; }
            set
            {
                _Resistances[index] = value;
                _Resistances.Sort();
            }
        }
    }
}