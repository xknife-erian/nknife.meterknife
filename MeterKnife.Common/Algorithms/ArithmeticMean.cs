using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 算术平均
    /// </summary>
    public class ArithmeticMean :　BaseAlgorithm
    {
        private double _SumData;
        private uint _Count;

        public override void Input(double src)
        {
            _SumData += src;
            _Count++;
            Output = _SumData/_Count;
        }
    }
}
