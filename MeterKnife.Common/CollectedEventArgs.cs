using System;
using NKnife.MeterKnife.Common.DataModels;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    /// 采集事件
    /// </summary>
    public class CollectedEventArgs : EventArgs
    {
        public CollectedEventArgs(Slot slot, int address, bool isCollected, string scpiGroupKey)
        {
            Slot = slot;
            GpibAddress = address;
            IsCollected = isCollected;
            ScpiGroupKey = scpiGroupKey;
        }

        /// <summary>
        /// 采集发生动作的插槽
        /// </summary>
        public Slot Slot { get; }

        /// <summary>
        /// 采集指向的GPIB地址
        /// </summary>
        public int GpibAddress { get; }

        /// <summary>
        /// 采集指令组组名
        /// </summary>
        public string ScpiGroupKey { get; }

        /// <summary>
        /// 如果为true，代表已采集完成
        /// </summary>
        public bool IsCollected { get; }
    }
}