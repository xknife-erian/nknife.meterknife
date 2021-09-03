using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Logic;
using NKnife.MeterKnife.Util.Serial;
using NKnife.Win.Forms.Forms;
using NKnife.Win.Quick.Base;
using NKnife.MeterKnife.Workbench.Base;
using NLog;

namespace NKnife.MeterKnife.App
{
    public class Startup : ApplicationContext
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        private readonly IAppManager _appManager;
        private readonly IWorkbench _workbench;
        private readonly IHabitManager _habitManager;

        public Startup(IHabitManager habitManager, IWorkbench workbench, IAppManager appManager)
        {
            Global.Culture = habitManager.GetHabitValue(nameof(Global.Culture), Global.Culture);
            _habitManager = habitManager;
            _workbench = workbench;
            _appManager = appManager;
            Task.Factory.StartNew(SerialHelper.RefreshSerialPorts);
            LoadEnvironment();
            Application.ApplicationExit += OnApplicationExit;
        }

        private void LoadEnvironment()
        {
            _Logger.Info("=============================================================================");
            _Logger.Info($">>>>>> {DateTime.Now.ToLongDateString()} {DateTime.Now.ToShortTimeString()}<<<<<<");
            _Logger.Info($">>>>>> {AppDomain.CurrentDomain.BaseDirectory} <<<<<<");

            _Logger.Info("开始加载...");

            Splasher.Status = "开始加载引擎...";
            _appManager.LoadCoreService(DisplayMessage);

            if (_workbench is Form mainWorkbench)
            {
                mainWorkbench.Shown += (s, e) =>
                {
                    Splasher.Status = "主控台即将载入完成...";
                    Thread.Sleep(100 * 5);
                    Splasher.Close();
                    mainWorkbench.Activate();
                    _Logger.Info("主控台载入完成.");
                };
                mainWorkbench.Closed += (s, e) =>
                {
                    Application.Exit();
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
                _appManager.UnloadCoreService();
            }
            catch (Exception exception)
            {
                _Logger.Error(exception, "卸载核心服务及插件");
            }
        }
    }
}