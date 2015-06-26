using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Controls.Plots
{
    public class TemperatureDataPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "temperature"; }
        }
    }
}
