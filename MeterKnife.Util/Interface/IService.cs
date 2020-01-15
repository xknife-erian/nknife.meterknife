namespace NKnife.Interface
{
    /// <summary>
    /// 一个描述应用程序服务的接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// 设置服务参数
        /// </summary>
        /// <param name="objects">初始化服务携带的参数.</param>
        void SetServiceParams(params object[] objects);

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <returns></returns>
        bool Initializes();

        /// <summary>
        /// 重新启动服务。先初始化，再执行Start.
        /// </summary>
        bool ReStart();

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// 终止服务
        /// </summary>
        /// <returns></returns>
        bool Stop();
    }
}
