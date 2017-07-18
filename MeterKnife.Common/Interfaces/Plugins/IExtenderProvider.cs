
namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// 核心扩展供给器，传递给所有插件（通过Register方法）
    /// </summary>
    public interface IExtenderProvider
    {
        /// <summary>返回IMeasureExtender的基接口，描述业务逻辑中"测量"功能的核心事件与事件函数
        /// </summary>
        IMeasureExtender Measures { get; }

        /// <summary>返回IDataExtender的基接口，描述对实时与历史数据提供的功能
        /// </summary>
        IDataExtender Datas { get; }

        /// <summary>返回IFuctionExtender接口，该接口公开了一些非业务逻辑的功能
        /// </summary>
        IFuctionExtender Fuctions { get; }
    }
}