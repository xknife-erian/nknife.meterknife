using System.Collections.Generic;
using System.ComponentModel;

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

        [Category("数据分析"), DisplayName("均方根差")]
        public double RootMeanSquareDeviation { get; set; }

        [Category("数据分析"), DisplayName("标准差")]
        public double StandardDeviation { get; set; }

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
        private readonly List<double> _TemperatureDatas = new List<double>();

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
                if (value > Max) Max = value;
                else if (value < Min) Min = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Max:{0}; Min:{1}; RMS:{2}", Max, Min, RootMeanSquare);
        }
    }
}
