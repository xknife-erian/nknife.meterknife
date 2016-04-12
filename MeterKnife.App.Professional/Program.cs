using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife;
using NKnife.IoC;

namespace MeterKnife.App.Professional
{
    internal class Program
    {
        private const string UPDATER_OPTION_FILE_NAME = "UpdaterOption.xml";

        [STAThread]
        private static void Main(string[] args)
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
            DI.AssmeblyNameFilters = new[] { "DirectX", "CommPort" };
            DI.Initialize();
            var logger = LogManager.GetLogger<Program>();

            //自动更新
            if (!ApplicationUpdate(args, logger))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.Properties.Settings.Default.CultureInfoName);
                Application.Run(new Form());
            }
#if !DEBUG
                mutex.ReleaseMutex();
#endif
        }

        private static bool ApplicationUpdate(string[] args, ILog logger)
        {
//            Splasher.Status = "开始准备自动更新参数";
//            var updateOptionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", UPDATER_OPTION_FILE_NAME);
//            bool requestUpdate = false; //是否开启自动更新
//            if (File.Exists(updateOptionFile))
//            {
//                try
//                {
//                    string content = File.ReadAllText(updateOptionFile);
//                    var doc = new XmlDocument();
//                    doc.LoadXml(content);
//                    var node = doc.SelectSingleNode("//UpdaterOption/UpdateStyle");
//                    if (node != null && node.InnerText == "First")
//                    {
//                        Splasher.Status = "开启自动更新";
//                        requestUpdate = true;
//                        Splasher.Status = "检测到需要自动更新";
//                    }
//                }
//                catch (Exception e)
//                {
//                    Debug.WriteLine(e);
//                }
//            }
//            return AutoUpdateStarter.Run(requestUpdate, args, logger.Info);
            return false;
        }

    }
}