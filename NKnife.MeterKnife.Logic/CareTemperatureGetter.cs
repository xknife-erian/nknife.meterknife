using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Common.Tunnels;

namespace NKnife.MeterKnife.Logic
{
    /// <summary>
    /// MeterCare专用的获取温度值的获取器
    /// </summary>
    public class CareTemperatureGetter : ITemperatureGetter
    {
        private readonly ISlotService _comm;
        private readonly Dictionary<Slot, bool> _portStartMap = new Dictionary<Slot, bool>();
        private readonly CareTemperatureHandler _tempHandler;

        public CareTemperatureGetter(ISlotService comm, CareTemperatureHandler tempHandler)
        {
            _comm = comm;
            _tempHandler = tempHandler;
        }

        /// <summary>
        ///     获取间隔
        /// </summary>
        public int Interval => 5000;

        /// <summary>
        ///     启动采集
        /// </summary>
        /// <param name="slot">采集的端口</param>
        public bool StartCollect(Slot slot)
        {
            Task.Factory.StartNew(() =>
            {
                if (!_portStartMap.TryGetValue(slot, out _))
                    _portStartMap.Add(slot, true);
                _portStartMap[slot] = true;
                _comm.Bind(slot, _tempHandler);
                //初始化完成,进入采集循环
                while (_portStartMap[slot])
                {
                    _comm.SendCommands(slot, CareScpiHelper.TEMP());
                    Thread.Sleep(Interval);
                }
            });
            return true;
        }

        /// <summary>
        ///     关闭采集
        /// </summary>
        /// <param name="slot">采集的端口</param>
        public bool CloseCollect(Slot slot)
        {
            _portStartMap[slot] = false;
            _comm.Remove(slot, _tempHandler);
            return true;
        }
    }
}