using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using MeterKnife.Plots;

namespace Huaxin.MultiTemperature.App.ViewModels
{
    public class RealTimePlotViewViewModel : ViewModelBase
    {
        public SimpleLinePlot SimpleLinePlot { get; set; } = new SimpleLinePlot("测量数据");
    }
}
