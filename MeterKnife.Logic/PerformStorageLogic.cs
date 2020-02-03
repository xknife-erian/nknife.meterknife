﻿using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IStorageDUTWrite<MetricalData> _dataStorageDUTWrite;

        public PerformStorageLogic(IStorageDUTWrite<MetricalData> dataStorageDUTWrite)
        {
            _dataStorageDUTWrite = dataStorageDUTWrite;
        }

        /// <summary>
        ///     被测单元
        /// </summary>
        public Dictionary<string, (Engineering, DUT)> DUTMap { get; set; } = new Dictionary<string, (Engineering, DUT)>();

        #region Implementation of IPerformStorageLogic

        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <param name="data">数据</param>
        public async Task<bool> ProcessAsync((Engineering, DUT) dut, MetricalData data)
        {
            return await _dataStorageDUTWrite.InsertAsync(dut, data);
        }

        /// <summary>
        /// 根据发送源命令的关系获取被测物
        /// </summary>
        /// <param name="relation">源命令的关系</param>
        /// <returns>被测物</returns>
        public (Engineering, DUT) GetDUT(string relation)
        {
            return DUTMap[relation];
        }

        /// <summary>
        ///     根据协议的命令字获取被测物（通常是Care自带的数据采集，例如温度）
        /// </summary>
        /// <param name="mainCommand">主命令字</param>
        /// <param name="subCommand">子命令字</param>
        /// <returns>被测物</returns>
        public (Engineering, DUT) GetDUT(byte mainCommand, byte subCommand)
        {
            var key = $"{mainCommand:X2}{subCommand:X2}";
            if (!DUTMap.TryGetValue(key, out var dut)) 
                DUTMap.Add(key, (new Engineering(), new DUT()));
            return dut;
        }

        /// <summary>
        ///     根据发送协议获取被测物
        /// </summary>
        /// <param name="sourceCommand">源命令</param>
        /// <returns>被测物</returns>
        public (Engineering, DUT) GetDUT(byte[] sourceCommand)
        {
            var key = sourceCommand.ToDUTKey();
            if (!DUTMap.TryGetValue(key, out var dut)) 
                DUTMap.Add(key, (new Engineering(), new DUT()));
            return dut;
        }

        /// <summary>
        /// 设置命令字与被测物的关系
        /// </summary>
        /// <param name="relation">源命令的关系</param>
        /// <param name="dut">被测物</param>
        public void SetDUT(string relation, (Engineering, DUT) dut)
        {
            if (!DUTMap.ContainsKey(relation))
                DUTMap.Add(relation, dut);
        }

        #endregion
    }
}