using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NKnife.IoC;

namespace MeterKnife.App
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.AssmeblyNameFilters = new[] {"DirectX", "CommPort", "NPOI"};
            DI.Initialize();
            DI.BindAppStartup<Surroundings>();

            Surroundings.Workbench = new Workbench();
            Application.Run(DI.Get<Surroundings>());
        }
    }
}
