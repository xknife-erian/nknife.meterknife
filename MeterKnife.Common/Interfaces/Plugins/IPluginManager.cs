using NKnife.Interface;

namespace MeterKnife.Interfaces.Plugins
{
    public interface IPluginManager
    {
        void RegistPlugIns(params IPlugIn[] plugIn);
    }
}
