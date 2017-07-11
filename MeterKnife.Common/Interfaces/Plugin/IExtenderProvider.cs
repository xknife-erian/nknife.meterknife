namespace MeterKnife.Interfaces.Plugin
{
    /// <summary>
    /// 排队核心服务（领域模型）供给器，传递给所有插件（通过Register方法）
    /// </summary>
    public interface IExtenderProvider
    {
        /// <summary>返回IQEventExtender的基接口，描述取号程序的核心事件与事件函数
        /// </summary>
        /// <returns>IQCore的基接口，描述取号程序的核心事件与事件函数</returns>
        IEventExtender Events { get; }

        /// <summary>返回IQDataExtender的基接口，描述取号程序的运行时数据提供器
        /// </summary>
        /// <returns>IQRunTime的基接口，描述取号程序的运行时数据提供器</returns>
        IDataExtender Datas { get; }

        /// <summary>返回IQManager接口，该接口描述取号程序的管理器，实现一些类似重启应用程序的功能
        /// </summary>
        /// <returns></returns>
        IFuctionExtender Fuctions { get; }
    }
}