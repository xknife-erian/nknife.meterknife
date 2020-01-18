using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeterKnife.Common;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Util;
using NLog;

namespace MeterKnife.Kernel.Services
{
    public class CareTemperatureService : ITemperatureService
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly BaseAntCommService _comm;
        private readonly Dictionary<Slot, bool> _portStartMap = new Dictionary<Slot, bool>();
        private readonly CareTemperatureHandler _temperatureHandler;

        public CareTemperatureService(BaseAntCommService comm, CareTemperatureHandler temperatureHandler)
        {
            _comm = comm;
            _temperatureHandler = temperatureHandler;
            TemperatureValues = new double[1];
        }

        public int Interval => 5000;

        public double[] TemperatureValues { get; private set; }

        public bool StartCollect(Slot carePort)
        {
            Task.Factory.StartNew(() =>
            {
                var isStart = true;
                if (!_portStartMap.TryGetValue(carePort, out isStart)) _portStartMap.Add(carePort, true);
                _portStartMap[carePort] = true;
                if (_portStartMap.Count > 1) //如果采集值的数量（多路温度采集）大于1时
                {
                    var v = TemperatureValues[0];
                    TemperatureValues = new double[_portStartMap.Count];
                    TemperatureValues[0] = v;
                }

                _comm.Bind(carePort, _temperatureHandler);
                //初始化完成,进入采集循环
                while (_portStartMap[carePort])
                {
                    _comm.SendCommands(carePort, CommandUtil.TEMP());
                    Thread.Sleep(Interval);
                }
            });
            return true;
        }

        public bool CloseCollect(Slot carePort)
        {
            _portStartMap[carePort] = false;
            _comm.Remove(carePort, _temperatureHandler);
            return true;
        }
    }
}