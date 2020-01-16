using System.Collections;
using System.Collections.Generic;
using MeterKnife.Util.Tunnel;

namespace MeterKnife.Util.Socket.Generic
{
    public abstract class SocketConfig : ITunnelConfig
    {
        protected SocketConfig()
        {
            //默认值
            _Map.Add("ReceiveBufferSize", 10240);
            _Map.Add("SendBufferSize", 10240);
            _Map.Add("MaxBufferSize", 20480);
            _Map.Add("MaxConnectCount", 64);
            _Map.Add("ReceiveTimeout", 1200);
            _Map.Add("SendTimeout", 1200);
        }

        public void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int receiveBufferSize, int sendBufferSize)
        {
            ReceiveBufferSize = receiveBufferSize;
            SendBufferSize = sendBufferSize;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
        }

        public virtual int ReceiveBufferSize
        {
            get { return int.Parse(_Map["ReceiveBufferSize"].ToString()); }
            set
            {
                _Map["ReceiveBufferSize"] = value;
            }
        }

        public int SendBufferSize
        {
            get { return int.Parse(_Map["SendBufferSize"].ToString()); }
            set
            {
                _Map["SendBufferSize"] = value;
            }
        }

        public virtual int MaxBufferSize
        {
            get { return int.Parse(_Map["MaxBufferSize"].ToString()); }
            set
            {
                _Map["MaxBufferSize"] = value;
            }
        }

        public virtual int MaxConnectCount
        {
            get { return int.Parse(_Map["MaxConnectCount"].ToString()); }
            set
            {
                _Map["MaxConnectCount"] = value;
            }
        }

        public virtual int ReceiveTimeout
        {
            get { return int.Parse(_Map["ReceiveTimeout"].ToString()); }
            set
            {
                _Map["ReceiveTimeout"] = value;
            }
        }

        public virtual int SendTimeout
        {
            get { return int.Parse(_Map["SendTimeout"].ToString()); }
            set
            {
                _Map["SendTimeout"] = value;
            }
        }

        #region IDictionary<string, object>

        protected IDictionary<string, object> _Map = new Dictionary<string, object>(5);

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _Map.Add(item);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _Map.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _Map.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _Map.Remove(item);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return _Map.IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            _Map.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _Map.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _Map.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return _Map[key]; }
            set { _Map[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _Map.Keys; }
        }

        public ICollection<object> Values
        {
            get { return _Map.Values; }
        }

        #endregion
    }
}