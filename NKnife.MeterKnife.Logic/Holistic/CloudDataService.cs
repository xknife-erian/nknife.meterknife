using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;

namespace NKnife.MeterKnife.Logic.Holistic
{
    public class CloudDataService : ICloudDataService
    {
        #region Implementation of ICloudDataService

        /// <summary>
        /// 从云端获取仪器SCPI指令模板
        /// </summary>
        /// <param name="manufacturer">仪器的生产厂商</param>
        /// <param name="model">型号</param>
        /// <param name="submodel">子型号</param>
        /// <returns>模板数量</returns>
        public Task<int> GetInstrumentScpiTemplate(string manufacturer, string model, string submodel)
        {
            return Task<int>.Factory.StartNew(() => 0);
        }

        #endregion

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 90;
        public string Description { get; } = "";

        #endregion
    }
}
