using System;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace MeterKnife.Interfaces.Measures
{
    /// <summary>
    /// 面向全局的测量数据广播服务。该测量服务以事件方式，广播测量指令所采集到的数据。
    /// </summary>
    public interface IMeasureService : IEnvironmentItem
    {
        event EventHandler<MeasureEventArgs> Measured;

        void AddValue(ushort number, IExhibit exhibit, double value);
    }
}