using System;
using MeterKnife.Models;
using MeterKnife.Scpis;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureChannelBase<T> : ChannelBase<T>
    {
        public abstract MeasureQuestionGroup<string> ToQuestionGroup(MeasureJob.Measure measure);
    }
}