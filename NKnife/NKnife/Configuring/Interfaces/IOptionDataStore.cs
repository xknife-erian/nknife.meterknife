using System.Collections.Concurrent;
using NKnife.Configuring.Option;
using NKnife.Configuring.OptionCase;

namespace NKnife.Configuring.Interfaces
{
    /// <summary>
    /// 描述应用程序选项信息存储空间操作方法的封装
    /// </summary>
    public interface IOptionDataStore
    {
        /// <summary>是否已经初始化完成
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialize success; otherwise, <c>false</c>.
        /// </value>
        bool IsInitializeSuccess { get; }

        /// <summary>获得一份选项实例的管理器。
        /// 往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例。
        /// </summary>
        /// <returns></returns>
        IOptionCaseManager CaseManager { get; }

        /// <summary>应用程序选项信息存储空间
        /// </summary>
        /// <value>The store.</value>
        object StoreObject { get; }

        /// <summary>
        /// 多个子选项信息的集合
        /// </summary>
        /// <returns></returns>
        ConcurrentDictionary<string, IOption> DataTables { get; }

        /// <summary>初始化选项的存储目标
        /// </summary>
        /// <param name="target">The target.</param>
        void InitializeStoreTarget(object target);

        /// <summary>更新解决方案的保存(当已存在时就更新，否则添加)
        /// </summary>
        void AddOrUpdateCaseStore(OptionCaseItem optionCase);

        /// <summary>删除一个解决方案的保存
        /// </summary>
        void RemoveCaseStore(OptionCaseItem optionCase);

        /// <summary>
        /// 保存一个节点的选项信息
        /// </summary>
        /// <returns></returns>
        bool Update(IOption table);

        /// <summary>
        /// 保存整个存储器
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        /// 选项信息即将载入前发生的事件
        /// </summary>
        event OptionLoadingEventHandler OptionLoadingEvent;

        /// <summary>
        /// 选项信息载入后发生的事件
        /// </summary>
        event OptionLoadedEventHandler OptionLoadedEvent;

        bool ReLoad();
        bool Clean();
        object Backup();
    }
}