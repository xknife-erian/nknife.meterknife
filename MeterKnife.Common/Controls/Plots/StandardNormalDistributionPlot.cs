using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot;
using OxyPlot.Series;

namespace MeterKnife.Common.Controls.Plots
{
    public class StandardNormalDistributionPlot
    {
        public StandardNormalDistributionPlot()
        {
//            double[] values = null;
//            const int StepsNumber = 30;
//            // Choosing the size of each bucket
//            double step = (values.Max() - values.Min()) / StepsNumber;
//
//            double mean = values.Average();
//            double deviationSq = values.Select(x => Math.Pow(x - mean, 2)).Average();
//
//            var bucketeer = new Dictionary<double, double>();
//            for (double curr = values.Min(); curr <= values.Max(); curr += step)
//            {
//                // Counting the values that can be put in the bucket and dividing them on values.Count()
//                var count = values.Where(x => x >= fromVal && x < fromVal + step).Count();
//                bucketeer.Add(fromVal, count / values.Count());
//            }
//
//            // Then I build normal distribution overlay 
//            var overlayData = new LineSeries();
//            double x0 = values.Min();
//            double x1 = values.Max();
//            for (int i = 0; i < n; i++)
//            {
//                double x = x0 + (x1 - x0) * i / (n - 1);
//                double f = 1.0 / Math.Sqrt(2 * Math.PI * varianceSq) * Math.Exp(-(x - mean) * (x - mean) / 2 / varianceSq);
//                overlayData.Points.Add(new DataPoint(x, f));
//            }
//
//            // And draw everything
//
//            plotModel.Series.Add(overlayData);
//            foreach (var pair in bucketeer.OrderBy(x => x.Key))
//            {
//                columnSeries.Items.Add(new RectangleBarItem(pair.Key, 0, pair.Key + step, pair.Value));
//            }
//            plotModel.Series.Add(columnSeries);
        }
    }
}
