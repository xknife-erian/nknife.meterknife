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
using Huaxin.MultiTemperature.App.Views.SubPages;
using NKnife.IoC;

namespace Huaxin.MultiTemperature.App.Views
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            NewProjectButton.Click += (s, e) => { DI.Get<WorkbenchViewModel>().CurrentPage = nameof(CompanyAndMeterInfoPage); };
        }
    }
}
