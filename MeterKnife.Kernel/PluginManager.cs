using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Kernel
{
    public class PluginManager : IPluginManager
    {
        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            throw new NotImplementedException();
        }

        public int Order { get; } = 100;
        public string Description { get; } = "插件管理器";

        public void RegistPlugIns(params IPlugIn[] plugIns)
        {
            var extenderProvider = DI.Get<IExtenderProvider>();
            foreach (IPlugIn plugIn in plugIns)
            {
                switch (plugIn.PluginStyle)
                {
                    case PluginStyle.Data:
                        plugIn.BindViewComponent(null);
                        break;
                    case PluginStyle.Tool:
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
