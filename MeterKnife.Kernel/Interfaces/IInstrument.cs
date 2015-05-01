using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Kernel.Interfaces;

namespace MonitorKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述仪器的接口
    /// </summary>
    public interface IInstrument : ICollectSource
    {
        string Name { get; }
    }
}
