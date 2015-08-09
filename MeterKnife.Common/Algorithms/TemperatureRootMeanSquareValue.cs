using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 温度均方根值
    /// </summary>
    public class TemperatureRootMeanSquareValue : RootMeanSquareValue
    {
        public TemperatureRootMeanSquareValue()
        {
            DecimalDigit = 3;
        }
    }
}
