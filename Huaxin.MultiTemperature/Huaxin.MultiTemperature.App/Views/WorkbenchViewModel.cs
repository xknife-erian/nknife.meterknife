using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Huaxin.MultiTemperature.App.ViewEntities;
using NKnife.Utility;
using OxyPlot;
using OxyPlot.Series;

namespace Huaxin.MultiTemperature.App.Views
{
    public class WorkbenchViewModel : ViewModelBase
    {
        public WorkbenchViewModel()
        {
            this.Plot = new PlotModel {Title = "Example 1"};
            this.Plot.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel Plot { get; private set; }

        public ObservableCollection<MeterPoint> MeterPoints { get; set; } = new ObservableCollection<MeterPoint>();

        public void BuildMeterPoints()
        {
            var rand = new UtilityRandom();
            for (int i = 0; i < 40; i++)
            {
                var point = new MeterPoint();
                point.Point = (ushort)(i + 1);
                point.MeterValue = rand.Next(10000, 12300) / 95;
                point.ComputeValue = rand.Next(30000, 32300) / 63;
                MeterPoints.Add(point);
            }
        }
    }
}
