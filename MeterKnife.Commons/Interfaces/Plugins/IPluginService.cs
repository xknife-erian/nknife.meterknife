using System.Collections.Generic;
using NKnife.Interface;

namespace MeterKnife.Interfaces.Plugins
{
    public interface IPluginService : IEnvironmentItem
    {
        List<IPlugIn> Plugins { get; }

        void RegistPlugIns(params IPlugIn[] plugIn);
    }
}
