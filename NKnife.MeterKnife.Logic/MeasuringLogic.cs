using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Logic
{
    /// <summary>
    ///     描述一次测量执行事件过程中的逻辑
    ///     2020年1月18日新增
    /// </summary>
    public class MeasuringLogic : IMeasuringLogic
    {
        private readonly IStorageDUTWrite<MeasureData> _dataStorageDUTWrite;

        public MeasuringLogic(IStorageDUTWrite<MeasureData> dataStorageDUTWrite)
        {
            _dataStorageDUTWrite = dataStorageDUTWrite;
        }

        /// <summary>
        ///     被测单元字典。Key是命令体，Value是工程与被测物。
        /// </summary>
        public Dictionary<string, (Engineering, DUT)> DUTMap { get; set; } = new Dictionary<string, (Engineering, DUT)>();

        #region Implementation of IMeasuringLogic

        /// <summary>
        ///     处理当前的被测物的测量数据
        /// </summary>
        /// <param name="dut">指定的被测物</param>
        /// <param name="data">数据</param>
        public async Task<bool> ProcessAsync((Engineering, DUT) dut, MeasureData data)
        {
            return await _dataStorageDUTWrite.InsertAsync(dut, data);
        }

        /// <summary>
        ///     根据发送源命令的关系获取被测物
        /// </summary>
        /// <param name="dutId">源命令的关系</param>
        /// <returns>被测物</returns>
        public (Engineering, DUT) GetDUT(string dutId)
        {
            return DUTMap[dutId];
        }

        /// <summary>
        ///     设置命令字与被测物的关系
        /// </summary>
        /// <param name="dutId">源命令的关系</param>
        /// <param name="dut">被测物</param>
        public void SetDUT(string dutId, (Engineering, DUT) dut)
        {
            if (!DUTMap.ContainsKey(dutId))
                DUTMap.Add(dutId, dut);
        }

        /// <summary>
        ///     设置命令字与被测物的关系
        /// </summary>
        public void SetDUTMap(List<ScpiCommandPool> commands, Engineering engineering)
        {
            foreach (var pool in commands)
            {
                foreach (ScpiCommand command in pool)
                {
                    if (command.DUT != null)
                        this.SetDUT(command.DUT.Id, (engineering, command.DUT));
                    //TODO:if(command.IsPool)
                    //SetDUTMap(performLogic, command, engineering);
                }
            }
        }

        #endregion
    }
}
