using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces.Plugins;
using NKnife.Interface;
using NKnife.IoC;

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

        public int Order { get; }
        public string Description { get; }

        #region Implementation of IEnvironmentItem

        public void RegistPlugIns(params IPlugIn[] plugIns)
        {
            var extenderProvider = DI.Get<IExtenderProvider>();
            foreach (IPlugIn plugIn in plugIns)
            {
                try
                {
                    plugIn.BindViewComponent(_DropFunction[plugIn.PluginStyle]);
                    plugIn.Register(ref extenderProvider);
                }
                catch (Exception e)
                {
                    _logger.Error($"注册插件异常：{e.Message}", e);
                }
            }
        }

        #endregion
    }
}
