using System;
using System.Collections;
using System.Collections.Generic;

namespace MeterKnife.Util.Collections
{
    [Serializable]
    public class DictionaryOrdered<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Private Data members

        private readonly IList<TKey> _List;
        private readonly IDictionary<TKey, TValue> _Map;

        #endregion

        #region Constructors

        public DictionaryOrdered()
        {
            _List = new List<TKey>();
            _Map = new Dictionary<TKey, TValue>();
        }

        #endregion

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Add to key/value for both forward and reverse lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            // Add to map and list.
            _Map.Add(key, value);
            _List.Add(key);
        }

        /// <summary>
        /// Determine if the key is contain in the forward lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _Map.ContainsKey(key);
        }

        /// <summary>
        /// Get a list of all the keys in the forward lookup.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _Map.Keys; }
        }

        /// <summary>
        /// Remove the key from the ordered dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            // Check.
            if (!_Map.ContainsKey(key)) return false;

            int ndxKey = IndexOfKey(key);
            _Map.Remove(key);
            _List.RemoveAt(ndxKey);
            return true;
        }

        /// <summary>
        /// Try to get the value from the forward lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _Map.TryGetValue(key, out value);
        }

        /// <summary>
        /// Get the collection of values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _Map.Values; }
        }

        /// <summary>
        /// Set the key / value for bi-directional lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _Map[key]; }
            set { _Map[key] = value; }
        }

        /// <summary>
        /// Add to ordered lookup.
        /// </summary>
        /// <param name="item"></param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Clears keys/value for bi-directional lookup.
        /// </summary>
        public void Clear()
        {
            _Map.Clear();
            _List.Clear();
        }

        /// <summary>
        /// Determine if the item is in the forward lookup.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _Map.Contains(item);
        }

        /// <summary>
        /// Copies the array of key/value pairs for both ordered dictionary.
        /// TO_DO: This needs to implemented.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _Map.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get number of entries.
        /// </summary>
        public int Count
        {
            get { return _Map.Count; }
        }

        /// <summary>
        /// Get whether or not this is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _Map.IsReadOnly; }
        }

        /// <summary>
        /// Remove the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            // Check.
            if (!_Map.ContainsKey(item.Key)) return false;

            int ndxOfKey = IndexOfKey(item.Key);
            _List.RemoveAt(ndxOfKey);
            return _Map.Remove(item);
        }

        /// <summary>
        /// Get the enumerator for the forward lookup.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        /// <summary>
        /// Get the enumerator for the forward lookup.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        #endregion

        #region IList methods

        /// <summary>
        /// Get/set the value at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue this[int index]
        {
            get
            {
                TKey key = _List[index];
                return _Map[key];
            }
            set
            {
                TKey key = _List[index];
                _Map[key] = value;
            }
        }

        /// <summary>
        /// Insert key/value at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(int index, TKey key, TValue value)
        {
            // Add to map and list.
            _Map.Add(key, value);
            _List.Insert(index, key);
        }


        /// <summary>
        /// Get the index of the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int IndexOfKey(TKey key)
        {
            if (!_Map.ContainsKey(key)) return -1;

            for (int ndx = 0; ndx < _List.Count; ndx++)
            {
                TKey keyInList = _List[ndx];
                if (keyInList.Equals(key))
                    return ndx;
            }
            return -1;
        }


        /// <summary>
        /// Remove the key/value item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            TKey key = _List[index];
            _Map.Remove(key);
            _List.RemoveAt(index);
        }

        #endregion
    }
}