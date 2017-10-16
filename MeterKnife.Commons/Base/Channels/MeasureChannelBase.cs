using System;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureChannelBase<T> : ChannelBase<T>
    {
        /// <summary>
        /// 绑定一个测量事件
        /// </summary>
        /// <param name="measure">一个测量事件</param>
        public abstract void Binding(MeasureJob.Measure measure);
    }
}