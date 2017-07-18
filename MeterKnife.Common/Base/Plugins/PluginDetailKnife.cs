using MeterKnife.Interfaces.Plugins;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Base.Plugins
{
    public class PluginDetailKnife : PluginDetail
    {
        public PluginDetailKnife()
        {
            Author = "Erian Lu";
            Contact = "lukan@xknife.net";
            Version = DI.Get<IAbout>().AssemblyVersion;
        }
    }
}
