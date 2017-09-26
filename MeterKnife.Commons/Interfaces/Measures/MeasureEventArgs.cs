using System;
using MeterKnife.Scpis;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Interfaces.Measures
{
    public class MeasureEventArgs : EventArgs
    {
        public MeasureEventArgs(ushort number, double value, IExhibit exhibit)
        {
            Number = number;
            Value = value;
            Exhibit = exhibit;
        }
        public ushort Number { get; set; }
        public double Value { get; set; }
        public IExhibit Exhibit { get; set; }
    }
}