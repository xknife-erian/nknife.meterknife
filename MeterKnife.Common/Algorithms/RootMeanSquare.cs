using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 均方根
    /// </summary>
    public class RootMeanSquare : IElectronAlgorithm
    {
        public double Output { get; private set; }

        private double _RmsData;
        private uint _Count;

        public void Input(params double[] src)
        {
            foreach (var value in src)
            {
                _RmsData += value*value;
                _Count++;
                Output = Math.Sqrt(_RmsData/_Count);
            }
        }
    }
}
