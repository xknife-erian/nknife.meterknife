using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;
using MathNet.Numerics.Statistics;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;

namespace MerterKnife.Common.Winforms.Plots
{
    /// <summary>
    /// 标准方差趋势图
    /// </summary>
    public class StandardDeviationPlot : DataPlot
    {
        private static readonly ILog _logger = LogManager.GetLogger<StandardDeviationPlot>();

        public override string ValueHead
        {
            get { return "standard_deviation"; }
        }

        protected override PlotModel BuildPlotModel()
        {
            _PlotModel.Title = "标准方差趋势图";
            return base.BuildPlotModel();
        }

        public override void Update(FiguredData fd)
        {
            DataTable table = fd.DataSet.Tables[1];
            int count = fd.DataSet.Tables[1].Rows.Count - 1;
            var valueList = new List<double>();
            for (int i = 0; i < count; i++)
            {
                var time = (DateTime) table.Rows[i]["datetime"];
                var standardDeviation = (double) table.Rows[i]["standard_deviation"]*100*10000;
                double value = 0;
                if (double.TryParse(standardDeviation.ToString(), out value))
                {
                    if (Math.Abs(value) <= 0)
                        continue;
                    valueList.Add(value);
                    DataPoint dataPoint = DateTimeAxis.CreateDataPoint(time, value);
                    _Series.Points.Add(dataPoint);
                }
            }
            RunningStatistics rs = new RunningStatistics();
            rs.PushRange(valueList);
            var max = rs.Maximum;
            var min = rs.Minimum;
            var rms = ArrayStatistics.RootMeanSquare(valueList.ToArray());

            var lineRms = new LineAnnotation();
            lineRms.Type = LineAnnotationType.Horizontal;
            lineRms.Y = rms;
            lineRms.Color = OxyColors.OrangeRed;
            lineRms.ClipByXAxis = false;
            lineRms.Text = string.Format((string) "Root Mean Square: {0}ppm", (object) rms.ToString("0.00000").TrimEnd('0'));
            _PlotModel.Annotations.Add(lineRms);

            var lineMax = new LineAnnotation();
            lineMax.Type = LineAnnotationType.Horizontal;
            lineMax.Y = max;
            lineMax.Color = OxyColors.BlueViolet;
            lineMax.ClipByXAxis = false;
            lineMax.Text = string.Format((string) "Maximum: {0}ppm", (object) max.ToString("0.00000").TrimEnd('0'));
            _PlotModel.Annotations.Add(lineMax);

            var lineMin = new LineAnnotation();
            lineMin.Type = LineAnnotationType.Horizontal;
            lineMin.Y = min;
            lineMin.Color = OxyColors.BlueViolet;
            lineMin.ClipByXAxis = false;
            lineMin.Text = string.Format((string) "Minimum: {0}ppm", (object) min.ToString("0.00000").TrimEnd('0'));
            _PlotModel.Annotations.Add(lineMin);

            this.ThreadSafeInvoke(() =>
            {
                UpdateRange(fd);
                _Series.PlotModel.InvalidatePlot(true);
            });
        }

        protected override OxyColor GetAreaColor()
        {
            return OxyColor.FromArgb(255, 245, 245, 255);
        }

        public override void Clear()
        {
            _PlotModel.Annotations.Clear();
            base.Clear();
        }

        protected override void UpdateRange(FiguredData fd)
        {
            DataTable table = fd.DataSet.Tables[1];
            if (table.Rows.Count <= 0)
                return;
            double max = table.AsEnumerable().Select(row => row.Field<double>(ValueHead)).Max()*1000000;
            double min = table.AsEnumerable().Select(row => row.Field<double>(ValueHead)).Min()*1000000;
            double offset = (Math.Abs(max - min))/4;
            _logger.Debug(string.Format("Max:{0}, Min:{1}, offset:{2}", max, min, offset));
            if (Math.Abs(offset) > 0)
            {
                _LeftAxis.Maximum = max + offset;
                _LeftAxis.Minimum = min - offset;
            }
        }
    }
}