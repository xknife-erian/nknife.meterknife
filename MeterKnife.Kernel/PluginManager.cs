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
