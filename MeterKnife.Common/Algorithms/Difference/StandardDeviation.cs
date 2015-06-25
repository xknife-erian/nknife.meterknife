using System;
using MeterKnife.Common.Base;

namespace MeterKnife.Common.Algorithms.Difference
{
    /// <summary>
    /// 标准差
    /// </summary>
    public class StandardDeviation : BaseDifferenceAlgorithm
    {
        private uint _Count = 0;
        private double _Temp;

        public StandardDeviation()
        {
            DecimalDigit = 5;
        }

        public override void Input(double src)
        {
            _Count++;
            if (_Count <= 1)
                return;
            var v = src - ValueOfComparison.Output;
            _Temp += v*v;
            Output = (Math.Sqrt(_Temp/_Count))/ValueOfComparison.Output;
        }

        public override string ToString()
        {
            var op = Output*1000000;
            return string.Format("{0}ppm", Math.Round(op, DecimalDigit));
        }
    }
}
