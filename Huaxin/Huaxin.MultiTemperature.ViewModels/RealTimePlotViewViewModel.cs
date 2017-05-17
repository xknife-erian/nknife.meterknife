using GalaSoft.MvvmLight;
using MeterKnife.Plots;

namespace Huaxin.MultiTemperature.ViewModels
{
    public class RealTimePlotViewViewModel : ViewModelBase
    {
        public SimpleLinePlot SimpleLinePlot { get; set; } = new SimpleLinePlot("测量数据");
    }
}
