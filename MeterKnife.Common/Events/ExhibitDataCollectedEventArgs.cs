using System;
using MeterKnife.Models.Exhibits;

namespace MeterKnife.Events
{
    public class ExhibitDataCollectedEventArgs : EventArgs
    {
        public ExhibitData<double> ExhibitData { get; set; }

        public ExhibitDataCollectedEventArgs(ExhibitData<double> data)
        {
            ExhibitData = data;
        }
    }
}