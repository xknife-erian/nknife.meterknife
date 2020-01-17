using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Util.Scpi;
using NKnife.Util;
using NLog;

namespace MeterKnife.Common.DataModels
{
    /// <summary>
    ///     一个端口可以有多个指令组等待循环；多个指令组由指令组的字符串Key进行管理。
    ///     当在仪器监控面板中，每个面板会拥有一个唯一的字符串Key，这样，面板开始采信与停止采集只需添加和移除该Key所
    ///     映射的指令组即可
    /// </summary>
    public class CommPortCommandMap : IDictionary<CommPort, ScpiCommandQueueMap>
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private readonly object _Lock = new object();

        private List<List<ScpiCommandQueue.Item>> _CloneItems = new List<List<ScpiCommandQueue.Item>>();

        public ScpiCommandQueue.Item[] this[CommPort commPort, string key] => _Map[commPort][key];

        public void Add(CommPort commPort, string key, ScpiCommandQueue.Item[] careItems)
        {
            if (!_Map.TryGetValue(commPort, out var queueMap))
            {
                queueMap = new ScpiCommandQueueMap();
                _Map.Add(commPort, queueMap);
            }

            queueMap.Add(key, careItems);
        }

        public bool HasCommand(CommPort commPort)
        {
            var hasCommand = false;
            lock (_Lock)
            {
                hasCommand = _Map.ContainsKey(commPort) && _Map[commPort].Values.Count > 0;
            }

            return hasCommand;
        }

        public void Remove(CommPort commPort, string scpiGroupKey)
        {
            lock (_Lock)
            {
                if (!_Map.TryGetValue(commPort, out var map))
                    return;
                //根据指定端口的指令组的Key停止采集指令循环
                if (map.ContainsKey(scpiGroupKey))
                {
                    map.Remove(scpiGroupKey);
                    _CloneItems.Clear();
                }
            }
        }

        public IEnumerable<IEnumerable<ScpiCommandQueue.Item>> GetItems(CommPort commPort)
        {
            if (_CloneItems == null || _CloneItems.Count <= 0)
            {
                _CloneItems = new List<List<ScpiCommandQueue.Item>>();
                try
                {
                    var values = _Map[commPort].Values;
                    foreach (var careItems in values)
                    {
                        if (UtilCollection.IsNullOrEmpty(careItems))
                            continue;
                        var its = new List<ScpiCommandQueue.Item>();
                        foreach (var careItem in careItems) its.Add(careItem.Clone());
                        if (its.Count > 0)
                            _CloneItems.Add(its);
                    }
                }
                catch (Exception e)
                {
                    _Logger.Warn($"Clone集合异常:{e}");
                }
            }

            return _CloneItems;
        }

        public string[] GetScpiGroupKeys(CommPort commPort)
        {
            return _Map[commPort].Select(pair => pair.Key).ToArray();
        }

        public bool ContainsKey(CommPort commPort, string key)
        {
            return _Map.ContainsKey(commPort) && _Map[commPort].ContainsKey(key);
        }

        #region IDictionary

        protected readonly Dictionary<CommPort, ScpiCommandQueueMap> _Map = new Dictionary<CommPort, ScpiCommandQueueMap>();

        IEnumerator<KeyValuePair<CommPort, ScpiCommandQueueMap>> IEnumerable<KeyValuePair<CommPort, ScpiCommandQueueMap>>.GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        void ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.Add(KeyValuePair<CommPort, ScpiCommandQueueMap> item)
        {
            ((IDictionary<CommPort, ScpiCommandQueueMap>) _Map).Add(item);
        }

        void ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.Clear()
        {
            _Map.Clear();
        }

        bool ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.Contains(KeyValuePair<CommPort, ScpiCommandQueueMap> item)
        {
            return ((IDictionary<CommPort, ScpiCommandQueueMap>) _Map).Contains(item);
        }

        void ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.CopyTo(KeyValuePair<CommPort, ScpiCommandQueueMap>[] array, int arrayIndex)
        {
            ((IDictionary<CommPort, ScpiCommandQueueMap>) _Map).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.Remove(KeyValuePair<CommPort, ScpiCommandQueueMap> item)
        {
            return ((IDictionary<CommPort, ScpiCommandQueueMap>) _Map).Remove(item);
        }

        int ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.Count => _Map.Count;

        bool ICollection<KeyValuePair<CommPort, ScpiCommandQueueMap>>.IsReadOnly => ((IDictionary<CommPort, ScpiCommandQueueMap>) _Map).IsReadOnly;

        public bool ContainsKey(CommPort key)
        {
            return _Map.ContainsKey(key);
        }

        void IDictionary<CommPort, ScpiCommandQueueMap>.Add(CommPort key, ScpiCommandQueueMap value)
        {
            _Map.Add(key, value);
        }

        bool IDictionary<CommPort, ScpiCommandQueueMap>.Remove(CommPort key)
        {
            return _Map.Remove(key);
        }

        bool IDictionary<CommPort, ScpiCommandQueueMap>.TryGetValue(CommPort key, out ScpiCommandQueueMap value)
        {
            return _Map.TryGetValue(key, out value);
        }

        ScpiCommandQueueMap IDictionary<CommPort, ScpiCommandQueueMap>.this[CommPort key]
        {
            get => _Map[key];
            set => _Map[key] = value;
        }

        ICollection<CommPort> IDictionary<CommPort, ScpiCommandQueueMap>.Keys => _Map.Keys;

        ICollection<ScpiCommandQueueMap> IDictionary<CommPort, ScpiCommandQueueMap>.Values => _Map.Values;

        #endregion
    }
}