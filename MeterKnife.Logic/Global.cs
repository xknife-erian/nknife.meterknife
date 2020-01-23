using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Logic
{
    public class Global : IGlobal
    {
        //private readonly ITemperatureService _temperatureService;
        private bool _onCollected;

        public Global()//(ITemperatureService temperatureService))
        {
            //_temperatureService = temperatureService;
            GpibDictionary = new Dictionary<Slot, List<int>>();
        }

        public string DataPath { get; set; }

        /// <summary>
        ///     指定的端口下的已使用的GPIB地址,不同的端口下的地址可以重复,同一端口下的地址不允许重复
        /// </summary>
        public Dictionary<Slot, List<int>> GpibDictionary { get; }

        /// <summary>
        ///     更新采集状态
        /// </summary>
        public void UpdateCollectState(Slot carePort, int address, bool value, string scpiGroupKey)
        {
            _onCollected = value;
//            if (_onCollected)
//                _temperatureService.StartCollect(carePort);
//            else
//                _temperatureService.CloseCollect(carePort);
            OnCollectedEvent(new CollectedEventArgs(carePort, address, value, scpiGroupKey));
        }

        public event EventHandler<CollectedEventArgs> Collected;

        protected virtual void OnCollectedEvent(CollectedEventArgs e)
        {
            var handler = Collected;
            handler?.Invoke(this, e);
        }
    }
}