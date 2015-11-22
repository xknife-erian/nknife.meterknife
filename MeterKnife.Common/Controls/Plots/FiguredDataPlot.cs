using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.DataModels;
using OxyPlot.Axes;

namespace MeterKnife.Common.Controls.Plots
{
    public class FiguredDataPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "value"; }
        }


    }
}
