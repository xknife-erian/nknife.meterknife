using GalaSoft.MvvmLight;

namespace Huaxin.MultiTemperature.App.ViewEntities
{
    public class MeterPoint : ViewModelBase
    {
        private double _ComputeValue;
        private double _MeterValue;
        private ushort _Point;

        public ushort Point
        {
            get { return _Point; }
            set { Set(() => Point, ref _Point, value); }
        }

        public double ComputeValue
        {
            get { return _ComputeValue; }
            set { Set(() => ComputeValue, ref _ComputeValue, value); }
        }

        public double MeterValue
        {
            get { return _MeterValue; }
            set { Set(() => MeterValue, ref _MeterValue, value); }
        }
    }
}