using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace NKnife.MeterKnife.ViewModels.Plots
{
    /// <summary>
    /// 基础的折线图表, 横轴表示时间，纵轴代表测量值
    /// </summary>
    public class DUTLinearPlot
    {
        private bool _isFirst = true;
        private readonly PlotModel _plotModel = new PlotModel();
        private readonly DateTimeAxis _timeAxis = new DateTimeAxis();
        private readonly Dictionary<int, (LinearAxis, double, double)> _axisMap = new Dictionary<int, (LinearAxis, double, double)>();

        /// <summary>
        /// 构造函数：基础的折线图表, 横轴表示时间，纵轴代表测量值
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

        public PlotModel GetPlotModel()
        {
            return _plotModel;
        }

        public string Title
        {
            get => _plotModel.Title;
            set => _plotModel.Title = value;
        }

        public PlotTheme PlotTheme { get; set; }

        /// <summary>
        /// 增加测量数据
        /// </summary>
        /// <param name="number">数据渠道编号</param>
        /// <param name="time">测量时间</param>
        /// <param name="value">测量数据</param>
        public void AddValues(int number, DateTime time, double value)
        {
            var axis = _axisMap[number];
            //先根据测量数据调整纵轴的值的范围
            var pair = UpdateRange(value, ref _isFirst, ref axis.Item2, ref axis.Item3);
            axis.Item1.Minimum = pair.Item1;
            axis.Item1.Maximum = pair.Item2;
            //向数据线上添加测量数据点
            var points = DateTimeAxis.CreateDataPoint(time, value);
            ((LineSeries) _plotModel.Series[number]).Points.Add(points);
        }

        /// <summary>
        /// 增加数据线
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
                _axisMap.Add(index, (style.Axis, 0, 0));
                _plotModel.Axes.Add(style.Axis);
                _plotModel.Series.Add(series);
            }
        }

        /// <summary>
        /// 根据当前测量值更新纵轴的显示区域
        /// </summary>
        /// <param name="value">当前测量值</param>
        /// <param name="isFirst">是否是第一个数据</param>
        /// <param name="max">纵值的最大数据</param>
        /// <param name="min">纵值的最小数据</param>
        /// <returns></returns>
        public static (double, double) UpdateRange(double value, ref bool isFirst, ref double max, ref double min)
        {
            if (isFirst) //当第一个数据时，做一些常规处理
            {
                var precision = GetPrecision(value);
                var offset = GetMinPrecisionValue(precision);
                max = value + offset;
                min = value - offset;
                isFirst = false;
                return (min, max);
            }
            if (value > max)
                max = value;
            else if (value < min)
                min = value;
            var r = Math.Abs(max - min) / 8;
            return (min - r, max + r);
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

        public static OxyColor ToOxyColor(Color color)
        {
            return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}