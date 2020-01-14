namespace NKnife.Configuring.Interfaces
{
    public interface IUserApplicationData
    {
        /// <summary>本选项面向的持久化文件
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; }

        /// <summary>用作当前非漫游用户使用的应用程序特定数据的公共储存库路径。
        /// </summary>
        /// <value>The user application data path.</value>
        string UserApplicationDataPath { get; }

        /// <summary>尝试按指定的名称获取选项值
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool TryGetValue(string localname, out object value);

        /// <summary>按指定的名称获取选项值，如果该值无法获取，将保存指定的默认值
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <param name="defalutValue">The defalut value.</param>
        /// <returns></returns>
        string GetValue(string localname, object defalutValue);

        /// <summary>按指定的名称设置值
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <param name="value">The value.</param>
        void SetValue(string localname, object value);

        /// <summary>加载选项文件
        /// </summary>
        void Load();

        /// <summary>持久化选项文件
        /// </summary>
        void Save();
    }
}