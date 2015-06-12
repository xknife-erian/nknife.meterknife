using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Fairy.Properties;
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
        static void Main()
        {
            Global.Culture = Settings.Default.CultureInfoName;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.AssmeblyNameFilters = new[] { "DirectX", "CommPort" };
            DI.Initialize();
            var logger = LogManager.GetLogger<Program>();

            Application.Run(new FairyForm());
        }
    }
}
