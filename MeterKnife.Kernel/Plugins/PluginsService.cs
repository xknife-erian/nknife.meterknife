using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces.Plugins;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel.Plugins
{
    public class PluginsService : IPluginService
    {
        private static readonly ILog _logger = LogManager.GetLogger<PluginsService>();

        private readonly IDropFunctionManager _DropFunction = DI.Get<IDropFunctionManager>();
        public List<IPlugIn> Plugins { get; } = new List<IPlugIn>();

        public bool StartService()
        {
            try
            {
                // 搜索所有的插件
                var plugins = SearchTypes();
                Plugins.AddRange(plugins);
                // 注册所有插件
                RegistPlugIns(Plugins.ToArray());
                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"注册插件异常：{e.Message}", e);
                return false;
            }
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 5;
        public string Description { get; } = "插件管理服务";

        #region Implementation of IEnvironmentItem

        public void RegistPlugIns(params IPlugIn[] plugIns)
        {
            var extenderProvider = DI.Get<IExtenderProvider>();
            foreach (IPlugIn plugIn in plugIns)
            {
                try
                {
                    var pvc = _DropFunction[plugIn.PluginStyle];
                    plugIn.BindViewComponent(pvc);
                    plugIn.Register(ref extenderProvider);
                }
                catch (Exception e)
                {
                    _logger.Error($"注册插件异常：{e.Message}", e);
                }
            }
        }

        #endregion

        private static ICollection<IPlugIn> SearchTypes()
        {
            List<IPlugIn> plugIns = new List<IPlugIn>(); 
            var assems = UtilityAssembly.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var assembly in assems)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.ContainsInterface(typeof(IPlugIn)))
                    {
                        var plugin = (IPlugIn)UtilityType.CreateObject(type, type, false);
                        plugIns.Add(plugin);
                        _logger.Info($"{type.FullName}创建成功, 当前共{plugIns.Count}个plugin.");
                    }
                }
            }
            return plugIns;
        }
    }
}
