using System;
using System.Collections.Generic;
using System.Linq;
using MeterKnife.Common.Base;

namespace MeterKnife.Common.Algorithms.Difference
{
    /// <summary>
    /// 标准差
    /// </summary>
    public class StandardDeviation : BaseDifferenceAlgorithm
    {
        private uint _Count = 0;
        private List<double> _Values = new List<double>();
        private int _Range = 50;

        public StandardDeviation()
        {
            DecimalDigit = 5;
        }

        public void SetRange(int range)
        {
            if (range <= 0 || range >= int.MaxValue)
                return;
            _Range = range;
            if (_Values.Count > range)
            {
                while (_Values.Count > range)
                {
                    _Values.RemoveAt(0);
                }
            }
        }

        public override void Clear()
        {
            _Count = 0;
            _Values = new List<double>(_Range);
            base.Clear();
        }

        public override void Input(double src)
        {
            _Count++;
            if (_Count <= 1)
                return;
            if (_Values.Count >= _Range)
                _Values.RemoveAt(0);
            _Values.Add(src);
            double avg = ValueOfComparison.Output;
            double sum = _Values.Select(value => value - avg).Select(v => v*v).Sum();
            Output = (Math.Sqrt(sum/_Values.Count))/ValueOfComparison.Output;
        }

        public override string ToString()
        {
            var op = Output*1000000;
            return string.Format("{0}ppm", Math.Round(op, DecimalDigit));
        }

    }
}
