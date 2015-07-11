using System;
using System.Data;
using System.Linq;
using Common.Logging;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;

namespace MeterKnife.Common.Controls.Plots
{
    public class StandardDeviationPlot : DataPlot
    {
        private static readonly ILog _logger = LogManager.GetLogger<StandardDeviationPlot>();

        public override string ValueHead
        {
            get { return "standard_deviation"; }
        }

        public override void Update(FiguredData fd)
        {
            DataTable table = fd.DataSet.Tables[1];
            int count = fd.DataSet.Tables[1].Rows.Count - 1;
            for (int i = 0; i < count; i++)
            {
                var time = (DateTime) table.Rows[i]["datetime"];
                var yzl = (double) table.Rows[i]["standard_deviation"];
                double value = double.Parse(yzl.ToString())*1000000;
                if (Math.Abs(value) <= 0)
                    continue;

                DataPoint dataPoint = DateTimeAxis.CreateDataPoint(time, value);
                _Series.Points.Add(dataPoint);
                _logger.Trace(string.Format("{0} : {1}", time, value));
            }
            this.ThreadSafeInvoke(() =>
            {
                UpdateRange(fd);
                _Series.PlotModel.InvalidatePlot(true);
            });
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