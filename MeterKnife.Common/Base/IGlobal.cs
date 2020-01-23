using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    /// 软件全局的一些数据
    /// </summary>
    public interface IGlobal
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
        /// 当采集状态完成时发生
        /// </summary>
        event EventHandler<CollectedEventArgs> Collected;
    }
}
