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
using Huaxin.MultiTemperature.App.ViewModels;

namespace Huaxin.MultiTemperature.App.Views.SubPages
{
    /// <summary>
    /// ProjectAndMeterInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectAndMeterInfoPage : UserControl
    {
        private ProjectAndMeterInfoViewModel _ViewModel;
        public ProjectAndMeterInfoPage()
        {
            InitializeComponent();
            _ViewModel = (ProjectAndMeterInfoViewModel) DataContext;
            ButtonClickEventManager();
        }

        private void ButtonClickEventManager()
        {
            ClearButton.Click += (s, e) =>
            {
                _ViewModel.Clear();
            };
            AcceptButton.Click += (s, e) =>
            {
                _ViewModel.Accept();
            };
        }
    }
}
