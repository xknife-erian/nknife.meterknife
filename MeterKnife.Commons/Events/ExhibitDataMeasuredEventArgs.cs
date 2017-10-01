using System;
using MeterKnife.Models.Exhibits;

namespace MeterKnife.Events
{
    public class ExhibitDataMeasuredEventArgs : EventArgs
    {
        public ExhibitData<double> ExhibitData { get; set; }

        public ExhibitDataMeasuredEventArgs(ExhibitData<double> data)
        {
            ExhibitData = data;
        }
    }
}