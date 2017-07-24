using System;
using System.Globalization;
using NKnife.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MeterKnife.Utils
{
    public class SimpleLinePlot
    {
        private readonly LinearAxis _LeftAxis = new LinearAxis();
        private readonly LineSeries _Series = new LineSeries();
        private readonly DateTimeAxis _TimeAxis = new DateTimeAxis();
        private bool _IsFirst = true;
        private double _Max;
        private double _Min;

        public SimpleLinePlot(string title)
        {
            PlotModel.PlotAreaBackground = AreaColor;
            PlotModel.Title = title;
            PlotModel.TitleFontSize = 12F;

            _LeftAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _LeftAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _LeftAxis.MajorGridlineStyle = LineStyle.Solid;
            _LeftAxis.MinorGridlineStyle = LineStyle.Dot;
            _LeftAxis.MaximumPadding = 0;
            _LeftAxis.MinimumPadding = 0;
            _LeftAxis.Angle = LeftAxisAngle;
            _LeftAxis.Maximum = 3000;
            _LeftAxis.Minimum = 0;
            _LeftAxis.Position = AxisPosition.Left;

            _TimeAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _TimeAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _TimeAxis.MajorGridlineStyle = LineStyle.Solid;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");

            _Series.Color = MainSeriesColor;
            _Series.MarkerFill = OxyColor.FromArgb(255, 24, 45, 6); //(255, 78, 154, 6);
            _Series.MarkerStroke = OxyColors.ForestGreen;
            _Series.StrokeThickness = Thickness;
            _Series.TrackerFormatString = "{1}: {2:HH:mm:ss}\n{3}: {4:0.######}";

            PlotModel.Axes.Add(_LeftAxis);
            PlotModel.Axes.Add(_TimeAxis);
            PlotModel.Series.Add(_Series);
        }

        public PlotModel PlotModel { get; } = new PlotModel();

        public double LeftAxisAngle => 0;

        public OxyColor MainSeriesColor => OxyColor.FromArgb(255, 78, 154, 6);

        public OxyColor AreaColor => OxyColor.FromArgb(255, 245, 255, 245);

        public double Thickness => 2;

        public void Add(double value)
        {
            var pair = UpdateRange(value, ref _IsFirst, ref _Max, ref _Min);
            _LeftAxis.Minimum = pair.First;
            _LeftAxis.Maximum = pair.Second;
            _Series.Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, value));
        }

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