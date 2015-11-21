using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Common.Logging;
using MathNet.Numerics.Statistics;
using MeterKnife.Common.Enums;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;
using NKnife.IoC;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData : ICollectSource
    {
        public const string DATETIME = "datetime";
        public const string VALUE = "value";
        public const string TEMPERATURE = "temperature";
        public const string STANDARD_DEVIATION = "standard_deviation";
        private static readonly ILog _logger = LogManager.GetLogger<FiguredData>();

        protected readonly ITemperatureService _TempService = DI.Get<ITemperatureService>();
        protected double _CurrentTemperature;
        protected DataSet _DataSet = new DataSet();
        protected string _DecimalDigit = "f6";
        protected string _PpmDecimalDigit = "f3";
        protected RunningStatistics _RunningStatistics = new RunningStatistics();
        protected int _SampleRange = 50;
        protected RunningStatistics _TemperatureRunningStatistics = new RunningStatistics();
        protected List<double> _Values = new List<double>();

        public FiguredData()
        {
            MeterRange = MeterRange.None;
            Filter = new FiguredDataFilter();
            Clear();

            var baseTable = new DataTable("BaseInfomation");
            baseTable.Columns.Add(new DataColumn("Key", typeof (string)));
            baseTable.Columns.Add(new DataColumn("Value", typeof (string)));
            _DataSet.Tables.Add(baseTable);

            var collectTable = new DataTable("CollectData");
            collectTable.Columns.Add(new DataColumn("datetime", typeof (DateTime)));
            collectTable.Columns.Add(new DataColumn("value", typeof (double)));
            collectTable.Columns.Add(new DataColumn("temperature", typeof (double)));
            collectTable.Columns.Add(new DataColumn("standard_deviation", typeof (double)));
            collectTable.Columns.Add(new DataColumn("ppv", typeof (double)));
            collectTable.Columns.Add(new DataColumn("abnormal", typeof (bool))); //是否是异常数据
            _DataSet.Tables.Add(collectTable);
        }

        #region implementing interface ICollectSource
        
        [Browsable(false)]
        public IMeter Meter { get; set; }

        public bool Save(string fileFullName)
        {
            return DI.Get<IMeterDataService>().Save(fileFullName, DataSet);
        }

        public event EventHandler<CollectDataEventArgs> ReceviedCollectData;

        [Browsable(false)]
        public DataSet DataSet
        {
            get { return _DataSet; }
        }

        [Browsable(false)]
        public MeterRange MeterRange { get; set; }

        [Browsable(false)]
        public bool HasData
        {
            get { return _DataSet.Tables[1].Rows.Count > 0; }
        }

        public void Clear()
        {
            if (_DataSet.Tables.Count > 1)
            {
                _DataSet.Tables[1].Rows.Clear();
                _DataSet.AcceptChanges();
            }
            _CurrentTemperature = 0;
            _RunningStatistics = new RunningStatistics();
            _TemperatureRunningStatistics = new RunningStatistics();

            Ppvalue = "0";
            TemperaturePpvalue = "0";

            SampleInterquartileRangeInplace = "0";
            SampleKurtosis = "0";
            SampleLowerQuartile = "0";
            SampleMean = "0";
            SampleMedianInplace = "0";
            SampleRootMeanSquareValue = "0";
            SampleSkewness = "0";
            SampleStandardDeviation = "0";
            SampleUpperQuartile = "0";

            _Values.Clear();
        }

        [Browsable(false)]
        public FiguredDataFilter Filter { get; set; }

        public virtual bool Add(double value)
        {
            //var v = MeterRangeCalculator.Run(MeterRange, value);
            var s = value.ToString("F12").TrimEnd('0');
            var n = s.Length - s.IndexOf('.');
            _DecimalDigit = string.Format("f{0}", n);
            _PpmDecimalDigit = string.Format("f{0}", ((uint) (n/2)) + 2);

            double relativeSampleStandardDeviation = 0;//相对标准方差

            var inFilter = false;
            if (Filter.Multiple != 0 && _DataSet.Tables[1].Rows.Count > 3)
            {
                var pre = (double) _DataSet.Tables[1].AsEnumerable().Last()["value"];
                var cha = Math.Abs(value - pre);
                var avg = GetPpvMean(_DataSet.Tables[1]);
                if (cha/avg > Filter.Multiple)
                {
                    _logger.Info(string.Format("{0}在被过滤范围内", value));
                    inFilter = true;
                }
            }

            if (!inFilter || Filter.InStatistical)
            {
                _RunningStatistics.Push(value);

                if (_Values.Count >= _SampleRange)
                    _Values.RemoveAt(0);
                _Values.Add(value);
                var ds = new DescriptiveStatistics(_Values);
                SampleKurtosis = ds.Kurtosis.ToString(_DecimalDigit).TrimEnd('0');
                SampleSkewness = ds.Skewness.ToString(_DecimalDigit).TrimEnd('0');

                var array = _Values.ToArray();
                var mean = ArrayStatistics.Mean(array);
                SampleMean = mean.ToString(_DecimalDigit).TrimEnd('0');
                var sampleStandardDeviation = ArrayStatistics.PopulationStandardDeviation(array);
                relativeSampleStandardDeviation = sampleStandardDeviation / GetRelativeValue(MeterRange, mean);
                SampleStandardDeviation = GetPpmValue(relativeSampleStandardDeviation).TrimEnd('0');
                SampleRootMeanSquareValue = ArrayStatistics.RootMeanSquare(array).ToString(_DecimalDigit).TrimEnd('0');

                Array.Sort(array);
                SampleInterquartileRangeInplace = GetPpmValue(SortedArrayStatistics.InterquartileRange(array)).TrimEnd('0');
                SampleMedianInplace = SortedArrayStatistics.Median(array).ToString(_DecimalDigit).TrimEnd('0');
                SampleLowerQuartile = SortedArrayStatistics.LowerQuartile(array).ToString(_DecimalDigit).TrimEnd('0');
                SampleUpperQuartile = SortedArrayStatistics.UpperQuartile(array).ToString(_DecimalDigit).TrimEnd('0');

                Ppvalue = Math.Abs(_RunningStatistics.Maximum - _RunningStatistics.Minimum).ToString(_DecimalDigit).TrimEnd('0'); //峰峰值
                TemperaturePpvalue = Math.Abs(_TemperatureRunningStatistics.Maximum - _TemperatureRunningStatistics.Minimum).ToString("f3").TrimEnd('0'); //峰峰值

                UpdateTemperature();

                ExtremePoint = new Tuple<double, double>(_RunningStatistics.Maximum, _RunningStatistics.Minimum);
                TemperatureExtremePoint = new Tuple<double, double>(_TemperatureRunningStatistics.Maximum, _TemperatureRunningStatistics.Minimum);
            }
            else
            {
                _logger.Debug(string.Format("{0}未参与统计", value));
            }
            if (!inFilter || Filter.IsSave)
            {
                _DataSet.Tables[1].Rows.Add(DateTime.Now, value, _CurrentTemperature, relativeSampleStandardDeviation, Ppvalue, inFilter);
                //触发数据源发生变化
                OnReceviedCollectData(new CollectDataEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
            }
            else
            {
                _logger.Debug(string.Format("{0}不保存", value));
            }
            return !inFilter;
        }

        private static double GetRelativeValue(MeterRange meterRange, double mean)
        {
            switch (meterRange)
            {
                case MeterRange.None:
                    return mean;
                case MeterRange.X0001:
                    return 0.001;
                case MeterRange.X001:
                    return 0.01;
                case MeterRange.X01:
                    return 0.1;
                case MeterRange.X1:
                    return 1;
                case MeterRange.X10:
                    return 10;
                case MeterRange.X100:
                    return 100;
                case MeterRange.X1K:
                    return 1000;
                case MeterRange.X10K:
                    return 10000;
                case MeterRange.X100K:
                    return 100000;
                case MeterRange.X1M:
                    return 1000000;
                case MeterRange.X10M:
                    return 10000000;
                case MeterRange.X100M:
                    return 100000000;
            }
            return mean;
        }

        #endregion

        public void SetRange(int range)
        {
            if (range <= 0 || range >= int.MaxValue)
                return;
            _SampleRange = range;
            if (_Values.Count > range)
            {
                while (_Values.Count > range)
                {
                    _Values.RemoveAt(0);
                }
            }
        }

        private static double GetPpvMean(DataTable dataTable)
        {
            var act = new List<double>();
            var list = dataTable.AsEnumerable().ToList();
            var index = list.Count - 1;
            var i = 0;
            while (i < 50 && index - i > 0)
            {
                var n = (double) list[index - i]["ppv"];
                act.Add(n);
                i++;
            }
            return ArrayStatistics.Mean(act.ToArray());
        }

        protected string GetPpmValue(double value)
        {
            var d = value*1000000;
            return string.Format("{0} ppm", d.ToString(_PpmDecimalDigit));
        }

        protected virtual void UpdateTemperature()
        {
            _CurrentTemperature = _TempService.TemperatureValues[0];
            _TemperatureRunningStatistics.Push(_CurrentTemperature);
        }

        protected virtual void OnReceviedCollectData(CollectDataEventArgs e)
        {
            var handler = ReceviedCollectData;
            if (handler != null)
                handler(this, e);
        }

        #region 数据集统计属性

        #region 数据

        [Category("数据"), DisplayName("总采样数")]
        public string Count
        {
            get { return _DataSet.Tables[1].Rows.Count.ToString(); }
        }

        [Category("数据"), DisplayName("最大值")]
        public string Maximum
        {
            get
            {
                if (_RunningStatistics.Maximum.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.Maximum.ToString(_DecimalDigit).TrimEnd('0');
            }
        }

        [Category("数据"), DisplayName("最小值")]
        public string Minimum
        {
            get
            {
                if (_RunningStatistics.Minimum.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.Minimum.ToString(_DecimalDigit).TrimEnd('0');
            }
        }

        [Category("数据"), DisplayName("峰-峰值")]
        public string Ppvalue { get; private set; }

        [Category("数据"), DisplayName("算术平均值")]
        public string Mean
        {
            get
            {
                if (_RunningStatistics.Mean.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.Mean.ToString(_DecimalDigit).TrimEnd('0');
            }
        }

        /*** 偏度就是样本偏斜度的估计值,峰度约等于样本峰值减去3。
         * 因此,若一组观察数据的偏度、峰度都接近于0,则可以认为这组数据是来自正态分布总体。
         * 若其偏度为正,则表示与标准正态分布相比,其峰度偏向较小数值方;
         * 偏度为负,则表示与标准正态分布相比,其峰偏向较大数值方;
         * 若其峰度为正,则表示与标准正态分布相比,其分布相对尖锐;
         * 峰度为负,则表示与标准正态分布相比,其分布相对平坦。
         */

        [Category("数据"), DisplayName("峰度")]
        public string PopulationKurtosis
        {
            get
            {
                if (_RunningStatistics.PopulationKurtosis.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.PopulationKurtosis.ToString(_DecimalDigit).TrimEnd('0');
            }
        }

        [Category("数据"), DisplayName("偏度")]
        public string PopulationSkewness
        {
            get
            {
                if (_RunningStatistics.PopulationSkewness.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.PopulationSkewness.ToString(_DecimalDigit).TrimEnd('0');
            }
        }

        #endregion

        #region 样本

        [Category("样本"), DisplayName("标准方差")]
        public string SampleStandardDeviation { get; private set; }

        [Category("样本"), DisplayName("算术平均值")]
        public string SampleMean { get; private set; }

        [Category("样本"), DisplayName("均方根值")]
        public string SampleRootMeanSquareValue { get; private set; }

        [Category("样本"), DisplayName("中位数")]
        public string SampleMedianInplace { get; private set; }

        /**
         * 四分位距（interquartile range, IQR），又称四分差。
         * 是描述统计学中的一种方法，以确定第三四分位数和第一四分位数的区别（即Q1~Q3 的差距）。
         * 与方差、标准差一样，表示统计资料中各变量分散情形，但四分差更多为一种稳健统计（robust statistic）。
         */

        [Browsable(false)]
        [Category("样本"), DisplayName("四分位距")]
        public string SampleInterquartileRangeInplace { get; private set; }

        [Browsable(false)]
        [Category("样本"), DisplayName("高四分位")]
        public string SampleUpperQuartile { get; private set; }

        [Browsable(false)]
        [Category("样本"), DisplayName("低四分位")]
        public string SampleLowerQuartile { get; private set; }

        [Category("样本"), DisplayName("峰度")]
        public string SampleKurtosis { get; private set; }

        [Category("样本"), DisplayName("偏度")]
        public string SampleSkewness { get; private set; }

        #endregion

        #region 温度

        [Category("温度"), DisplayName("最大值")]
        public string TemperatureMaximum
        {
            get
            {
                if (_TemperatureRunningStatistics.Maximum.Equals(double.NaN))
                    return "0";
                return _TemperatureRunningStatistics.Maximum.ToString("f2").TrimEnd('0');
            }
        }

        [Category("温度"), DisplayName("最小值")]
        public string TemperatureMinimum
        {
            get
            {
                if (_TemperatureRunningStatistics.Minimum.Equals(double.NaN))
                    return "0";
                return _TemperatureRunningStatistics.Minimum.ToString("f2").TrimEnd('0');
            }
        }

        [Category("温度"), DisplayName("峰-峰值")]
        public string TemperaturePpvalue { get; private set; }

        [Category("温度"), DisplayName("算术平均值")]
        public string TemperatureMean
        {
            get
            {
                if (_TemperatureRunningStatistics.Mean.Equals(double.NaN))
                    return "0";
                return _TemperatureRunningStatistics.Mean.ToString("f3").TrimEnd('0');
            }
        }

        #endregion

        #region 极值点

        [Browsable(false)]
        public Tuple<double, double> ExtremePoint { get; set; }

        [Browsable(false)]
        public Tuple<double, double> TemperatureExtremePoint { get; set; }

        #endregion

        #endregion
    }
}