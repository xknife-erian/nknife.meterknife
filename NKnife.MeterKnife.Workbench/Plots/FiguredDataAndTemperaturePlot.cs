using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace NKnife.MeterKnife.Workbench.Plots
{
    public class FiguredDataAndTemperaturePlot : DataPlot
    {
        protected LineSeries _TemperatureSeries = new LineSeries();
        protected LinearAxis _TemperatureAxis = new LinearAxis();

        public override string ValueHead
        {
            get { return "value"; }
        }

        protected override PlotModel BuildPlotModel()
        {
            _DataPlotModel.PlotAreaBackground = GetAreaColor();

            _TimeAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _TimeAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _TimeAxis.MajorGridlineStyle = LineStyle.Solid;
            _TimeAxis.MinorGridlineStyle = LineStyle.Dot;
            _TimeAxis.MaximumPadding = 0;
            _TimeAxis.MinimumPadding = 0;
            _TimeAxis.Position = AxisPosition.Bottom;
            _TimeAxis.LabelFormatter = d => DateTimeAxis.ToDateTime(d).ToString("HH:mm:ss");
            _DataPlotModel.Axes.Add(_TimeAxis);

            _DataAxis.MajorGridlineColor = OxyColor.FromArgb(25, 0, 0, 90);
            _DataAxis.MinorGridlineColor = OxyColor.FromArgb(15, 0, 0, 90);
            _DataAxis.MajorGridlineStyle = LineStyle.Solid;
            _DataAxis.MinorGridlineStyle = LineStyle.Dot;
            _DataAxis.MaximumPadding = 0;
            _DataAxis.MinimumPadding = 0;
            _DataAxis.Maximum = 15;
            _DataAxis.Minimum = 5;
            _DataAxis.Position = AxisPosition.Left;
            _DataPlotModel.Axes.Add(_DataAxis);

            _TemperatureAxis.MaximumPadding = 0;
            _TemperatureAxis.MinimumPadding = 0;
            _TemperatureAxis.Maximum = 15;
            _TemperatureAxis.Minimum = 5;
            _TemperatureAxis.Key = "temperature";
            _TemperatureAxis.Position = AxisPosition.Right;
            _DataPlotModel.Axes.Add(_TemperatureAxis);

            _DataSeries.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _DataSeries.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            _DataSeries.StrokeThickness = 2.6;

            _TemperatureSeries.YAxisKey = "temperature";
            _TemperatureSeries.Color = OxyColors.Red;
            _TemperatureSeries.MarkerFill = OxyColors.Red;
            _TemperatureSeries.StrokeThickness = 1.5;

            _DataPlotModel.Series.Add(_TemperatureSeries);
            _DataPlotModel.Series.Add(_DataSeries);

            return _DataPlotModel;
        }

        protected override void UpdateRange(FiguredData fd)
        {
            if (fd.ExtremePoint != null)
            {
                var max = fd.ExtremePoint.Item1;
                var min = fd.ExtremePoint.Item2;
                if (Math.Abs(max) > 0 && Math.Abs(min) > 0)
                {
                    double j = (Math.Abs(max - min)) / 8;
                    if (Math.Abs(j) > 0)
                    {
                        _DataAxis.Maximum = max + j;
                        _DataAxis.Minimum = min - j;
                    }
                }
            }
            var tempMax = fd.TemperatureExtremePoint.Item1;
            var tempMin = fd.TemperatureExtremePoint.Item2;
            if (Math.Abs(tempMax - tempMin) <= 0)
            {
                _TemperatureAxis.Maximum = tempMax + 0.01;
                _TemperatureAxis.Minimum = tempMin - 0.01;
            }
            else if (Math.Abs(tempMax) > 0 && Math.Abs(tempMin) > 0)
            {
                var j = (Math.Abs(tempMax - tempMin)) / 6;
                if (Math.Abs(j) > 0)
                {
                    _TemperatureAxis.Maximum = tempMax + j;
                    _TemperatureAxis.Minimum = tempMin - j;
                }
            }
        }

        public override void Update(FiguredData fd)
        {
            var row = fd.DataSet.Tables[1].AsEnumerable().Last();
            var time = DateTime.Now;
            var data = (double)(row == null ? 0 : row["value"]);
            var dataPoint = DateTimeAxis.CreateDataPoint(time, double.Parse(data.ToString()));

            var temperature = (double)(row == null ? 0 : row["temperature"]);
            var temperaturePoint = DateTimeAxis.CreateDataPoint(time, temperature);

            _DataPlotModel.Title = data.ToString();
            _DataPlotModel.Subtitle = temperature.ToString();

            _TemperatureSeries.Points.Add(temperaturePoint);
            _DataSeries.Points.Add(dataPoint);

            UpdateRange(fd);

            this.ThreadSafeInvoke(() =>
            {
                _TemperatureSeries.PlotModel.InvalidatePlot(true);
                _DataSeries.PlotModel.InvalidatePlot(true);
            });
        }

        public override void Clear()
        {
            _TemperatureSeries.Points.Clear();
            _DataPlotModel.Subtitle = " ";
            base.Clear();
        }
    }
}
