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
            return true;
        }

        public bool CloseService()
        {
            throw new NotImplementedException();
        }

        public int Order { get; } = 100;
        public string Description { get; } = "插件管理器";

        public void RegistPlugIns(params IPlugIn[] plugIn)
        {
        }

        #endregion
    }
}
