using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Algorithms
{
    /// <summary>
    /// 均方根值也称作为效值，它的计算方法是先平方、再平均、然后开方。
    /// </summary>
    public class RootMeanSquareValue : BaseAlgorithm
    {
        private double _RmsData;
        private uint _Count;

        public override void Input(double src)
        {
            _RmsData += src*src;
            _Count++;
            Output = Math.Sqrt(_RmsData/_Count);
        }
    }
}
