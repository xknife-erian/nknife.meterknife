using System;
using NKnife.MeterKnife.Common.Domain;
using OxyPlot;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.Workbench.Plots
{
    public class TemperatureDataPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "temperature"; }
        }

        protected override double GetLeftAxisAngle()
        {
            return 0;
        }

        protected override OxyColor GetMainSeriesColor()
        {
            return OxyColor.FromArgb(255, 86, 96, 225);
        }

        protected override void UpdateRange(FiguredData fd)
        {
            UpdateRange(fd.TemperatureExtremePoint.Item1, fd.TemperatureExtremePoint.Item2);
        }

        public override void Update(FiguredData fd)
        {
            var yzl = SelectValue(fd);
            var value = double.Parse(yzl.ToString());

            _DataPlotModel.Title = value.ToString();

            var dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _DataSeries.Points.Add(dataPoint);
            _DataSeries.PlotModel.InvalidatePlot(true);

            UpdateRange(fd);
        }
    }
}
