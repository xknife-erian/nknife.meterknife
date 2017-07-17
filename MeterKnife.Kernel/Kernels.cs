using System;
using Common.Logging;

namespace MeterKnife.Kernel
{
    public class Kernels
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("加载核心服务及插件...");
            // 加载并注册插件
            // ClientSender.SendSplashMessage("加载插件...");
            // _PluginManager = DI.Get<IPluginManager>();
            // if (_PluginManager.StartService())
            // {
            //     ClientSender.SendSplashMessage("注册插件...");
            //     _PluginManager.RegistPlugIns(DI.Get<IExtenderProvider>());
            // }
            displayMessage("加载核心服务及插件完成...");
            _logger.Info("加载核心服务及插件完成,关闭欢迎界面.");
        }
        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        public static void UnloadCoreService()
        {
            //处理程序退出前要处理的东西
        }
    }
}