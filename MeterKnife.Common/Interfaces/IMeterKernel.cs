using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using NKnife.Events;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Common.Interfaces
{
    public interface IMeterKernel
    {
        string DataPath { get; set; }

        /// <summary>
        /// 串口下的Gpib地址
        /// </summary>
        Dictionary<CarePort, List<int>> GpibDictionary { get; }

        /// <summary>
        /// 已存在的仪器面板集合
        /// </summary>
        Dictionary<BaseMeter, DockContent> MeterContents { get; }

        /// <summary>
        /// 更新采集状态
        /// </summary>
        void UpdateCollectState(CarePort carePort, int address, bool isCollected);

        /// <summary>
        /// 当应用程序的采集状态发生改变时
        /// </summary>
        event EventHandler<CollectedEventArgs> Collected;
    }
}
