using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MeterKnife.Common.DataModels;
using OxyPlot.Axes;

namespace MeterKnife.Common.Controls.Plots
{
    public class StandardDeviationPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "standard_deviation"; }
        }

        public override void Update(FiguredData fd)
        {
            var table = fd.DataSet.Tables[1];
            var count = fd.DataSet.Tables[1].Rows.Count - 1;
            for (int i = 0; i < count; i++)
            {
                var time = (DateTime) table.Rows[count]["datetime"];

                var yzl = SelectValue(fd);
                var value = double.Parse(yzl.ToString())*1000000;
                if (Math.Abs(value) <= 0)
                    continue;

                var dataPoint = DateTimeAxis.CreateDataPoint(time, value);
                _Series.Points.Add(dataPoint);
            }
            this.ThreadSafeInvoke(() =>
            {
                _Series.PlotModel.InvalidatePlot(true);
                UpdateRange(fd);
            });
        }

        protected override void UpdateRange(FiguredData fd)
        {
            var table = fd.DataSet.Tables[1];
            double max = table.AsEnumerable().Select(row => row.Field<double>(ValueHead)).Max()*1000000;
            var list = new List<double>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var v = row.Field<double>(ValueHead);
                if (Math.Abs(v) > 0)
                    list.Add(v);
            }
            list.Sort();
            double min = list[0]*1000000;
            double j = (Math.Abs(max - min))/4;
            if (Math.Abs(j) > 0)
            {
                _LeftAxis.Maximum = max + j;
                _LeftAxis.Minimum = min - j;
            }
        }
    }
}
