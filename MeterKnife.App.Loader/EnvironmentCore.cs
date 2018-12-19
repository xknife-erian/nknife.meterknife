using System;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Kernel;
using MeterKnife.Views;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class EnvironmentCore : ApplicationContext
    {
        private static readonly ILog Logger = LogManager.GetLogger<EnvironmentCore>();
        private readonly IKernels _kernels = DI.Get<IKernels>();

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
            Logger.Info($"=============================================================================");
            Logger.Info($">>>>>> {DateTime.Now.ToLongDateString()} <<<<<<");
            Logger.Info($">>>>>> {AppDomain.CurrentDomain.BaseDirectory} <<<<<<");

            Logger.Info("开始加载...");

            Splasher.Status = "开始加载引擎...";
            _kernels.LoadCoreService(DisplayMessage);

            var mainWorkbench = (Form)(DI.Get<IWorkbench>());
            mainWorkbench.Shown += (s, e) =>
            {
                Splasher.Status = "主控台即将载入完成...";
                Thread.Sleep(600);
                Splasher.Close();
                mainWorkbench.Activate();
                Logger.Info("主控台载入完成.");
            };
            mainWorkbench.Closed += (s, e) =>
            {
                Logger.Info("软件准备关闭...");
                Application.Exit();
            };
            mainWorkbench.Show();
            mainWorkbench.Refresh();
        }

        private void DisplayMessage(string message)
        {
            Splasher.Status = message;
            Logger.Info(message);
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
                _kernels.UnloadCoreService();
            }
            catch (Exception exception)
            {
                Logger.Error("卸载核心服务及插件", exception);
            }
        }
    }
}