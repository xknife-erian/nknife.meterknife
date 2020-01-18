using System;
using System.Collections.Generic;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common
{
    public interface IMeterKernel
    {
        string DataPath { get; set; }

        /// <summary>
        /// 串口下的Gpib地址
        /// </summary>
        Dictionary<Slot, List<int>> GpibDictionary { get; }

        /// <summary>
        /// 更新采集状态
        /// </summary>
        void UpdateCollectState(Slot carePort, int address, bool isCollected, string scpiGroupKey);

        /// <summary>
        /// 当应用程序的采集状态发生改变时
        /// </summary>
        event EventHandler<CollectedEventArgs> Collected;
    }
}
