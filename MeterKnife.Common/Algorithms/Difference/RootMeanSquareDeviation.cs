using System;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms.Difference
{
    /// <summary>
    /// 均方根差
    /// </summary>
    public class RootMeanSquareDeviation : BaseDifferenceAlgorithm
    {
        public override void Input(double src)
        {
            var v = src-ValueOfComparison.Output;
            Output = v;
            //未实现
        }

    }
}
