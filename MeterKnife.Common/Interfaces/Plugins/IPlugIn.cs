
namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// 插件接口。系统的功能通过插件实现，插件与核心服务交换消息，以实现各个扩展功能模组。
    /// </summary>
    public interface IPlugIn
    {
        /// <summary>
        /// 描述本插件类型
        /// </summary>
        PluginStyle PluginStyle { get; }

        /// <summary>
        /// 将本插件的功能绑定于相应的菜单与工具条上，绑定需要呈现的控件到相应的界面组件上。
        /// </summary>
        /// <param name="component"></param>
        void BindViewComponent(IPluginViewComponent component);

        /// <summary>
        /// 向扩展模组注册并释放核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        bool Register(ref IExtenderProvider provider);

        /// <summary>
        /// 从扩展模组回收核心扩展供给器。
        /// </summary>
        bool UnRegister();
    }
}