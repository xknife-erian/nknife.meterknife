using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 算术平均
    /// </summary>
    public class ArithmeticMean :　IElectronAlgorithm
    {
        public double Output { get; private set; }

        private double _SumData;
        private uint _Count;

        public void Input(params double[] src)
        {
            foreach (var v in src)
            {
                _SumData += v;
                _Count++;
                Output = _SumData/_Count;
            }
        }
    }
}
