namespace NKnife.Circe.Base.Modules
{
    /// <summary>
    ///     应用程序的环境相关。
    /// </summary>
    public interface ISurroundings
    {
        /// <summary>
        ///     应用的开发者路径
        /// </summary>
        string AppDeveloperPath { get; }

        /// <summary>
        ///     应用程序的数据基础路径
        /// </summary>
        string AppPath { get; }

        /// <summary>
        ///     应用程序扩展数据路径
        /// </summary>
        string AppExtensionPath { get; }

        /// <summary>
        ///     配置文件的保存路径
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        ///     工作日志的保存路径
        /// </summary>
        string LoggerPath { get; }

        /// <summary>
        ///     实验数据保存的路径
        /// </summary>
        string GlobalDataPath { get; }

        string DefaultUserPassword { get; }
    }
}