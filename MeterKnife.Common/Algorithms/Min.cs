using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    public class Min : BaseAlgorithm
    {
        public Min()
        {
            Output = double.MaxValue;
        }

        public override void Input(double src)
        {
            if (src < Output)
                Output = src;
        }

        public override string ToString()
        {
            if (Output == double.MaxValue)
                return "0";
            return base.ToString();
        }
    }
}
