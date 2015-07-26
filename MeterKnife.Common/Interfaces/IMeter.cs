using System.Collections.Generic;
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
        /// <summary>
        /// 仪器名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 仪器常用简称
        /// </summary>
        string AbbrName { get; }
        /// <summary>
        /// 仪器当前GPIB地址
        /// </summary>
        int GpibAddress { get; set; }
        /// <summary>
        /// 仪器工作主题集合
        /// </summary>
        ScpiSubjectCollection ScpiSubjects { get; set; }
    }
}
