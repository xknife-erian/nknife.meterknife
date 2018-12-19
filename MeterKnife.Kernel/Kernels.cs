using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel
{
    public class Kernels : IKernels
    {
        private static readonly ILog Logger = LogManager.GetLogger<Kernels>();

        private readonly IAppTrayService _appTrayService;
        private readonly IPluginService _pluginService;
        private readonly IGatewayService _gatewayService;
        private readonly IDatasService _datasService;
        private readonly IMeasureService _measureService;

        public Kernels()
        {
            _appTrayService = DI.Get<IAppTrayService>();
            _pluginService = DI.Get<IPluginService>();
            _gatewayService = DI.Get<IGatewayService>();
            _datasService = DI.Get<IDatasService>();
            _measureService = DI.Get<IMeasureService>();
        }

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"加载{_appTrayService.Description}...");
            _appTrayService.StartService();

            displayMessage($"加载{_pluginService.Description}...");
            _pluginService.StartService();
            displayMessage("注册所有插件完成...");

            displayMessage($"加载{_datasService.Description}...");
            _datasService.StartService();

            displayMessage($"加载{_gatewayService.Description}...");
            _gatewayService.StartService();

            displayMessage($"加载{_measureService.Description}...");
            _measureService.StartService();

            displayMessage("加载核心服务及插件完成,关闭欢迎界面.");
        }

        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        public void UnloadCoreService()
        {
            _gatewayService.CloseService();
            _datasService.CloseService();
            _pluginService.CloseService();
            _measureService.StartService();
            _appTrayService.CloseService();
        }
    }
}