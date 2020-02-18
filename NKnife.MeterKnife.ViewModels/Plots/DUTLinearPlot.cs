using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using NKnife.MeterKnife.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    ///     基础的折线图表, 横轴表示时间，纵轴代表测量值
    /// </summary>
    public class DUTLinearPlot
    {
        private readonly Dictionary<int, LinearAxis> _axisMap = new Dictionary<int, LinearAxis>();
        private readonly Dictionary<int, bool> _axisFirstMap = new Dictionary<int, bool>();
        private readonly PlotModel _plotModel = new PlotModel();
        private readonly DateTimeAxis _timeAxis = new DateTimeAxis();

        /// <summary>
        ///     构造函数：基础的折线图表, 横轴表示时间，纵轴代表测量值
        /// </summary>
        public DUTLinearPlot(PlotTheme plotTheme, string title = "")
        {
            PlotTheme = plotTheme;

            _plotModel.PlotAreaBackground = ToOxyColor(plotTheme.AreaBackground);
            _plotModel.Title = title;
            _plotModel.TitleFontSize = 12F;

            _timeAxis.TextColor = ToOxyColor(Color.Lavender);
            _timeAxis.MajorGridlineColor = ToOxyColor(plotTheme.BottomAxisGridLineColors.Major);
            _timeAxis.MinorGridlineColor = ToOxyColor(plotTheme.BottomAxisGridLineColors.Minor);
            _timeAxis.MajorGridlineStyle = LineStyle.Dash;
            _timeAxis.MinorGridlineStyle = LineStyle.Dot;
            _timeAxis.MaximumPadding = 0;
            _timeAxis.MinimumPadding = 0;
            _timeAxis.Position = AxisPosition.Bottom;
            _timeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");

            _plotModel.Axes.Add(_timeAxis);
        }

        public string Title
        {
            get => _plotModel.Title;
            set => _plotModel.Title = value;
        }

        public PlotTheme PlotTheme { get; set; }

        public PlotModel GetPlotModel()
        {
            return _plotModel;
        }

        /// <summary>
        ///     增加测量数据
        /// </summary>
        /// <param name="number">数据渠道编号</param>
        /// <param name="time">测量时间</param>
        /// <param name="value">测量数据</param>
        public void AddValues(int number, DateTime time, double value)
        {
            var axis = _axisMap[number];
            //先根据测量数据调整纵轴的值的范围
            if (_axisFirstMap[number])
            {
                UpdateRange(value, axis, PlotTheme.YSpaceLevel, true);
                _axisFirstMap[number] = false;
            }
            else
            {
                UpdateRange(value, axis, PlotTheme.YSpaceLevel);
            }

            //向数据线上添加测量数据点
            var points = DateTimeAxis.CreateDataPoint(time, value);
            ((LineSeries) _plotModel.Series[number]).Points.Add(points);
        }

        /// <summary>
        ///     增加数据线
        /// </summary>
        /// <param name="styles">数据线的样式</param>
        public void SetSeries(params DUTSeriesStyle[] styles)
        {
            _plotModel.Series.Clear();
            for (var index = 0; index < styles.Length; index++)
            {
                var style = styles[index];
                var series = new LineSeries
                {
                    LineStyle = style.LineStyle,
                    Color = ToOxyColor(style.Color),
                    StrokeThickness = style.Thickness,
                    TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}"
                };
                style.Axis.AxislineColor = ToOxyColor(style.Color);
                style.Axis.TextColor = ToOxyColor(style.Color);
                if (index > 0)
                {
                    style.Axis.MinorGridlineStyle = LineStyle.None;
                    style.Axis.MajorGridlineStyle = LineStyle.None;
                }
                series.YAxisKey = style.Axis.Key;

                _axisMap.Add(index, style.Axis);
                _axisFirstMap.Add(index, true);

                _plotModel.Axes.Add(style.Axis);
                _plotModel.Series.Add(series);
            }
        }

        /// <summary>
        ///     根据当前测量值更新纵轴的显示区域
        /// </summary>
        /// <param name="value">当前测量值</param>
        /// <param name="axis">LinearAxis的相对值</param>
        /// <param name="ySpaceLevel">Y轴上下的留白级别</param>
        /// <param name="isFirst">是第一个数据</param>
        public static void UpdateRange(double value, Axis axis, short ySpaceLevel, bool isFirst = false)
        {
            var ys = ySpaceLevel * 2;
            if (isFirst) //当第一个数据时，做一些常规处理
            {
                var offset = Math.Abs(value) > 0 ? GetOffset(value) : 0.00000001;
                axis.Maximum = value + offset;
                axis.Minimum = value - offset;
                return;
            }

            if (value >= axis.Maximum)
            {
                var min = axis.Minimum;
                var max = value;
                var offset = Math.Abs((max - min) / ys);
                axis.Maximum = value + offset;
            }
            else if (value <= axis.Minimum)
            {
                var min = value;
                var max = axis.Maximum;
                var offset = Math.Abs((max - min) / ys);
                axis.Minimum = value - offset;
            }
        }

        public static double GetOffset(double value)
        {
            var precision = GetPrecision(value);
            var offset = GetMinPrecisionValue(precision);
            return offset;
        }

        /// <summary>
        ///     获取小数的精度
        /// </summary>
        public static int GetPrecision(double value)
        {
            var strValue = value.ToString(CultureInfo.InvariantCulture);
            if (!strValue.Contains("."))
                return 0;
            var maxLength = strValue.Length;
            var index = strValue.IndexOf(".", StringComparison.Ordinal);
            return maxLength - 1 - index;
        }

        /// <summary>
        ///     获取指定小数精度的最小值
        /// </summary>
        /// <param name="precision">指定小数精度</param>
        public static double GetMinPrecisionValue(int precision)
        {
            return Math.Pow(10, -(precision+1));
        }

        public static OxyColor ToOxyColor(Color color)
        {
            return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}