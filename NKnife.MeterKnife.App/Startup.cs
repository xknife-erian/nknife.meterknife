using System;
using System.Threading;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.Win.Forms.Forms;
using NLog;

namespace NKnife.MeterKnife.App
{
    public class Startup : ApplicationContext
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        private readonly IAppManager _manager;
        private readonly IWorkbench _workbench;

        public Startup(IWorkbench workbench, IAppManager manager)
        {
            Application.ApplicationExit += OnApplicationExit;
            _workbench = workbench;
            _manager = manager;
            LoadEnvironment();
        }

        private void LoadEnvironment()
        {
            _Logger.Info("=============================================================================");
            _Logger.Info($">>>>>> {DateTime.Now.ToLongDateString()} {DateTime.Now.ToShortTimeString()}<<<<<<");
            _Logger.Info($">>>>>> {AppDomain.CurrentDomain.BaseDirectory} <<<<<<");

            _Logger.Info("开始加载...");

            Splasher.Status = "开始加载引擎...";
            _manager.LoadCoreService(DisplayMessage);

            if (_workbench is Form mainWorkbench)
            {
                mainWorkbench.Shown += (s, e) =>
                {
                    Splasher.Status = "主控台即将载入完成...";
                    Thread.Sleep(1000 * 2);
                    Splasher.Close();
                    mainWorkbench.Activate();
                    _Logger.Info("主控台载入完成.");
                };
                mainWorkbench.Show();
                mainWorkbench.Refresh();
            }
        }

        private void DisplayMessage(string message)
        {
            Splasher.Status = message;
            _Logger.Info(message);
        }

        /// <summary>
        ///     应用程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                if (_workbench is Form mainWorkbench)
                    if (!mainWorkbench.IsDisposed)
                        mainWorkbench.Close();
                _manager.UnloadCoreService();
            }
            catch (Exception exception)
            {
                _Logger.Error(exception, "卸载核心服务及插件");
            }
        }
    }
}