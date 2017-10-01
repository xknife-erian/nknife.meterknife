using System;
using MeterKnife.Base;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Interfaces.Measures
{
    public class MeasureEventArgs : EventArgs
    {
        public MeasureEventArgs(ExhibitBase exhibit, double value)
        {
            Value = value;
            Exhibit = exhibit;
        }
        public double Value { get; set; }
        public ExhibitBase Exhibit { get; set; }
    }
}