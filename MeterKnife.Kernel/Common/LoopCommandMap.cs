using System.Collections.Generic;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;

namespace MeterKnife.Kernel.Common
{
    /// <summary>
    /// 一个端口可以有多个指令组等待循环；多个指令组由指令组的字符串Key进行管理。
    /// 当在仪器监控面板中，每个面板会拥有一个唯一的字符串Key，这样，面板开始采信与停止采集只需添加和移除该Key所
    /// 映射的指令组即可
    /// </summary>
    internal class LoopCommandMap : Dictionary<CarePort, Dictionary<string, CommandQueue.CareItem[]>>
    {
        public void Add(CarePort carePort, string key, CommandQueue.CareItem[] careItems)
        {
            Dictionary<string, CommandQueue.CareItem[]> items;
            if (!TryGetValue(carePort, out items))
            {
                items = new Dictionary<string, CommandQueue.CareItem[]>();
                Add(carePort, items);
            }
            items.Add(key, careItems);
        }

        public CommandQueue.CareItem[] this[CarePort carePort, string key]
        {
            get
            {
                return this[carePort][key];
            }
        }
    }
}
