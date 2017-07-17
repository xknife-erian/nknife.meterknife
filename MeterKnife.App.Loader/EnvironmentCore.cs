using System;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Kernel;
using MeterKnife.Views;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class EnvironmentCore : ApplicationContext
    {
        private static readonly ILog _logger = LogManager.GetLogger<EnvironmentCore>();

        #region Singleton Instance

        private static EnvironmentCore _instance;

        public static EnvironmentCore Instance(string[] args)
        {
            return _instance ?? (_instance = new EnvironmentCore(args));
        }

        #endregion

        private EnvironmentCore(string[] args)
        {
            Application.ApplicationExit += OnApplicationExit;
            LoadEnvironment();
        }

        private void LoadEnvironment()
        {
            _logger.Info($"==== {DateTime.Now.ToLongDateString()} ========================");
            _logger.Info($"==== {AppDomain.CurrentDomain.BaseDirectory} ====");

            _logger.Info("开始加载...");

            Splasher.Status = "开始加载引擎...";
            Kernels.LoadCoreService(DisplayMessage);

            var mainWorkbench = new Workbench();
            mainWorkbench.Shown += (s, e) =>
            {
                Splasher.Status = "主控台即将载入完成...";
                Thread.Sleep(1500);
                Splasher.Close();
                mainWorkbench.Activate();
                _logger.Info("主控台载入完成.");
            };
            mainWorkbench.Closed += (s, e) =>
            {
                _logger.Info("软件准备关闭...");
                OnApplicationExit(s, e);
            };
            mainWorkbench.Show();
            mainWorkbench.Refresh();
        }

        private void DisplayMessage(string message)
        {
            Splasher.Status = message;
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
                Kernels.UnloadCoreService();
            }
            catch (Exception exception)
            {
                _logger.Error("卸载核心服务及插件", exception);
            }
            try
            {
                Application.Exit();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}