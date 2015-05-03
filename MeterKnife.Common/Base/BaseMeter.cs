using System;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;

namespace MeterKnife.Common.Base
{
    public abstract class BaseMeter : IMeter
    {
        public int GpibAddress { get; set; }
        public string Name { get; set; }
        public string SimpleName { get { return MeterUtil.SimplifyName(Name); } }
        public object Parameters { get; set; }

    }
}