using System.Collections;
using System.Collections.Generic;

namespace NKnife.MeterKnife.Util.Scpi
{
    /// <summary>
    /// 仪器命令队列的字典。Key是队列名，Value是命令封装的数组。
    /// </summary>
    public class ScpiCommandQueueMap : IDictionary<string, ScpiCommandQueue.Item[]>
    {
        private readonly Dictionary<string, ScpiCommandQueue.Item[]> _items = new Dictionary<string, ScpiCommandQueue.Item[]>();

        public IEnumerator<KeyValuePair<string, ScpiCommandQueue.Item[]>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            return ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, ScpiCommandQueue.Item[]>[] array, int arrayIndex)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            return ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Remove(item);
        }

        public int Count => _items.Count;

        public bool IsReadOnly => ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).IsReadOnly;

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public void Add(string key, ScpiCommandQueue.Item[] value)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Add(key, value);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public bool TryGetValue(string key, out ScpiCommandQueue.Item[] value)
        {
            return _items.TryGetValue(key, out value);
        }

        public ScpiCommandQueue.Item[] this[string key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public ICollection<string> Keys => ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Keys;

        public ICollection<ScpiCommandQueue.Item[]> Values => ((IDictionary<string, ScpiCommandQueue.Item[]>) _items).Values;
    }
}