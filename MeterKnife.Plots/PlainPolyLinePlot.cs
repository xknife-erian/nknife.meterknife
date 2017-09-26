using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using MeterKnife.Models;
using NKnife.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MeterKnife.Plots
{
    /// <summary>
    /// 基础的折线图表, 横轴表示时间，纵轴代表测量值
    /// </summary>
    public class PlainPolyLinePlot
    {
        private readonly PlotModel _PlotModel = new PlotModel();
        private readonly LinearAxis _LeftAxis = new LinearAxis();
        private readonly DateTimeAxis _TimeAxis = new DateTimeAxis();
        private bool _IsFirst = true;
        private double _Max;
        private double _Min;

        /// <summary>
        /// 构造函数：基础的折线图表, 横轴表示时间，纵轴代表测量值
        /// </summary>
        public PlainPolyLinePlot(PlotTheme plotTheme, string title = "")
        {
            PlotTheme = plotTheme;

            _PlotModel.PlotAreaBackground = PlotTheme.ToOxyColor(plotTheme.AreaBackground);
            _PlotModel.Title = title;
            _PlotModel.TitleFontSize = 12F;

            _LeftAxis.TextColor = PlotTheme.ToOxyColor(Color.Lavender);
            _LeftAxis.MajorGridlineColor = PlotTheme.ToOxyColor(plotTheme.LeftAxisGridLineColors.Major);
            _LeftAxis.MinorGridlineColor = PlotTheme.ToOxyColor(plotTheme.LeftAxisGridLineColors.Minor);
            _LeftAxis.MajorGridlineStyle = LineStyle.Dash;
            _LeftAxis.MinorGridlineStyle = LineStyle.Dot;
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Angle = LeftAxisAngle;
            _LeftAxis.Maximum = 220;
            _LeftAxis.Minimum = -220;
            _LeftAxis.Position = AxisPosition.Left;

            _TimeAxis.TextColor = PlotTheme.ToOxyColor(Color.Lavender);
            _TimeAxis.MajorGridlineColor = PlotTheme.ToOxyColor(plotTheme.BottomAxisGridLineColors.Major);
            _TimeAxis.MinorGridlineColor = PlotTheme.ToOxyColor(plotTheme.BottomAxisGridLineColors.Minor);
            _TimeAxis.MajorGridlineStyle = LineStyle.Dash;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");

            _PlotModel.Axes.Add(_LeftAxis);
            _PlotModel.Axes.Add(_TimeAxis);
        }

        public PlotModel GetPlotModel()
        {
            return _PlotModel;
        }

        public string Title
        {
            get => _PlotModel.Title;
            set => _PlotModel.Title = value;
        }

        public double LeftAxisAngle => 0;

        public PlotTheme PlotTheme { get; set; }

        private readonly List<LineSeries> _SeriesList = new List<LineSeries>();

        /// <summary>
        /// 增加测量数据
        /// </summary>
        /// <param name="number">数据渠道编号</param>
        /// <param name="value">测量数据</param>
        public void Add(ushort number, double value)
        {
            if (number > _SeriesList.Count)
                AddSeries(number);
            //先根据测量数据调整纵轴的值的范围
            var pair = UpdateRange(value, ref _IsFirst, ref _Max, ref _Min);
            _LeftAxis.Minimum = pair.First;
            _LeftAxis.Maximum = pair.Second;
            //向数据线上添加测量数据点
            _SeriesList[number-1].Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, value));
        }

        /// <summary>
        /// 增加数据线
        /// </summary>
        /// <param name="number"></param>
        private void AddSeries(ushort number)
        {
            var seriesStyle = PlotTheme.SeriesStyles[number];
            var series = new LineSeries
            {
                Color = PlotTheme.ToOxyColor(seriesStyle.Color),
                MarkerFill = PlotTheme.ToOxyColor(Color.Red),
                MarkerStroke = PlotTheme.ToOxyColor(Color.Red),
                StrokeThickness = seriesStyle.Thickness,
                TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}"
            };
            _SeriesList.Add(series);
            _PlotModel.Series.Add(series);
        }

        /// <summary>
        /// 根据当前测量值更新纵轴的显示区域
        /// </summary>
        /// <param name="value">当前测量值</param>
        /// <param name="isFirst">是否是第一个数据</param>
        /// <param name="max">纵值的最大数据</param>
        /// <param name="min">纵值的最小数据</param>
        /// <returns></returns>
        protected static Pair<double, double> UpdateRange(double value, ref bool isFirst, ref double max, ref double min)
        {
            if (isFirst) //当第一个数据时，做一些常规处理
            {
                var precision = GetPrecision(value);
                var offset = GetMinPrecisionValue(precision);
                max = value + offset;
                min = value - offset;
                isFirst = false;
                return Pair<double, double>.Build(min, max);
            }
            if (value > max)
                max = value;
            else if (value < min)
                min = value;
            var r = Math.Abs(max - min) / 8;
            return Pair<double, double>.Build(min - r, max + r);
        }

        /// <summary>
        /// 获取小数的精度
        /// </summary>
        public static int GetPrecision(double value)
        {
            string strValue = value.ToString(CultureInfo.InvariantCulture);
            if (!strValue.Contains("."))
                return 0;
            int maxLength = strValue.Length;
            int index = strValue.IndexOf(".", StringComparison.Ordinal);
            return maxLength - 1 - index;
        }

        /// <summary>
        /// 获取指定小数精度的最小值
        /// </summary>
        /// <param name="precision">指定小数精度</param>
        public static double GetMinPrecisionValue(int precision)
        {
            return Math.Pow(10, -precision);
        }
    }
}