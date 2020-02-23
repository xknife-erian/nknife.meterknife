using NKnife.MeterKnife.Base;
using NLog;

namespace NKnife.MeterKnife.ViewModels
{
    public class RealTimeDataPlotViewModel : PlotViewModel
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        public RealTimeDataPlotViewModel(IHabitManager habit, IAcquisitionService acquisitionService) 
            : base(habit)
        {
            acquisitionService.Acquired += OnAcquired;
        }
        
        private void OnAcquired(object sender, AcquisitionEventArgs e)
        {
            var index = _solution.IndexOf(e.DUT.Item2.Id);
            _Logger.Trace($"数据Index:{index},{e.Measurements.Data},{e.Time},{e.DUT},{e.Group}");
            if (index >= 0)
            {
                LinearPlot.AddValues((index, e.Time, e.Measurements.Data + _solution[index].Offset));
                OnPlotModelUpdated();
            }
        }
    }
}