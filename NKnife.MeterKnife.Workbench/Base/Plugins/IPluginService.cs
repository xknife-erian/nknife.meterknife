using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.MeterKnife.Workbench.Base.Plugins
{
    public interface IPluginService : IEnvironmentItem
    {
        List<IPlugIn> Plugins { get; }

        void RegistPlugIns(params IPlugIn[] plugIn);
    }
}
