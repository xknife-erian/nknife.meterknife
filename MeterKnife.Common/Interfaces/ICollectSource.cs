using System;
using System.Data;
using MeterKnife.Common.Enums;
using MeterKnife.Common.EventParameters;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述采集数据源的接口
    /// </summary>
    public interface ICollectSource
    {
        IMeter Meter { get; set; }

        DataSet DataSet { get; }

        MeterRange MeterRange { get; set; }

        bool HasData { get; }

        void Clear();

        /// <summary>
        /// 导出到Excle文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        bool Save(string fileFullName);

        /// <summary>
        /// 当收到仪器采集数据时
        /// </summary>
        event EventHandler<CollectDataEventArgs> ReceviedCollectData;
    }
}
