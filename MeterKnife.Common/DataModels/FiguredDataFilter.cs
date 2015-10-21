using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.DataModels
{
    public class FiguredDataFilter
    {
        public FiguredDataFilter()
        {
            InStatistical = true;
            IsSave = true;
        }

        public uint Multiple { get; set; }
        public bool IsSave { get; set; }
        public bool InStatistical { get; set; }

    }
}
