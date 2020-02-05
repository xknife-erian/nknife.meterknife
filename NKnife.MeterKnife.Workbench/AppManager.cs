using System;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class AppManager : IAppManager
    {
        private readonly IAppTrayService _trayService;

        public AppManager(IAppTrayService trayService)
        {
            _trayService = trayService;
        }

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"加载{_trayService.Description}...");
            _trayService.StartService();

            displayMessage("加载核心服务及插件完成,关闭欢迎界面.");
        }

        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        public void UnloadCoreService()
        {
            _trayService.CloseService();
        }
    }
}