using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Logging;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData
    {
        #region 分析内容
        
        [Category("数据分析"), DisplayName("最大值")]
        public double Max { get; set; }

        [Category("数据分析")]
        [DisplayName("最大值")]
        public double Min { get; set; }

        [Category("数据分析")]
        [DisplayName("均方根")]
        public double RootMeanSquare { get; set; }

        [Category("数据分析"), DisplayName("算术平均")]
        public double ArithmeticMean { get; set; }

        [Category("数据分析"), DisplayName("总采样数")]
        public uint Count { get { return (uint)_Datas.Count; } }

        [Category("温度"), DisplayName("最大值")]
        public double MaxTemperature { get; set; }

        [Category("温度"), DisplayName("最大值")]
        public double MinTemperature { get; set; }

        [Category("温度"), DisplayName("均方根")]
        public double TemperatureRootMeanSquare { get; set; }

        [Category("温度"), DisplayName("算术平均")]
        public double TemperatureArithmeticMean { get; set; }

        #endregion

        private readonly List<double> _Datas = new List<double>();
        private double _SumData = 0;
        private double _RmsData = 0;
        private readonly List<double> _TemperatureDatas = new List<double>();
        private double _SumTemperatureData = 0;
        private double _RmsTemperatureData = 0;

        private static readonly ILog _logger = LogManager.GetLogger<FiguredData>();

        public void Add(double value)
        {
            _Datas.Add(value);
            if (_Datas.Count <= 1)
            {
                Max = value;
                Min = value;
            }
            else
            {
                if (value > Max) Max = value;//最大值
                else if (value < Min) Min = value;//最小值
            }
            _SumData += value;
            ArithmeticMean = _SumData / _Datas.Count;
            _RmsData += value * value;
            RootMeanSquare = Math.Sqrt(_RmsData / _Datas.Count);
        }

        public void AddTemperature(double value)
        {
            _TemperatureDatas.Add(value);
            if (_TemperatureDatas.Count <= 1)
            {
                MaxTemperature = value;
                MinTemperature = value;
            }
            else
            {
                if (value > MaxTemperature) MaxTemperature = value;
                else if (value < MinTemperature) MinTemperature = value;
            }
            _SumTemperatureData += value;
            TemperatureArithmeticMean = _SumTemperatureData / _TemperatureDatas.Count;
            _RmsTemperatureData += value * value;
            TemperatureRootMeanSquare = Math.Sqrt(_RmsTemperatureData / _TemperatureDatas.Count);
        }

        public override string ToString()
        {
            return string.Format("Max:{0}; Min:{1}; RMS:{2}", Max, Min, RootMeanSquare);
        }
    }
}
