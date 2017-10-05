using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Common.Logging;
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
        private static readonly ILog _logger = LogManager.GetLogger<PlainPolyLinePlot>();
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

        /// <summary>
        /// 增加测量数据
        /// </summary>
        /// <param name="number">数据渠道编号</param>
        /// <param name="values">测量数据</param>
        public void AddValues(int number, params double[] values)
        {
            //先根据测量数据调整纵轴的值的范围
            var pair = UpdateRange(values, ref _IsFirst, ref _Max, ref _Min);
            _LeftAxis.Minimum = pair.First;
            _LeftAxis.Maximum = pair.Second;
            //向数据线上添加测量数据点
            DataPoint[] points = new DataPoint[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                points[i] = DateTimeAxis.CreateDataPoint(DateTime.Now, values[i]);
            }
            ((LineSeries)_PlotModel.Series[number]).Points.AddRange(points);
        }

        /// <summary>
        /// 增加数据线
        /// </summary>
        /// <param name="styles">数据线的样式</param>
        public void SetSeries(params PlotSeriesStyleSolution.ExhibitSeriesStyle[] styles)
        {
            _PlotModel.Series.Clear();
            foreach (var style in styles)
            {
                var series = new LineSeries
                {
                    Color = PlotTheme.ToOxyColor(style.SeriesStyle.Color),
                    MarkerFill = PlotTheme.ToOxyColor(style.SeriesStyle.MarkerFillColor),
                    MarkerStroke = PlotTheme.ToOxyColor(style.SeriesStyle.MarkerStrokeColor),
                    StrokeThickness = style.SeriesStyle.Thickness,
                    TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}"
                };
                _PlotModel.Series.Add(series);
                _logger.Trace($"{_PlotModel.Series.Count}:{series.Color}");
            }
        }

        /// <summary>
        /// 根据当前测量值更新纵轴的显示区域
        /// </summary>
        /// <param name="values">当前测量值</param>
        /// <param name="isFirst">是否是第一个数据</param>
        /// <param name="max">纵值的最大数据</param>
        /// <param name="min">纵值的最小数据</param>
        /// <returns></returns>
        protected static Pair<double, double> UpdateRange(double[] values, ref bool isFirst, ref double max, ref double min)
        {
            if (isFirst) //当第一个数据时，做一些常规处理
            {
                var precision = GetPrecision(values[0]);
                var offset = GetMinPrecisionValue(precision);
                max = values[0] + offset;
                min = values[0] - offset;
                isFirst = false;
                return Pair<double, double>.Build(min, max);
            }
            var x = values.Max();
            var n = values.Min();
            if (x > max)
                max = x;
            else if (n < min)
                min = n;
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