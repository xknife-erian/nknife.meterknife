using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Huaxin.MultiTemperature.ViewModels;

namespace Huaxin.MultiTemperature.App.Views
{
    /// <summary>
    /// PlotPage.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimePlotViewPage
    {
        private readonly RealTimePlotViewViewModel _ViewModel;

        public RealTimePlotViewPage()
        {
            InitializeComponent();
            _ViewModel = (RealTimePlotViewViewModel) DataContext;
            MainPlotView.Model = _ViewModel.SimpleLinePlot.PlotModel;
        }
    }
}
