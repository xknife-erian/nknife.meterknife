using System.Collections.Generic;

namespace MeterKnife.Util.Maths
{
    /// <summary>
    /// 描述数学中的“组合”概念
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Combinations<T> : IEnumerable<IList<T>>
    {
        private readonly List<IList<T>> _Combinations;
        private readonly IList<T> _Items;
        private readonly int _Length;
        private readonly int[] _EndIndices;

        public Combinations(IList<T> itemList)
            : this(itemList, itemList.Count)
        {
        }

        public Combinations(IList<T> itemList, int length)
        {
            _Items = itemList;
            _Length = length;
            _Combinations = new List<IList<T>>();
            _EndIndices = new int[length];
            int j = length - 1;
            for (int i = _Items.Count - 1; i > _Items.Count - 1 - length; i--)
            {
                _EndIndices[j] = i;
                j--;
            }
            ComputeCombination();
        }

        private void ComputeCombination()
        {
            int[] indices = new int[_Length];
            for (int i = 0; i < _Length; i++)
            {
                indices[i] = i;
            }

            do
            {
                T[] oneCom = new T[_Length];
                for (int k = 0; k < _Length; k++)
                {
                    oneCom[k] = _Items[indices[k]];
                }
                _Combinations.Add(oneCom);
            }
            while (GetNext(indices));
        }

        private bool GetNext(int[] indices)
        {
            bool hasMore = true;

            for (int j = _EndIndices.Length - 1; j > -1; j--)
            {
                if (indices[j] < _EndIndices[j])
                {
                    indices[j]++;
                    for (int k = 1; j + k < _EndIndices.Length; k++)
                    {
                        indices[j + k] = indices[j] + k;
                    }
                    break;
                }
                else if (j == 0)
                {
                    hasMore = false;
                }
            }
            return hasMore;
        }

        public int Count
        {
            get { return _Combinations.Count; }
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            return _Combinations.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<IList<T>>)GetEnumerator();
        }
    }
}
