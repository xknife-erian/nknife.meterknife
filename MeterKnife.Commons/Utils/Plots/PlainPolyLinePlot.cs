using System;
using System.Drawing;
using System.Globalization;
using NKnife.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MeterKnife.Utils.Plots
{
    /// <summary>
    /// 基础的折线图表, 横轴表示时间，纵轴代表测量值
    /// </summary>
    public class PlainPolyLinePlot
    {
        private readonly PlotModel _PlotModel = new PlotModel();
        private readonly LinearAxis _LeftAxis = new LinearAxis();
        private readonly LineSeries _Series = new LineSeries();
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
            _LeftAxis.MajorGridlineStyle = LineStyle.Dash;
            _LeftAxis.MinorGridlineStyle = LineStyle.Dot;
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Angle = LeftAxisAngle;
            _LeftAxis.Maximum = 220;
            _LeftAxis.Minimum = -220;
            _LeftAxis.Position = AxisPosition.Left;

            _TimeAxis.TextColor = PlotTheme.ToOxyColor(Color.Lavender);
            _TimeAxis.MajorGridlineStyle = LineStyle.Dash;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");

            _Series.Color = PlotTheme.ToOxyColor(plotTheme.SeriesColor);
            _Series.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6); //(255, 78, 154, 6);
            _Series.MarkerStroke = OxyColors.ForestGreen;
            _Series.StrokeThickness = plotTheme.Thickness;
            _Series.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";

            _PlotModel.Axes.Add(_LeftAxis);
            _PlotModel.Axes.Add(_TimeAxis);
            _PlotModel.Series.Add(_Series);
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

        public PlotTheme PlotTheme { get ; set; }

        /// <summary>
        /// 增加测量数据
        /// </summary>
        /// <param name="value">测量数据</param>
        public void Add(double value)
        {
            //先根据测量数据调整纵轴的值的范围
            var pair = UpdateRange(value, ref _IsFirst, ref _Max, ref _Min);
            _LeftAxis.Minimum = pair.First;
            _LeftAxis.Maximum = pair.Second;
            //向数据线上添加测量数据点
            _Series.Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, value));
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
            if (isFirst)
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
        private static int GetPrecision(double value)
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
        private static double GetMinPrecisionValue(int precision)
        {
            switch (precision)
            {
                case 1:
                    return 0.1;
                case 2:
                    return 0.01;
                case 3:
                    return 0.001;
                case 4:
                    return 0.0001;
                case 5:
                    return 0.00001;
                case 6:
                    return 0.000001;
                case 7:
                    return 0.0000001;
                case 8:
                    return 0.00000001;
                case 9:
                    return 0.000000001;
                case 10:
                    return 0.0000000001;
                case 11:
                    return 0.00000000001;
                case 12:
                    return 0.000000000001;
                case 13:
                    return 0.0000000000001;
                case 14:
                    return 0.00000000000001;
                case 15:
                    return 0.000000000000001;
                case 16:
                    return 0.0000000000000001;
                default:
                    return 1;
            }
        }

    }
}