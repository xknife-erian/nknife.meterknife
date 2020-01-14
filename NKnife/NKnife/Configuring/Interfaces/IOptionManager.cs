using System;
using NKnife.Configuring.OptionCase;

namespace NKnife.Configuring.Interfaces
{
    /// <summary>
    /// 应用程序选项信息的管理器
    /// </summary>
    public interface IOptionManager
    {
        /// <summary>当前应用程序ID
        /// </summary>
        /// <value>The name of the curr option group.</value>
        string CurrentClientId { get; }

        /// <summary>根据组名+键名获取选项信息
        /// </summary>
        /// <param name="category">选项信息所在的信息表</param>
        /// <param name="key">选项键</param>
        /// <returns></returns>
        string GetOptionValue(string category, string key);

        /// <summary>根据组名+键名获取选项信息
        /// </summary>
        /// <param name="category">选项信息所在的分类</param>
        /// <param name="key">选项键</param>
        /// <returns></returns>
        T GetOptionValue<T>(string category, string key) where T : struct;

        /// <summary>根据组名+键名获取选项信息，并通过解析器将选项信息转换成指定的类型
        /// </summary>
        /// <param name="category">选项信息所在的分类</param>
        /// <param name="key">选项键</param>
        /// <param name="parser">解析器</param>
        /// <returns></returns>
        T GetOptionValue<T>(string category, string key, Func<object, T> parser);

        /// <summary>根据组名+键名设置选项信息
        /// </summary>
        /// <param name="category">选项信息所在的分类</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool SetOptionValue(string category, string key, object value);

        // ===========================

        /// <summary>重新加载所有的选项值
        /// </summary>
        /// <returns></returns>
        bool ReLoad();

        /// <summary>清理选项相关的环境、目录等
        /// </summary>
        /// <returns></returns>
        bool Clean();

        /// <summary>备份选项存储器
        /// </summary>
        /// <returns></returns>
        object Backup();

        /// <summary>持久化选项信息
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>当初始化完成后发生的事件
        /// </summary>
        event EventHandler LoadedEvent;

        // ==== OptionSolution ===========================

        /// <summary>当前应用程序的选项信息组的名称
        /// </summary>
        /// <value>The name of the curr option group.</value>
        OptionCaseItem CurrentCase { get; set; }

        /// <summary>更新解决方案的保存(当已存在时就更新，否则添加)
        /// </summary>
        void AddOrUpdateCaseStore(OptionCaseItem optionCase);

        /// <summary>删除一个解决方案的保存
        /// </summary>
        void RemoveCaseStore(OptionCaseItem optionCase);

        /// <summary>以源方案为模板复制一套新的解决方案
        /// </summary>
        /// <param name="srcCase">源方案.</param>
        OptionCaseItem CopyCaseFrom(OptionCaseItem srcCase);

        /// <summary>新增一套方案
        /// </summary>
        /// <param name="optionCase">The solution.</param>
        /// <param name="isStore">True时同时持久化，否则反之</param>
        void AddCase(OptionCaseItem optionCase, bool isStore);

        /// <summary>删除一套方案
        /// </summary>
        /// <param name="optionCase">The solution.</param>
        /// <param name="isStore">True时同时从持久化中删除，否则反之</param>
        void RemoveCase(OptionCaseItem optionCase, bool isStore);
    }
}