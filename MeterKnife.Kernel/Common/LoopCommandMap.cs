using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using NKnife.Utility;

namespace MeterKnife.Kernel.Common
{
    /// <summary>
    /// 一个端口可以有多个指令组等待循环；多个指令组由指令组的字符串Key进行管理。
    /// 当在仪器监控面板中，每个面板会拥有一个唯一的字符串Key，这样，面板开始采信与停止采集只需添加和移除该Key所
    /// 映射的指令组即可
    /// </summary>
    public class LoopCommandMap : Dictionary<CarePort, Dictionary<string, CommandQueue.CareItem[]>>
    {
        private static readonly ILog _logger = LogManager.GetLogger<LoopCommandMap>();

        public void Add(CarePort carePort, string key, CommandQueue.CareItem[] careItemses)
        {
            Dictionary<string, CommandQueue.CareItem[]> items;
            if (!TryGetValue(carePort, out items))
            {
                items = new Dictionary<string, CommandQueue.CareItem[]>();
                Add(carePort, items);
            }
            items.Add(key, careItemses);
        }

        public CommandQueue.CareItem[] this[CarePort carePort, string key]
        {
            get
            {
                return this[carePort][key];
            }
        }
        private readonly object _Lock = new object();

        public bool HasCommand(CarePort carePort)
        {
            bool hasCommand = false;
            lock (_Lock)
            {
                hasCommand = ContainsKey(carePort) && this[carePort].Values.Count > 0;
            }
            return hasCommand;
        }

        public void Remove(CarePort carePort, string scpiGroupKey)
        {
            lock (_Lock)
            {
                Dictionary<string, CommandQueue.CareItem[]> scmap = this[carePort];
                //根据指定端口的指令组的Key停止采集指令循环
                if (scmap.ContainsKey(scpiGroupKey))
                {
                    scmap.Remove(scpiGroupKey);
                }
            }
        }

        public IEnumerable<IEnumerable<CommandQueue.CareItem>> GetCareItemses(CarePort carePort)
        {
            var items = new List<List<CommandQueue.CareItem>>();
            lock (_Lock)
            {
                try
                {
                    var values = this[carePort].Values;
                    foreach (CommandQueue.CareItem[] careItems in values)
                    {
                        if (UtilityCollection.IsNullOrEmpty(careItems))
                            continue;
                        var its = new List<CommandQueue.CareItem>();
                        foreach (var careItem in careItems)
                        {
                            its.Add(careItem.Clone());
                        }
                        if (its.Count > 0)
                            items.Add(its);
                    }
                }
                catch (Exception e)
                {
                    _logger.Warn(string.Format("Clone集合异常:{0}", e.Message), e);
                }
            }
            return items;
        }
    }
}
