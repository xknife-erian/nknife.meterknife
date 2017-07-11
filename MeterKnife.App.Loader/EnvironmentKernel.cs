using System;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Interfaces.Plugin;
using MeterKnife.Views;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class EnvironmentKernel : ApplicationContext
    {
        private static readonly ILog _logger = LogManager.GetLogger<EnvironmentKernel>();

        private bool _IsDomainUnload;
        private IPluginManager _PluginManager;

        private EnvironmentKernel(string[] args)
        {
            // 注册应用程序事件
            Application.ApplicationExit += OnApplicationExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainDomainUnload;

            _logger.Info($"==== {DateTime.Now.ToLongDateString()} ========================");
            _logger.Info($"==== {AppDomain.CurrentDomain.BaseDirectory} ====");

            _logger.Info("开始加载...");

            DomainSender.SendSplashMessage("开始加载引擎...");
            LoadCoreService();

            var workbench = new Workbench();
            workbench.Shown += (s, e) =>
            {
                DomainSender.SendSplashMessage("主控台载入完成...");
                DomainSender.SendStartFormShown();
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
            DomainSender.SendSplashMessage("加载核心服务及插件...");

            //加载并注册插件
            DomainSender.SendSplashMessage("加载插件...");
            _PluginManager = DI.Get<IPluginManager>();
            if (_PluginManager.StartService())
            {
                DomainSender.SendSplashMessage("注册插件...");
                _PluginManager.RegistPlugIns(DI.Get<IExtenderProvider>());
            }

            DomainSender.SendSplashMessage("加载核心服务及插件完成...");
        }

        /// <summary>
        ///     卸载排队核心服务及插件
        /// </summary>
        private void UnloadQService()
        {
            //处理程序退出前要处理的东西
            _PluginManager.CloseService();
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
                UnloadQService();
                if (!_IsDomainUnload) //如果是主程序主动发起退出时，向父程序域发出退出指令
                    DomainSender.SendExitServiceCommand();
            }
            catch (Exception exception)
            {
                _logger.Error("应用程序退出时异常", exception);
            }
        }

        #region 单件实例

        private static readonly object _Lock = new object();
        private static EnvironmentKernel _Instance;

        public static EnvironmentKernel Instance(string[] args)
        {
            lock (_Lock)
            {
                if (_Instance == null)
                    _Instance = new EnvironmentKernel(args);
            }
            return _Instance;
        }

        public static EnvironmentKernel Instance()
        {
            return _Instance;
        }

        #endregion
    }
}