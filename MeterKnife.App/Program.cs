using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife;
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
            Global.Culture = Common.Properties.Settings.Default.CultureInfoName;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.AssmeblyNameFilters = new[] {"DirectX", "CommPort"};
            DI.Initialize();
            LogManager.GetLogger<Program>();

            Surroundings.Workbench = new Workbench();
            Application.Run(new Surroundings());
        }
    }
}
