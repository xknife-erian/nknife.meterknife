using System;
using System.Collections.Generic;
using MeterKnife.Interfaces.Plugins;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Kernel.Plugins
{
    public class PluginsService : IPluginService
    {
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
                switch (plugIn.PluginStyle)
                {
                    case PluginStyle.DataMenu:
                        plugIn.BindViewComponent(null);
                        break;
                    case PluginStyle.ToolMenu:
                        break;
                    default:
                        break;
                }
                plugIn.Register(ref extenderProvider);
            }
        }

        #endregion
    }
}
