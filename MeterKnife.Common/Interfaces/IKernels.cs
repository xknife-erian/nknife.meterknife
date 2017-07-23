using System;

namespace MeterKnife.Interfaces
{
    public interface IKernels
    {
        /// <summary>
        ///     加载核心服务及插件
        /// </summary>  
        void LoadCoreService(Action<string> displayMessage);
        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        void UnloadCoreService();
    }
}