using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NLog;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Holistic
{
    /// <summary>
    ///     面向全局的数据采集广播服务。该服务以事件方式，广播采集指令所采集到的数据。
    /// </summary>
    public class AcquisitionService : IAcquisitionService
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly short _habitDroppedDataCount;
        private readonly Dictionary<string, short> _droppedMap = new Dictionary<string, short>();

        public AcquisitionService(IHabitManager habit)
        {
            _habitDroppedDataCount = habit.GetOptionValue(HabitKey.Plot_DroppedDataCount, (short)5);
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 999;
        public string Description { get; } = "面向全局的采集数据广播服务";

        #endregion

        #region Implementation of IAcquisitionService

        /// <summary>
        ///     当采集指令采集到数据时发生。
        /// </summary>
        public event EventHandler<AcquisitionEventArgs> Acquired;

        /// <summary>
        ///     当测量指令采集到数据时，将数据置入<see cref="AcquisitionService"/>服务中
        /// </summary>
        /// <param name="dut">指定的工程与被测物</param>
        /// <param name="data">数据</param>
        public void AddValue((Project, DUT) dut, MeasureData data)
        {
            var key = $"{dut.Item1.Id}///{dut.Item2.Id}";
            if (!_droppedMap.ContainsKey(key))
            {
                _droppedMap.Add(key, 0);
            }
            if (_droppedMap[key] < _habitDroppedDataCount)
            {
                _droppedMap[key]++;
                _Logger.Info($"按配置，丢弃起始采集的一些数据{_droppedMap[key]}：{key}:{data}");
                return;
            }

            Task.Factory.StartNew(OnAcquired, new AcquisitionEventArgs(dut, data));
        }

        protected virtual void OnAcquired(object e)
        {
            Acquired?.Invoke(this, (AcquisitionEventArgs) e);
        }

        #endregion
    }
}