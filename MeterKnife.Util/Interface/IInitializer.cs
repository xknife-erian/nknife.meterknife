using System;

namespace MeterKnife.Util.Interface
{
    /// <summary>当一个类型需要初始化时
    /// </summary>
    public interface IInitializer
    {
        /// <summary>是否已经初始化
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        bool Initialize(params object[] args);

        /// <summary>初始化完成时发生的事件
        /// </summary>
        event EventHandler InitializedEvent;
    }
}
