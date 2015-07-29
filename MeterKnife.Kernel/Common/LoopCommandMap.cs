using System.Collections.Generic;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;

namespace MeterKnife.Kernel.Common
{
    internal class LoopCommandMap : Dictionary<CarePort, Dictionary<string, CommandQueue.CareItem[]>>
    {
        public void Add(CarePort carePort, string key, CommandQueue.CareItem[] careItems)
        {
            Dictionary<string, CommandQueue.CareItem[]> items;
            if (!TryGetValue(carePort, out items))
                items = new Dictionary<string, CommandQueue.CareItem[]>();
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
