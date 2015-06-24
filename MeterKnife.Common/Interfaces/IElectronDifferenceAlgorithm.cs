namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述电子行业的数据采集的“差”的实时算法的接口
    /// </summary>
    public interface IElectronDifferenceAlgorithm
    {
        /// <summary>
        /// 标称值
        /// </summary>
        double NominalValue { get; set; }

        IElectronAlgorithm Original { get; set; }

        double Output();
    }
}