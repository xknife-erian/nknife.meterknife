using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Events;

namespace MeterKnife.Common.Interfaces
{
    public interface IMeterKernel
    {
        /// <summary>
        /// 串口下的Gpib地址
        /// </summary>
        Dictionary<int, List<int>> GpibDictionary { get; set; }

        /// <summary>
        /// 正在采集数据,当正在采集数据,一些功能将被禁用
        /// </summary>
        bool OnCollected { get; set; }

        /// <summary>
        /// 当应用程序的采集状态发生改变时
        /// </summary>
        event EventHandler<EventArgs<bool>> Collected;
    }
}
