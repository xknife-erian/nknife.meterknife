using System;
using System.Threading;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class AppManager : IAppManager
    {
        private readonly IAppTrayService _trayService;
        private readonly IDialogService _dialogService;

        public AppManager(IAppTrayService trayService, IDialogService dialogService)
        {
            _trayService = trayService;
            _dialogService = dialogService;
        }

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"加载{_trayService.Description}...");
            _trayService.StartService();
            Thread.Sleep(1 * 1000);
            displayMessage($"加载{_dialogService.Description}...");
            _dialogService.StartService();
            Thread.Sleep(1 * 1000);

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