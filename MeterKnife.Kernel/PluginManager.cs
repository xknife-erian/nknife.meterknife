using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Plugin;

namespace MeterKnife.Kernel
{
    public class PluginManager : IPluginManager
    {
        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            throw new NotImplementedException();
        }

        public bool CloseService()
        {
            throw new NotImplementedException();
        }

        public int Order { get; } = 100;
        public string Description { get; } = "插件管理器";

        public void RegistPlugIns(IExtenderProvider provider)
        {
        }

        #endregion
    }
}
