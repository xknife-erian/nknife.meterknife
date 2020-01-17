using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Enums;

namespace MeterKnife.Common.Util
{
    /// <summary>
    /// 量程计算器
    /// </summary>
    public class MeterRangeCalculator
    {
        public static double Run(MeterRange meterRange, double sourceValue)
        {
            switch (meterRange)
            {
                case MeterRange.None:
                    return sourceValue;
                case MeterRange.X0001:
                case MeterRange.X001:
                case MeterRange.X01:
                    return sourceValue / 0.001;
                case MeterRange.X1:
                case MeterRange.X10:
                case MeterRange.X100:
                    return sourceValue;
                case MeterRange.X1K:
                case MeterRange.X10K:
                case MeterRange.X100K:
                    return sourceValue / 1000;
                case MeterRange.X1M:
                case MeterRange.X10M:
                case MeterRange.X100M:
                    return sourceValue / 1000 * 1000;
            }
            return sourceValue;
        }

        private static double RunAuto(double sourceValue)
        {
            if (sourceValue < 1)
                return sourceValue/0.001;
            if (sourceValue < 1000)
                return sourceValue;
            if (sourceValue < 1000*1000)
                return sourceValue/1000;
            return sourceValue/1000*1000;
        }
    }
}
