using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MeterKnife.Util.Collections
{
    /// <summary>描述一个可序列化，且集合的内容发生改变时会发出通知的KeyValue的集合
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class NotifySerializableMap<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged, IXmlSerializable, ISerializable, ICloneable
    {
        private readonly SerializableMap<TKey, TValue> _Map = new SerializableMap<TKey, TValue>();

        #region Implementation of INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _Map.Clear();
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(e);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + _Map.Count)
                Array.Resize(ref array, arrayIndex + _Map.Count);
            foreach (var pair in _Map)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] items)
        {
            foreach (var item in items)
                Add(item.Key, item.Value);
        }

        #endregion

        #region Implementation of IDictionary<TKey,TValue>

        public bool ContainsKey(TKey key)
        {
            return _Map.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (_Map.ContainsKey(key))
            {
                _Map[key] = value;
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value);
                OnCollectionChanged(e);
            }
            else
            {
                _Map.Add(key, value);
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value);
                OnCollectionChanged(e);
            }
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Remove(TKey key)
        {
            if (_Map.ContainsKey(key))
            {
                bool removeFlag = _Map.Remove(key);
                if (removeFlag)
                {
                    var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key);
                    OnCollectionChanged(e);
                    OnPropertyChanged("Count");
                    OnPropertyChanged("Item[]");
                }
                return removeFlag;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _Map.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _Map[key]; }
            set { Add(key, value); }
        }

        public ICollection<TKey> Keys
        {
            get { return _Map.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _Map.Values; }
        }

        #endregion

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return ((IXmlSerializable) _Map).GetSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            ((IXmlSerializable) _Map).ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            ((IXmlSerializable) _Map).WriteXml(writer);
        }

        #endregion

        #region Implementation of ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _Map).GetObjectData(info, context);
        }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            return _Map.Clone();
        }

        #endregion

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}