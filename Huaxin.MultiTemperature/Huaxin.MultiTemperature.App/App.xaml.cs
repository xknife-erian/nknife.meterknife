using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using NKnife.IoC;

namespace Huaxin.MultiTemperature.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DI.Initialize();
            DispatcherHelper.Initialize();
        }
    }
}
