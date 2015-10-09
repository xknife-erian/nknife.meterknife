using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using MathNet.Numerics.Statistics;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MeterKnife.Common.DataModels
{
    public class FiguredData : ICollectSource
    {
        public const string DATETIME = "datetime";
        public const string VALUE = "value";
        public const string TEMPERATURE = "temperature";
        public const string STANDARD_DEVIATION = "standard_deviation";

        protected readonly ITemperatureService _TempService = DI.Get<ITemperatureService>();
        protected double _CurrentTemperature;
        protected DataSet _DataSet = new DataSet();
        protected string _DecimalDigit = "f6";
        protected string _PpmDecimalDigit = "f3";
        protected RunningStatistics _RunningStatistics = new RunningStatistics();
        protected int _SampleRange = 50;
        protected RunningStatistics _TemperatureRunningStatistics = new RunningStatistics();
        protected List<double> _Values = new List<double>();

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
                return _RunningStatistics.Maximum.ToString(_DecimalDigit);
            }
        }

        [Category("数据"), DisplayName("最小值")]
        public string Minimum
        {
            get
            {
                if (_RunningStatistics.Minimum.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.Minimum.ToString(_DecimalDigit);
            }
        }

        [Category("数据"), DisplayName("峰峰值")]
        public string Ppvalue { get; private set; }

        [Category("数据"), DisplayName("算术平均值")]
        public string Mean
        {
            get
            {
                if (_RunningStatistics.Mean.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.Mean.ToString(_DecimalDigit);
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
                return _RunningStatistics.PopulationKurtosis.ToString(_DecimalDigit);
            }
        }

        [Category("数据"), DisplayName("偏度")]
        public string PopulationSkewness
        {
            get
            {
                if (_RunningStatistics.PopulationSkewness.Equals(double.NaN))
                    return "0";
                return _RunningStatistics.PopulationSkewness.ToString(_DecimalDigit);
            }
        }

        #endregion

        #region 样本

        [Category("样本"), DisplayName("标准差")]
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

        [Category("样本"), DisplayName("四分位距")]
        public string SampleInterquartileRangeInplace { get; private set; }

        [Category("样本"), DisplayName("高四分位")]
        public string SampleUpperQuartile { get; private set; }

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
                return _TemperatureRunningStatistics.Maximum.ToString("f2");
            }
        }

        [Category("温度"), DisplayName("最小值")]
        public string TemperatureMinimum
        {
            get
            {
                if (_TemperatureRunningStatistics.Minimum.Equals(double.NaN))
                    return "0";
                return _TemperatureRunningStatistics.Minimum.ToString("f2");
            }
        }

        [Category("温度"), DisplayName("算术平均值")]
        public string TemperatureMean
        {
            get
            {
                if (_TemperatureRunningStatistics.Mean.Equals(double.NaN))
                    return "0";
                return _TemperatureRunningStatistics.Mean.ToString("f3");
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

        public FiguredData()
        {
            var baseTable = new DataTable("BaseInfomation");
            baseTable.Columns.Add(new DataColumn("Key", typeof (string)));
            baseTable.Columns.Add(new DataColumn("Value", typeof (string)));
            _DataSet.Tables.Add(baseTable);

            var collectTable = new DataTable("CollectData");
            collectTable.Columns.Add(new DataColumn("datetime", typeof (DateTime)));
            collectTable.Columns.Add(new DataColumn("value", typeof (double)));
            collectTable.Columns.Add(new DataColumn("temperature", typeof (double)));
            collectTable.Columns.Add(new DataColumn("standard_deviation", typeof (double)));
            _DataSet.Tables.Add(collectTable);
        }

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
        public bool HasData
        {
            get { return _DataSet.Tables[1].Rows.Count > 0; }
        }

        public void Clear()
        {
            _DataSet.Tables[1].Rows.Clear();
            _DataSet.AcceptChanges();
            _CurrentTemperature = 0;
            _RunningStatistics = new RunningStatistics();
            _TemperatureRunningStatistics = new RunningStatistics();
            _Values.Clear();
        }

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

        public virtual void Add(double value)
        {
            string s = value.ToString();
            int n = s.Length - s.IndexOf('.') - 1;
            _DecimalDigit = string.Format("f{0}", n);
            _PpmDecimalDigit = string.Format("f{0}", ((uint) (n/2)) + 1);

            _RunningStatistics.Push(value);

            if (_Values.Count >= _SampleRange)
                _Values.RemoveAt(0);
            _Values.Add(value);
            var ds = new DescriptiveStatistics(_Values);
            SampleKurtosis = ds.Kurtosis.ToString(_DecimalDigit);
            SampleSkewness = ds.Skewness.ToString(_DecimalDigit);

            double[] array = _Values.ToArray();
            SampleMean = ArrayStatistics.Mean(array).ToString(_DecimalDigit);
            double sampleStandardDeviation = ArrayStatistics.PopulationStandardDeviation(array);
            SampleStandardDeviation = GetPpmValue(sampleStandardDeviation);
            SampleRootMeanSquareValue = ArrayStatistics.RootMeanSquare(array).ToString(_DecimalDigit);

            Array.Sort(array);
            SampleInterquartileRangeInplace = GetPpmValue(SortedArrayStatistics.InterquartileRange(array));
            SampleMedianInplace = SortedArrayStatistics.Median(array).ToString(_DecimalDigit);
            SampleLowerQuartile = SortedArrayStatistics.LowerQuartile(array).ToString(_DecimalDigit);
            SampleUpperQuartile = SortedArrayStatistics.UpperQuartile(array).ToString(_DecimalDigit);

            Ppvalue = GetPpmValue(Math.Abs(_RunningStatistics.Maximum - _RunningStatistics.Minimum)); //峰峰值

            UpdateTemperature();

            ExtremePoint = new Tuple<double, double>(_RunningStatistics.Maximum, _RunningStatistics.Minimum);
            TemperatureExtremePoint = new Tuple<double, double>(_TemperatureRunningStatistics.Maximum, _TemperatureRunningStatistics.Minimum);

            _DataSet.Tables[1].Rows.Add(DateTime.Now, value, _CurrentTemperature, sampleStandardDeviation);
            //触发数据源发生变化
            OnReceviedCollectData(new CollectDataEventArgs(Meter, CollectData.Build(DateTime.Now, value, _CurrentTemperature)));
        }

        protected string GetPpmValue(double value)
        {
            double d = value*1000000;
            return string.Format("{0} ppm", d.ToString(_PpmDecimalDigit));
        }

        protected virtual void UpdateTemperature()
        {
            _CurrentTemperature = _TempService.TemperatureValues[0];
            _TemperatureRunningStatistics.Push(_CurrentTemperature);
        }

        protected virtual void OnReceviedCollectData(CollectDataEventArgs e)
        {
            EventHandler<CollectDataEventArgs> handler = ReceviedCollectData;
            if (handler != null)
                handler(this, e);
        }
    }
}