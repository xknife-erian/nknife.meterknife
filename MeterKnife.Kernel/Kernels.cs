using System;
using System.Collections.Generic;
using Common.Logging;
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

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("定义核心服务...");
            _pluginService = DI.Get<IPluginService>();

            displayMessage("遍历程序集...");
            FindTypes();

            displayMessage("加载核心服务及插件...");
            _pluginService.StartService();

            displayMessage("注册所有插件完成...");

            displayMessage("加载核心服务及插件完成,关闭欢迎界面.");
        }

        private static void FindTypes()
        {
            var assems = UtilityAssembly.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var assembly in assems)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.ContainsInterface(typeof(IPlugIn)))
                    {
                        var plugin = (IPlugIn) UtilityType.CreateObject(type, type, false);
                        _pluginService.Plugins.Add(plugin);
                        _logger.Info($"{type.FullName}创建成功, 当前共{_pluginService.Plugins.Count}个plugin.");
                    }
                }
            }
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