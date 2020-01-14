using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NKnife.Interface;
using NKnife.IoC;
using NLog;

namespace NKnife.NLog3.Controls.WPF
{
    /// <summary>
    /// NLogWpfListView.xaml 的交互逻辑
    /// </summary>
    public partial class NLogWpfGrid : UserControl
    {
        private static readonly LogMessageObservableCollection _logMessages = LogMessageObservableCollection.Instance;

        public NLogWpfGrid()
        {
            InitializeComponent();
            _LoggerGrid.ItemsSource = _logMessages;
        }

        private void ClearMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _logMessages.Clear();
        }

        private void LevelMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var filter = DI.Get<LogMessageFilter>();
            var menuItem = (MenuItem) sender;
            var isChecked = menuItem.IsChecked;
            menuItem.IsChecked = !isChecked;
            var header = menuItem.Header.ToString();
            switch (header)
            {
                case "Trace":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Trace);
                    else
                        filter.Remove(LogLevel.Trace);
                    break;
                case "Debug":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Debug);
                    else
                        filter.Remove(LogLevel.Debug);
                    break;
                case "Info":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Info);
                    else
                        filter.Remove(LogLevel.Info);
                    break;
                case "Warn":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Warn);
                    else
                        filter.Remove(LogLevel.Warn);
                    break;
                case "Error":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Error);
                    else
                        filter.Remove(LogLevel.Error);
                    break;
                case "Fatal":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Fatal);
                    else
                        filter.Remove(LogLevel.Fatal);
                    break;
            }
        }
    }
}
