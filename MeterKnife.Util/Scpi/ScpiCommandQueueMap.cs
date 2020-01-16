using System.Collections;
using System.Collections.Generic;

namespace NKnife.Scpi
{
    public class ScpiCommandQueueMap : IDictionary<string, ScpiCommandQueue.Item[]>
    {
        protected Dictionary<string, ScpiCommandQueue.Item[]> _Itemses =
            new Dictionary<string, ScpiCommandQueue.Item[]>();

        public IEnumerator<KeyValuePair<string, ScpiCommandQueue.Item[]>> GetEnumerator()
        {
            return _Itemses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Add(item);
        }

        public void Clear()
        {
            _Itemses.Clear();
        }

        public bool Contains(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            return ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, ScpiCommandQueue.Item[]>[] array, int arrayIndex)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, ScpiCommandQueue.Item[]> item)
        {
            return ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Remove(item);
        }

        public int Count
        {
            get { return _Itemses.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Itemses.ContainsKey(key);
        }

        public void Add(string key, ScpiCommandQueue.Item[] value)
        {
            ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Add(key, value);
        }

        public bool Remove(string key)
        {
            return _Itemses.Remove(key);
        }

        public bool TryGetValue(string key, out ScpiCommandQueue.Item[] value)
        {
            return _Itemses.TryGetValue(key, out value);
        }

        public ScpiCommandQueue.Item[] this[string key]
        {
            get { return _Itemses[key]; }
            set { _Itemses[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Keys; }
        }

        public ICollection<ScpiCommandQueue.Item[]> Values
        {
            get { return ((IDictionary<string, ScpiCommandQueue.Item[]>) _Itemses).Values; }
        }
    }
}