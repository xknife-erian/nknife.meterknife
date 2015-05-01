using System.ComponentModel;

namespace MeterKnife.Kernel.DataModels
{
    public class FiguredData
    {
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
        public uint Count { get; set; }

        [Category("温度"), DisplayName("最大值")]
        public double MaxTemperature { get; set; }

        [Category("温度"), DisplayName("最大值")]
        public double MinTemperature { get; set; }

        [Category("温度"), DisplayName("均方根")]
        public double TemperatureRootMeanSquare { get; set; }

        [Category("温度"), DisplayName("算术平均")]
        public double TemperatureArithmeticMean { get; set; }

        public override string ToString()
        {
            return string.Format("Max:{0}; Min:{1}; RMS:{2}", Max, Min, RootMeanSquare);
        }
    }
}
