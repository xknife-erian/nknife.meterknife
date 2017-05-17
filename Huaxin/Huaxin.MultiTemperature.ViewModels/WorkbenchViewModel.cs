using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Huaxin.MultiTemperature.ViewModels.Entities;
using MeterKnife.Plots;
using NKnife.Utility;

namespace Huaxin.MultiTemperature.ViewModels
{
    public class WorkbenchViewModel : ViewModelBase
    {
        public SimpleLinePlot Plot { get; private set; } = new SimpleLinePlot("温度曲线");

        public ObservableCollection<MeterPoint> MeterPoints { get; set; } = new ObservableCollection<MeterPoint>();

        private string _CurrentPage = "HomePage";

        public string CurrentPage
        {
            get { return _CurrentPage; }
            set { Set(() => CurrentPage, ref _CurrentPage, value); }
        }

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
