using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Logging;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData : ICollectSource
    {
        #region 分析内容

        [Category("数据分析"), DisplayName("最大值")]
        public double Max { get; private set; }

        [Category("数据分析")]
        [DisplayName("最大值")]
        public double Min { get; private set; }

        [Category("数据分析")]
        [DisplayName("均方根")]
        public double RootMeanSquare { get; private set; }

        [Category("数据分析"), DisplayName("算术平均")]
        public double ArithmeticMean { get; private set; }

        [Category("数据分析"), DisplayName("总采样数")]
        public uint Count
        {
            get { return (uint) _Datas.Count; }
        }

        [Category("温度"), DisplayName("最大值")]
        public double MaxTemperature { get; private set; }

        [Category("温度"), DisplayName("最大值")]
        public double MinTemperature { get; private set; }

        [Category("温度"), DisplayName("均方根")]
        public double TemperatureRootMeanSquare { get; private set; }

        [Category("温度"), DisplayName("算术平均")]
        public double TemperatureArithmeticMean { get; private set; }

        [Category("偏差"), DisplayName("标称值")]
        public double ExpectedValue { get; set; }

        [Category("偏差"), DisplayName("阿仑方差")]
        public double AllanVariance { get; private set; }

        [Category("偏差"), DisplayName("均方根差")]
        public double RootMeanSquareDeviation { get; private set; }

        #endregion

        private static readonly ILog _logger = LogManager.GetLogger<FiguredData>();
        private readonly List<double> _Datas = new List<double>();
        private readonly List<double> _TemperatureDatas = new List<double>();
        private double _CurrentTemperature;
        private double _RmsData;
        private double _RmsTemperatureData;
        private double _SumData;
        private double _SumTemperatureData;

        [Browsable(false)]
        public IMeter Meter { get; set; }

        public event EventHandler<CollectEventArgs> ReceviedCollectData;

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
                if (value > Max) Max = value; //最大值
                else if (value < Min) Min = value; //最小值
            }
            _SumData += value;
            ArithmeticMean = _SumData/_Datas.Count;

            //计算均方根
            _RmsData += value*value;
            RootMeanSquare = Math.Sqrt(_RmsData/_Datas.Count);

            if (Math.Abs(ExpectedValue) > 0)
            {
                RootMeanSquareDeviation = RootMeanSquare - ExpectedValue;
            }

            //触发数据源发生变化
            OnReceviedCollectData(new CollectEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
        }

        public void AddTemperature(double value)
        {
            _CurrentTemperature = value;
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
            TemperatureArithmeticMean = Math.Round(_SumTemperatureData/_TemperatureDatas.Count, 4);

            //计算均方根
            _RmsTemperatureData += value*value;
            TemperatureRootMeanSquare = Math.Round(Math.Sqrt(_RmsTemperatureData/_TemperatureDatas.Count), 4);
        }

        protected virtual void OnReceviedCollectData(CollectEventArgs e)
        {
            EventHandler<CollectEventArgs> handler = ReceviedCollectData;
            if (handler != null) handler(this, e);
        }
    }
}