namespace NKnife.MeterKnife
{
    public interface IRouteEnable
    {
        /// <summary>
        ///     启用路由（即启用多路）
        /// </summary>
        bool EnableMultiplexing { get; }
    }
}