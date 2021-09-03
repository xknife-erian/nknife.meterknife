using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NKnife.Interface;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface ICloudDataService : IEnvironmentItem
    {
        /// <summary>
        /// 从云端获取仪器SCPI指令模板
        /// </summary>
        /// <param name="manufacturer">仪器的生产厂商</param>
        /// <param name="model">型号</param>
        /// <param name="submodel">子型号</param>
        /// <returns>模板数量</returns>
        Task<int> GetInstrumentScpiTemplate(string manufacturer, string model, string submodel);
    }
}
