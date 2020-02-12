using System;
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
    public class PlainPolyLinePlot
    {
        private readonly PlotModel _plotModel = new PlotModel();
        private readonly LinearAxis _leftAxis = new LinearAxis();
        private readonly DateTimeAxis _timeAxis = new DateTimeAxis();
        private bool _isFirst = true;
        private double _max;
        private double _min;

        /// <summary>
        /// 构造函数：基础的折线图表, 横轴表示时间，纵轴代表测量值
        /// </summary>
        public PlainPolyLinePlot(PlotTheme plotTheme, string title = "")
        {
            PlotTheme = plotTheme;

            _plotModel.PlotAreaBackground = ToOxyColor(plotTheme.AreaBackground);
            _plotModel.Title = title;
            _plotModel.TitleFontSize = 12F;

            _leftAxis.TextColor = ToOxyColor(Color.Lavender);
            _leftAxis.MajorGridlineColor = ToOxyColor(plotTheme.LeftAxisGridLineColors.Major);
            _leftAxis.MinorGridlineColor = ToOxyColor(plotTheme.LeftAxisGridLineColors.Minor);
            _leftAxis.MajorGridlineStyle = LineStyle.Dash;
            _leftAxis.MinorGridlineStyle = LineStyle.Dot;
            _leftAxis.MaximumPadding = 0;
            _leftAxis.MinimumPadding = 0;
            _leftAxis.Angle = LeftAxisAngle;
            _leftAxis.Maximum = 220;
            _leftAxis.Minimum = -220;
            _leftAxis.Position = AxisPosition.Left;

            _timeAxis.TextColor = ToOxyColor(Color.Lavender);
            _timeAxis.MajorGridlineColor = ToOxyColor(plotTheme.BottomAxisGridLineColors.Major);
            _timeAxis.MinorGridlineColor = ToOxyColor(plotTheme.BottomAxisGridLineColors.Minor);
            _timeAxis.MajorGridlineStyle = LineStyle.Dash;
            _timeAxis.MinorGridlineStyle = LineStyle.Dot;
            _timeAxis.MaximumPadding = 0;
            _timeAxis.MinimumPadding = 0;
            _timeAxis.Position = AxisPosition.Bottom;
            _timeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");

            var l1 = new LinearAxis
            {
                Position = AxisPosition.Left, 
                AxisDistance = 80, 
                TextColor = ToOxyColor(Color.Lavender)
            };
            _plotModel.Axes.Add(l1);
            var r1 = new LinearAxis
            {
                Position = AxisPosition.Right, 
                AxisDistance = -80, 
                TextColor = ToOxyColor(Color.Lavender)
            };
            _plotModel.Axes.Add(r1);
            _plotModel.Axes.Add(_leftAxis);
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
            var pair = UpdateRange(values, ref _isFirst, ref _max, ref _min);
            _leftAxis.Minimum = pair.Item1;
            _leftAxis.Maximum = pair.Item2;
            //向数据线上添加测量数据点
            DataPoint[] points = new DataPoint[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                points[i] = DateTimeAxis.CreateDataPoint(DateTime.Now, values[i]);
            }
            ((LineSeries)_plotModel.Series[number]).Points.AddRange(points);
        }

        /// <summary>
        /// 增加数据线
        /// </summary>
        /// <param name="styles">数据线的样式</param>
        public void SetSeries(params DUTSeriesStyle[] styles)
        {
            _plotModel.Series.Clear();
            foreach (var style in styles)
            {
                var series = new LineSeries
                {
                    Color = ToOxyColor(style.SeriesStyle.Color),
                    StrokeThickness = style.SeriesStyle.Thickness,
                    TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}"
                };
                _plotModel.Series.Add(series);
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
        protected static (double, double) UpdateRange(double[] values, ref bool isFirst, ref double max, ref double min)
        {
            if (isFirst) //当第一个数据时，做一些常规处理
            {
                var precision = GetPrecision(values[0]);
                var offset = GetMinPrecisionValue(precision);
                max = values[0] + offset;
                min = values[0] - offset;
                isFirst = false;
                return (min, max);
            }
            var x = values.Max();
            var n = values.Min();
            if (x > max)
                max = x;
            else if (n < min)
                min = n;
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