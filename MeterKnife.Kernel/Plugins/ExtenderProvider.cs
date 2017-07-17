using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Kernel.Plugins
{
    public class ExtenderProvider : IExtenderProvider
    {
        #region Implementation of IExtenderProvider

        /// <summary>返回IQEventExtender的基接口，描述取号程序的核心事件与事件函数
        /// </summary>
        /// <returns>IQCore的基接口，描述取号程序的核心事件与事件函数</returns>
        public IEventExtender Events { get; }

        /// <summary>返回IQDataExtender的基接口，描述取号程序的运行时数据提供器
        /// </summary>
        /// <returns>IQRunTime的基接口，描述取号程序的运行时数据提供器</returns>
        public IDataExtender Datas { get; }

        /// <summary>返回IQManager接口，该接口描述取号程序的管理器，实现一些类似重启应用程序的功能
        /// </summary>
        /// <returns></returns>
        public IFuctionExtender Fuctions { get; }

        #endregion
    }
}
