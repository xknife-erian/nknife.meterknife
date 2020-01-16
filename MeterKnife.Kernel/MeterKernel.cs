using System;
using System.Collections.Generic;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Kernel
{
    public class MeterKernel : IMeterKernel
    {
        //private readonly ITemperatureService _temperatureService;
        private bool _onCollected;

        public MeterKernel()//(ITemperatureService temperatureService))
        {
            //_temperatureService = temperatureService;
            GpibDictionary = new Dictionary<CommPort, List<int>>();
            MeterContents = new Dictionary<BaseMeter, DockContent>();
        }

        public string DataPath { get; set; }

        /// <summary>
        ///     指定的端口下的已使用的GPIB地址,不同的端口下的地址可以重复,同一端口下的地址不允许重复
        /// </summary>
        public Dictionary<CommPort, List<int>> GpibDictionary { get; }

        /// <summary>
        ///     仪表面板
        /// </summary>
        public Dictionary<BaseMeter, DockContent> MeterContents { get; }

        /// <summary>
        ///     更新采集状态
        /// </summary>
        public void UpdateCollectState(CommPort carePort, int address, bool value, string scpiGroupKey)
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