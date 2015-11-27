using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Logging;
using MathNet.Numerics.Statistics;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;

namespace MeterKnife.Common.Winforms.Plots
{
    /// <summary>
    ///     标准方差趋势图
    /// </summary>
    public class StandardDeviationPlot : DataPlot
    {
        private static readonly ILog _logger = LogManager.GetLogger<StandardDeviationPlot>();

        public override string ValueHead
        {
            get { return "standard_deviation"; }
        }

        protected override string GetPlotTitle()
        {
            return "离散系数趋势图";
        }

        public override void Update(FiguredData fd)
        {
            var table = fd.DataSet.Tables[1];
            var count = fd.DataSet.Tables[1].Rows.Count - 1;
            var valueList = new List<double>();
            for (var i = 0; i < count; i++)
            {
                var time = (DateTime) table.Rows[i]["datetime"];
                var standardDeviation = (double) table.Rows[i]["standard_deviation"]*100*10000;
                double value = 0;
                if (double.TryParse(standardDeviation.ToString(), out value))
                {
                    if (Math.Abs(value) <= 0)
                        continue;
                    valueList.Add(value);
                    var dataPoint = DateTimeAxis.CreateDataPoint(time, value);
                    _DataSeries.Points.Add(dataPoint);
                }
            }
            var rs = new RunningStatistics();
            rs.PushRange(valueList);
            var max = rs.Maximum;
            var min = rs.Minimum;
            var array = valueList.ToArray();
            var rms = ArrayStatistics.RootMeanSquare(array);//标准差的均方根
            var sd = ArrayStatistics.PopulationStandardDeviation(array);//标准差的标准差

            _DataPlotModel.Subtitle = sd.ToString("0.0000").TrimZero();

            var lineRms = new LineAnnotation();
            lineRms.Type = LineAnnotationType.Horizontal;
            lineRms.Y = rms;
            lineRms.Color = OxyColors.OrangeRed;
            lineRms.ClipByXAxis = false;
            lineRms.Text = string.Format("RMS: {0}ppm", rms.ToString("0.0000").TrimZero());
            _DataPlotModel.Annotations.Add(lineRms);

            var lineMax = new LineAnnotation();
            lineMax.Type = LineAnnotationType.Horizontal;
            lineMax.Y = max;
            lineMax.Color = OxyColors.BlueViolet;
            lineMax.ClipByXAxis = false;
            lineMax.Text = string.Format("MAX: {0}ppm", max.ToString("0.0000").TrimZero());
            _DataPlotModel.Annotations.Add(lineMax);

            var lineMin = new LineAnnotation();
            lineMin.Type = LineAnnotationType.Horizontal;
            lineMin.Y = min;
            lineMin.Color = OxyColors.BlueViolet;
            lineMin.ClipByXAxis = false;
            lineMin.Text = string.Format("MIN: {0}ppm", min.ToString("0.0000").TrimZero());
            _DataPlotModel.Annotations.Add(lineMin);

            this.ThreadSafeInvoke(() =>
            {
                UpdateRange(fd);
                _DataSeries.PlotModel.InvalidatePlot(true);
            });
        }

        protected override OxyColor GetAreaColor()
        {
            return OxyColor.FromArgb(255, 245, 245, 255);
        }

        protected override double GetThickness()
        {
            return 2.5;
        }

        public override void Clear()
        {
            _DataPlotModel.Annotations.Clear();
            _DataSeries.Points.Clear();
            this.ThreadSafeInvoke(() => _DataSeries.PlotModel.InvalidatePlot(true));
        }

        protected override void UpdateRange(FiguredData fd)
        {
            var table = fd.DataSet.Tables[1];
            if (table.Rows.Count <= 0)
                return;
            var max = table.AsEnumerable().Select(row => row.Field<double>(ValueHead)).Max()*1000000;
            var min = table.AsEnumerable().Select(row => row.Field<double>(ValueHead)).Min()*1000000;

            var offset = (Math.Abs(max - min))/6;

            _logger.Debug(string.Format("Max:{0}, Min:{1}, offset:{2}", max, min, offset));
            if (Math.Abs(offset) > 0)
            {
                _DataAxis.Maximum = max + offset;
                _DataAxis.Minimum = min - offset;
            }
        }
    }
}