using System.Collections.Generic;
using NKnife.Configuring.OptionCase;

namespace NKnife.Configuring.Interfaces
{
    /// <summary>
    /// 往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例，
    /// 本接口描述多份选项实例的管理器的接口。
    /// </summary>
    public interface IOptionCaseManager : IList<OptionCaseItem>
    {
        /// <summary>
        /// 根据指定的含有管理器序列化信息的XML节点初始化选项实例的管理器。
        /// </summary>
        void Initialize();

        /// <summary>按照当前的场景或时段匹配的OptionCaseItem
        /// </summary>
        OptionCaseItem Current();
    }
}
