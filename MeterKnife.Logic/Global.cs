using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
{
    public class Global : IGlobal
    {
        /// <summary>
        ///     指定的端口下的已使用的GPIB地址,不同的端口下的地址可以重复,同一端口下的地址不允许重复
        /// </summary>
        public Dictionary<Slot, List<int>> GpibDictionary { get; } = new Dictionary<Slot, List<int>>();
    }
}