using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NKnife.MeterKnife.Util.Scpi;
using NKnife.Util;
using NLog;

namespace NKnife.MeterKnife.Common.DataModels
{
    /// <summary>
    ///     一个端口可以有多个指令组等待循环；多个指令组由指令组的字符串Key进行管理。
    ///     当在仪器监控面板中，每个面板会拥有一个唯一的字符串Key，这样，面板开始采信与停止采集只需添加和移除该Key所
    ///     映射的指令组即可
    /// </summary>
    public class SlotCommandMap : IDictionary<Slot, ScpiCommandQueueMap>
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<Slot, ScpiCommandQueueMap> _map = new Dictionary<Slot, ScpiCommandQueueMap>();
        private readonly object _Lock = new object();

        private List<List<ScpiCommandQueue.Item>> _CloneItems = new List<List<ScpiCommandQueue.Item>>();

        public ScpiCommandQueue.Item[] this[Slot slot, string key] => _map[slot][key];

        public void Add(Slot slot, string key, ScpiCommandQueue.Item[] careItems)
        {
            if (!_map.TryGetValue(slot, out var queueMap))
            {
                queueMap = new ScpiCommandQueueMap();
                _map.Add(slot, queueMap);
            }

            queueMap.Add(key, careItems);
        }

        public bool HasCommand(Slot slot)
        {
            var hasCommand = false;
            lock (_Lock)
            {
                hasCommand = _map.ContainsKey(slot) && _map[slot].Values.Count > 0;
            }

            return hasCommand;
        }

        public void Remove(Slot slot, string scpiGroupKey)
        {
            lock (_Lock)
            {
                if (!_map.TryGetValue(slot, out var map))
                    return;
                //根据指定端口的指令组的Key停止采集指令循环
                if (map.ContainsKey(scpiGroupKey))
                {
                    map.Remove(scpiGroupKey);
                    _CloneItems.Clear();
                }
            }
        }

        public IEnumerable<IEnumerable<ScpiCommandQueue.Item>> GetItems(Slot slot)
        {
            if (_CloneItems == null || _CloneItems.Count <= 0)
            {
                _CloneItems = new List<List<ScpiCommandQueue.Item>>();
                try
                {
                    var values = _map[slot].Values;
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

        public string[] GetScpiGroupKeys(Slot slot)
        {
            return _map[slot].Select(pair => pair.Key).ToArray();
        }

        public bool ContainsKey(Slot slot, string key)
        {
            return _map.ContainsKey(slot) && _map[slot].ContainsKey(key);
        }

        #region IDictionary

        IEnumerator<KeyValuePair<Slot, ScpiCommandQueueMap>> IEnumerable<KeyValuePair<Slot, ScpiCommandQueueMap>>.GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        void ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.Add(KeyValuePair<Slot, ScpiCommandQueueMap> item)
        {
            ((IDictionary<Slot, ScpiCommandQueueMap>) _map).Add(item);
        }

        void ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.Clear()
        {
            _map.Clear();
        }

        bool ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.Contains(KeyValuePair<Slot, ScpiCommandQueueMap> item)
        {
            return ((IDictionary<Slot, ScpiCommandQueueMap>) _map).Contains(item);
        }

        void ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.CopyTo(KeyValuePair<Slot, ScpiCommandQueueMap>[] array, int arrayIndex)
        {
            ((IDictionary<Slot, ScpiCommandQueueMap>) _map).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.Remove(KeyValuePair<Slot, ScpiCommandQueueMap> item)
        {
            return ((IDictionary<Slot, ScpiCommandQueueMap>) _map).Remove(item);
        }

        int ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.Count => _map.Count;

        bool ICollection<KeyValuePair<Slot, ScpiCommandQueueMap>>.IsReadOnly => ((IDictionary<Slot, ScpiCommandQueueMap>) _map).IsReadOnly;

        public bool ContainsKey(Slot key)
        {
            return _map.ContainsKey(key);
        }

        void IDictionary<Slot, ScpiCommandQueueMap>.Add(Slot key, ScpiCommandQueueMap value)
        {
            _map.Add(key, value);
        }

        bool IDictionary<Slot, ScpiCommandQueueMap>.Remove(Slot key)
        {
            return _map.Remove(key);
        }

        bool IDictionary<Slot, ScpiCommandQueueMap>.TryGetValue(Slot key, out ScpiCommandQueueMap value)
        {
            return _map.TryGetValue(key, out value);
        }

        ScpiCommandQueueMap IDictionary<Slot, ScpiCommandQueueMap>.this[Slot key]
        {
            get => _map[key];
            set => _map[key] = value;
        }

        ICollection<Slot> IDictionary<Slot, ScpiCommandQueueMap>.Keys => _map.Keys;

        ICollection<ScpiCommandQueueMap> IDictionary<Slot, ScpiCommandQueueMap>.Values => _map.Values;

        #endregion
    }
}