using NKnife.Interface;

namespace MeterKnife.Interfaces.Plugins
{
    public interface IPluginManager : IEnvironmentItem
    {
        void RegistPlugIns(params IPlugIn[] plugIn);
    }
}
