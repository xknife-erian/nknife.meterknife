using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.MeterKnife.Workbench.Base
{
    public interface IAppManager
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
