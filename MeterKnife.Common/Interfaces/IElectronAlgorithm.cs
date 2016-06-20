namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    ///     一个描述电子行业的数据采集的实时算法的接口
    /// </summary>
    public interface IElectronAlgorithm<in T>
    {
        /// <summary>
        ///     小数位数
        /// </summary>
        ushort DecimalDigit { get; set; }

        /// <summary>
        ///     输出数据
        /// </summary>
        double Output { get; }

        /// <summary>
        ///     清除数据
        /// </summary>
        void Clear();

        /// <summary>
        ///     输入数据
        /// </summary>
        /// <param name="src">指定的输入数据</param>
        void Input(T src);
    }
}