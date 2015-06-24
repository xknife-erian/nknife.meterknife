using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    public class Min : IElectronAlgorithm
    {
        public double Output { get; private set; }
        public void Input(params double[] src)
        {
            foreach (var v in src)
            {
                if (v < Output)
                    Output = v;
            }
        }
    }
}
