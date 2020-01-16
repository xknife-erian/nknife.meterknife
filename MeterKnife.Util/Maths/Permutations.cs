using System;
using System.Collections;
using System.Collections.Generic;

namespace MeterKnife.Util.Maths
{
    /// <summary>
    ///     描述数学中的“排列”概念
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Permutations<T> : IEnumerable<IList<T>>
    {
        private readonly List<int[]> _Indices;
        private readonly int _Length;
        private readonly List<IList<T>> _Permutations;
        private int _Level = -1;
        private int[] _Value;

        public Permutations(IList<T> items)
            : this(items, items.Count)
        {
        }

        public Permutations(IList<T> items, int length)
        {
            _Length = length;
            _Permutations = new List<IList<T>>();
            _Indices = new List<int[]>();
            BuildIndices();
            foreach (var oneCom in new Combinations<T>(items, length))
            {
                _Permutations.AddRange(GetPermutations(oneCom));
            }
        }

        public int Count
        {
            get { return _Permutations.Count; }
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            return _Permutations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Permutations.GetEnumerator();
        }

        private void BuildIndices()
        {
            _Value = new int[_Length];
            Visit(0);
        }

        private void Visit(int k)
        {
            _Level += 1;
            _Value[k] = _Level;

            if (_Level == _Length)
            {
                _Indices.Add(_Value);
                var newValue = new int[_Length];
                Array.Copy(_Value, newValue, _Length);
                _Value = newValue;
            }
            else
            {
                for (int i = 0; i < _Length; i++)
                {
                    if (_Value[i] == 0)
                    {
                        Visit(i);
                    }
                }
            }

            _Level -= 1;
            _Value[k] = 0;
        }

        private IEnumerable<IList<T>> GetPermutations(IList<T> oneCom)
        {
            var t = new List<IList<T>>();

            foreach (var idxs in _Indices)
            {
                var onePerm = new T[_Length];
                for (int i = 0; i < _Length; i++)
                {
                    onePerm[i] = oneCom[idxs[i] - 1];
                }
                t.Add(onePerm);
            }

            return t;
        }
    }
}