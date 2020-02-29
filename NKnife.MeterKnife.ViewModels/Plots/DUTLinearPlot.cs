using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NLog;
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
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly PlotModel _plotModel = new PlotModel();
        private readonly DateTimeAxis _timeAxis = new DateTimeAxis();

        private readonly Dictionary<int, PlotInfo> _plotInfoMap = new Dictionary<int, PlotInfo>();
        // private readonly Dictionary<int, LinearAxis> _axisMap = new Dictionary<int, LinearAxis>();
        // private readonly Dictionary<int, bool> _axisFirstMap = new Dictionary<int, bool>();
        // /// <summary>
        // /// 数轴的极值
        // /// </summary>
        // private readonly Dictionary<int, (double, double)> _axisExtremumMap = new Dictionary<int, (double, double)>();
        /// <summary>
        /// Y轴区间，图表上下方留白空间的级别，1-12级，1级留白最大，12级留白最小。默认5级。
        /// </summary>
        private readonly short _ySpaceLevel;

        /// <summary>
        ///     构造函数：基础的折线图表, 横轴表示时间，纵轴代表测量值
        /// </summary>
        public DUTLinearPlot(PlotTheme plotTheme, short ySpaceLevel, string title = "")
        {
            _ySpaceLevel = ySpaceLevel;

            PlotTheme = plotTheme;

            _plotModel.PlotAreaBackground = ToOxyColor(plotTheme.AreaBackground);
            _plotModel.Title = title;
            _plotModel.TitleFontSize = 12F;

            _timeAxis.TextColor = ToOxyColor(SystemColors.ControlText);
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
        /// <param name="value">测量数据: Item1是数据渠道编号，Item2是测量时间，Item3是测量数据值</param>
        public void AddValue((int, DateTime, double) value)
        {
            var plotInfo = _plotInfoMap[value.Item1];
            var axis = plotInfo.Axis;
            //先根据测量数据调整纵轴的值的范围
            if (plotInfo.IsFirst)
            {
                UpdateRange(value.Item1, value.Item3, axis, true);
                plotInfo.IsFirst = false;
            }
            else
            {
                UpdateRange(value.Item1, value.Item3, axis);
            }

            //向数据线上添加测量数据点
            var points = DateTimeAxis.CreateDataPoint(value.Item2, value.Item3);
            ((LineSeries) _plotModel.Series[value.Item1]).Points.Add(points);
        }

        public void AddValues(int index, MeasureData[] measureDatas)
        {
            var list = measureDatas.Select(data => DateTimeAxis.CreateDataPoint(data.Time, data.Data)).ToList();
            var axis = _plotInfoMap[index].Axis;
            var series = _plotInfoMap[index].Series;
            series.Points.AddRange(list);
            var max = (from value in measureDatas select value.Data).Max();
            var min = (from value in measureDatas select value.Data).Min();

            var dataRange = max - min;
            double offset = 0;
            try
            {
                offset = Math.Abs(dataRange / _ySpaceLevel);
            }
            catch (Exception e)
            {
                _Logger.Warn($"DUTLinearPlot/// Double计算图表留白异常：{e}");
            }

            if (offset > 0)
            {
                try
                {
                    axis.Maximum = max + offset;
                    axis.Minimum = min - offset;
                }
                catch (Exception e)
                {
                    _Logger.Warn($"DUTLinearPlot/// 设置Y轴的极轴有异常: {e}");
                    return;
                }
            }

            _Logger.Info($"向{series}[{series.YAxisKey}]填入{list.Count}个数据。Max:{max}, Min:{min}, Y-max:{axis.Maximum}, Y-min:{axis.Minimum}");
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public void ClearValues()
        {
            foreach (var series in _plotModel.Series)
            {
                ((LineSeries)series).Points.Clear();
            }
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
                style.Axis.TextColor = ToOxyColor(SystemColors.ControlText);
                style.Axis.MajorGridlineColor = ToOxyColor(PlotTheme.LeftAxisGridLineColors.Major);
                style.Axis.MinorGridlineColor = ToOxyColor(PlotTheme.LeftAxisGridLineColors.Minor);
                if (index > 0)
                {
                    style.Axis.MinorGridlineStyle = LineStyle.None;
                    style.Axis.MajorGridlineStyle = LineStyle.None;
                }

                _plotModel.Axes.Add(style.Axis);
                _plotModel.Series.Add(series);
                _plotInfoMap.Add(index, new PlotInfo {Axis = style.Axis, Series = series}); //style.Axis);
                series.YAxisKey = style.Axis.Key;
            }
        }

        /// <summary>
        ///     根据当前测量值更新纵轴的显示区域
        /// </summary>
        /// <param name="index">数据渠道编号</param>
        /// <param name="value">当前测量值</param>
        /// <param name="axis">LinearAxis的相对值</param>
        /// <param name="isFirst">是第一个数据</param>
        public void UpdateRange(int index, double value, Axis axis, bool isFirst = false)
        {
            double min = 0;
            double max = 0;
            double offset = 0;

            if (isFirst) //当第一个数据时，做一些常规处理
            {
                offset = Math.Abs(value) > 0 ? GetOffset(value) : 0.00000001;
                axis.Maximum = value + offset;
                axis.Minimum = value - offset;
                _plotInfoMap[index].AxisMin = value;
                _plotInfoMap[index].AxisMax = value;
                return;
            }

            var extremum = _plotInfoMap[index];
            if (value >= extremum.AxisMax)
            {
                min = extremum.AxisMin;
                max = value;
            }
            else if (value <= extremum.AxisMin)
            {
                min = value;
                max = extremum.AxisMax;
            }
            else
            {
                return;
            }

            _plotInfoMap[index].AxisMin = min;
            _plotInfoMap[index].AxisMax = max;
            offset = Math.Abs((max - min) / _ySpaceLevel);
            axis.Maximum = max + offset;
            axis.Minimum = min - offset;
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

        private class PlotInfo
        {
            public LineSeries Series { get; set; }
            public LinearAxis Axis { get; set; }
            public bool IsFirst { get; set; } = true;
            public double AxisMax { get; set; } = 0;
            public double AxisMin { get; set; } = 0;
        }
    }
}