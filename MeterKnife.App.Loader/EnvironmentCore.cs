using System;
using System.Windows.Forms;
using Common.Logging;
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

        private bool _IsDomainUnload;

        private EnvironmentCore(string[] args)
        {
            // 注册应用程序事件
            Application.ApplicationExit += OnApplicationExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainDomainUnload;

            _logger.Info($"==== {DateTime.Now.ToLongDateString()} ========================");
            _logger.Info($"==== {AppDomain.CurrentDomain.BaseDirectory} ====");

            _logger.Info("开始加载...");

            Splasher.Status = "开始加载引擎...";
            LoadCoreService();

            var workbench = new Workbench();
            workbench.Shown += (s, e) =>
            {
                Splasher.Status = "主控台载入完成...";
                Splasher.Close();
                workbench.Activate();
                _logger.Info("主控台载入完成.");
            };
            workbench.Closed += (s, e) =>
            {
                _logger.Info("软件准备关闭...");
                OnApplicationExit(s, e);
            };
            workbench.Show();
            workbench.Refresh();
        }

        private void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            _IsDomainUnload = true;
            OnApplicationExit(sender, e);
        }

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>
        private void LoadCoreService()
        {
            Splasher.Status = "加载核心服务及插件...";

            //加载并注册插件
            //            ClientSender.SendSplashMessage("加载插件...");
            //            _PluginManager = DI.Get<IPluginManager>();
            //            if (_PluginManager.StartService())
            //            {
            //                ClientSender.SendSplashMessage("注册插件...");
            //                _PluginManager.RegistPlugIns(DI.Get<IExtenderProvider>());
            //            }
            Splasher.Status = "加载核心服务及插件完成...";
            _logger.Info("加载核心服务及插件完成.");
        }

        /// <summary>
        ///     卸载排队核心服务及插件
        /// </summary>
        private void UnloadCoreService()
        {
            //处理程序退出前要处理的东西
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
                UnloadCoreService();
                Application.Exit();
            }
            catch (Exception exception)
            {
                _logger.Error("应用程序退出时异常", exception);
            }
        }

    }
}