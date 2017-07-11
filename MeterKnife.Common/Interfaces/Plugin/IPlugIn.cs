namespace MeterKnife.Interfaces.Plugin
{
    /// <summary>
    /// 插件接口。
    /// 功能：排队系统的功能通过插件实现，插件与核心服务交换消息，
    /// 以实现排队流程的各个扩展功能模组。
    /// </summary>
    public interface IPlugIn
    {
        /// <summary>向扩展模组(Client)注册并释放扩展接口供给器，以暴露ICore的方法与事件。
        /// </summary>
        /// <param name="provider">排队核心服务释放给插件的接口供给器</param>
        /// <returns></returns>
        bool Register(ref IExtenderProvider provider);

        /// <summary>从扩展模组(Client)回收扩展接口供给器。
        /// </summary>
        /// <returns></returns>
        bool UnRegister();
    }
}