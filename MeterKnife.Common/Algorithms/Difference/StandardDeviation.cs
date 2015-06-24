using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 标准差
    /// </summary>
    public class StandardDeviation : IElectronDifferenceAlgorithm
    {
        public double NominalValue { get; set; }
        public IElectronAlgorithm Original { get; set; }
        public double Output()
        {
            throw new NotImplementedException();
        }
    }
}
