using System.Collections;
using System.Collections.Generic;

namespace NKnife.MeterKnife.Util.Scpi
{
    /// <summary>
    /// 仪器命令队列的字典。Key是队列名，Value是命令封装的数组。
    /// </summary>
    public class ScpiCommandQueueMap : IDictionary<string, CareCommand[]>
    {
        private readonly Dictionary<string, CareCommand[]> _items = new Dictionary<string, CareCommand[]>();

        public IEnumerator<KeyValuePair<string, CareCommand[]>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, CareCommand[]> item)
        {
            ((IDictionary<string, CareCommand[]>) _items).Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(KeyValuePair<string, CareCommand[]> item)
        {
            return ((IDictionary<string, CareCommand[]>) _items).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, CareCommand[]>[] array, int arrayIndex)
        {
            ((IDictionary<string, CareCommand[]>) _items).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, CareCommand[]> item)
        {
            return ((IDictionary<string, CareCommand[]>) _items).Remove(item);
        }

        public int Count => _items.Count;

        public bool IsReadOnly => ((IDictionary<string, CareCommand[]>) _items).IsReadOnly;

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public void Add(string key, CareCommand[] value)
        {
            ((IDictionary<string, CareCommand[]>) _items).Add(key, value);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public bool TryGetValue(string key, out CareCommand[] value)
        {
            return _items.TryGetValue(key, out value);
        }

        public CareCommand[] this[string key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public ICollection<string> Keys => ((IDictionary<string, CareCommand[]>) _items).Keys;

        public ICollection<CareCommand[]> Values => ((IDictionary<string, CareCommand[]>) _items).Values;
    }
}