using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Financial;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util;

namespace NKnife.MeterKnife.Logic
{
    /// <summary>
    ///     描述一次测量执行事件的数据逻辑
    ///     2020年1月18日新增
    /// </summary>
    public class PerformStorageLogic : IPerformStorageLogic
    {
        /// <summary>
        ///     采集编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     关于本次采集的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     被测单元
        /// </summary>
        public Dictionary<string, DUT> DUTMap { get; set; } = new Dictionary<string, DUT>();

        #region Implementation of IPerformStorageLogic

        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <param name="data">数据</param>
        public async Task<bool> ProcessAsync(DUT dut, MetricalData data)
        {
            return await Task.Factory.StartNew(() => true);
        }

        /// <summary>
        /// 根据协议的命令字获取被测物（通常是Care自带的数据采集，例如温度）
        /// </summary>
        /// <param name="mainCommand">主命令字</param>
        /// <param name="subCommand">子命令字</param>
        /// <returns>被测物</returns>
        public DUT GetDUT(byte mainCommand, byte subCommand)
        {
            string key = $"{mainCommand:X2}{subCommand:X2}";
            if (!DUTMap.TryGetValue(key, out var dut))
            {
                DUTMap.Add(key, new DUT());
            }
            return dut;
        }

        /// <summary>
        /// 根据发送协议获取被测物
        /// </summary>
        /// <param name="sourceCommand">源命令</param>
        /// <returns>被测物</returns>
        public DUT GetDUT(byte[] sourceCommand)
        {
            string key = sourceCommand.ToDUTKey();
            if (!DUTMap.TryGetValue(key, out var dut))
            {
                DUTMap.Add(key, new DUT());
            }
            return dut;
        }

        #endregion
    }
}
