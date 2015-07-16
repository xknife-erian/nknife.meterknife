using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using ScpiKnife;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述仪器的接口
    /// </summary>
    public interface IMeter
    {
        /// <summary>
        /// 品牌
        /// </summary>
        string Brand { get; set; }
        string Name { get; set; }
        string AbbrName { get; }
        int GpibAddress { get; set; }

    }
}
