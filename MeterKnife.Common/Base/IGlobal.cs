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
        /// <summary>
        /// 串口下的Gpib地址
        /// </summary>
        Dictionary<Slot, List<int>> GpibDictionary { get; }
    }
}
