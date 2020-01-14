using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.App.Professional;
using NKnife;
using NKnife.IoC;

namespace MeterKnife.App.Lite
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Global.Culture = Common.Properties.Settings.Default.CultureInfoName;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.AssmeblyNameFilters = new[] {"DirectX", "CommPort"};
            DI.Initialize();
            LogManager.GetLogger<Program>();

            FileCleaner.Run();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);

            MeterKnifeEnvironment.Workbench = new MeterLiteMainForm();
            Application.Run(new MeterKnifeEnvironment());
        }
    }
}
