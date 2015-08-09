using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using MathNet.Numerics.Statistics;
using MeterKnife.Common.Base;

namespace MeterKnife.Common.Algorithms.Difference
{
    /// <summary>
    /// 样本标准差
    /// </summary>
    public class SampleStandardDeviation : BaseDifferenceAlgorithm
    {
        private List<double> _Values = new List<double>();
        private int _Range = 50;
        private string _RangeFormate = "f8";

        public override ushort DecimalDigit
        {
            get { return _DecimalDigit; }
            set
            {
                _DecimalDigit = value;
                _RangeFormate = string.Format("f{0}", value);
            }
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
            _Values = new List<double>(_Range);
            base.Clear();
        }

        public override void Input(double src)
        {
            if (_Values.Count >= _Range)
                _Values.RemoveAt(0);
            _Values.Add(src);
            Output = ArrayStatistics.StandardDeviation(_Values.ToArray());
        }

        public override string ToString()
        {
            if (Output == 0 || Output == double.NaN || Output == double.MaxValue || Output == double.MinValue)
                return "0";
            var o = Output*1000000;
            return string.Format("{0}ppm", o.ToString(_RangeFormate));
        }

    }
}
