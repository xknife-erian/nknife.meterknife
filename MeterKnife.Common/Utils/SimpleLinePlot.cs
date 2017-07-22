using System;
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
        private float _Max;
        private float _Min;

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

        public void Add(float value)
        {
            var pair = UpdateRange(value, ref _IsFirst, ref _Max, ref _Min);
            _LeftAxis.Minimum = pair.First;
            _LeftAxis.Maximum = pair.Second;
            _Series.Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, value));
        }

        protected static Pair<float, float> UpdateRange(float value, ref bool isFirst, ref float max, ref float min)
        {
            if (isFirst)
            {
                if (value < 2)
                {
                    max = (float) (value + 0.1);
                    min = (float) (value - 0.1);
                }
                else
                {
                    max = value + 1;
                    min = value - 1;
                }
                isFirst = false;
                return Pair<float, float>.Build(min, max);
            }
            if (value > max)
                max = value;
            else if (value < min)
                min = value;
            var r = Math.Abs(max - min) / 8;
            return Pair<float, float>.Build(min - r, max + r);
        }
    }
}