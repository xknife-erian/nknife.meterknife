using System;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms.Difference
{
    /// <summary>
    /// 均方根差
    /// </summary>
    public class RootMeanSquareDeviation : IElectronDifferenceAlgorithm
    {
        public double NominalValue { get; set; }
        public IElectronAlgorithm Original { get; set; }
        public double Output()
        {
            return Original.Output - NominalValue;
        }
    }
}
