using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Channels.Channels.Serials;
using NKnife.IoC;

namespace Huaxin.MultiTemperature.App.Demo
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();
            LogManager.GetLogger<Program>();
            SerialUtils.RefreshSerialPorts();

            Application.Run(new Workbench());
        }
    }
}
