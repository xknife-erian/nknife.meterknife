using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel
{
    public class Kernels
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        private static IPluginService _pluginService;
        private static IGatewayService _gatewayService;

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("加载插件服务...");
            _pluginService = DI.Get<IPluginService>();
            _pluginService.StartService();
            displayMessage("注册所有插件完成...");

            displayMessage("加载Getway服务...");
            _gatewayService = DI.Get<IGatewayService>();
            _gatewayService.StartService();

            displayMessage("加载核心服务及插件完成,关闭欢迎界面.");
        }

        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        public static void UnloadCoreService()
        {
            _gatewayService.CloseService();
            _pluginService.CloseService();
        }
    }
}