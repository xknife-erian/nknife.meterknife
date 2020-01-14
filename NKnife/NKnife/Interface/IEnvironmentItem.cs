namespace NKnife.Interface
{
    /// <summary>应用程序启动时需加载的各种服务(环境)
    /// </summary>
    public interface IEnvironmentItem
    {
        /// <summary>启动的顺序，值越大将越先启动
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>启动服务
        /// </summary>
        bool StartService();


        /// <summary>关闭服务，关闭服务将会按照启动的顺序反向关闭
        /// </summary>
        bool CloseService();

        /// <summary>服务的描述
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }
    }
}
