using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Fairy.Properties;
using MeterKnife.Instruments;
using MeterKnife.Starter;
using MeterKnife.Workbench.Dialogs;
using NKnife;
using NKnife.IoC;

namespace MeterKnife.Fairy
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if !DEBUG
            // 得到正在运行的例程
            bool createdNew;
            var mutex = new System.Threading.Mutex(true, "MeterKnife", out createdNew);
            if (!createdNew)
            {
                System.Windows.Forms.MessageBox.Show("在同一时间内仅支持一个MeterKnife程序进程。", "启动注意:",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }
#endif
            Global.Culture = Common.Properties.Settings.Default.CultureInfoName;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.AssmeblyNameFilters = new[] {"DirectX", "CommPort"};
            DI.Initialize();
            LogManager.GetLogger<Program>();

            FileCleaner.Run();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);

            AddGpibMeterDialog.IsFairy = true;
            DigitMultiMeterView.IsFairy = true;

            MeterKnifeEnvironment.Workbench = new FairyForm();
            Application.Run(new MeterKnifeEnvironment());
#if !DEBUG
            mutex.ReleaseMutex();
#endif
        }
    }
}
