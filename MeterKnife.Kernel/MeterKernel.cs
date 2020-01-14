using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.Base;
using NKnife.Events;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Kernel
{
    public class MeterKernel : IMeterKernel
    {
        protected bool _OnCollected;
        private readonly ITemperatureService _TemperatureService = DI.Get<ITemperatureService>();

        public string DataPath { get; set; }

        /// <summary>
        /// 指定的端口下的已使用的GPIB地址,不同的端口下的地址可以重复,同一端口下的地址不允许重复
        /// </summary>
        public Dictionary<CommPort, List<int>> GpibDictionary { get; private set; }

        /// <summary>
        /// 仪表面板
        /// </summary>
        public Dictionary<BaseMeter, DockContent> MeterContents { get; private set; }

        /// <summary>
        /// 更新采集状态
        /// </summary>
        public void UpdateCollectState(CommPort carePort, int address, bool value, string scpiGroupKey)
        {
            _OnCollected = value;
            if (_OnCollected)
                _TemperatureService.StartCollect(carePort);
            else
                _TemperatureService.CloseCollect(carePort);
            OnCollectedEvent(new CollectedEventArgs(carePort, address, value, scpiGroupKey));
        }

        public event EventHandler<CollectedEventArgs> Collected;

        protected virtual void OnCollectedEvent(CollectedEventArgs e)
        {
            EventHandler<CollectedEventArgs> handler = Collected;
            if (handler != null) 
                handler(this, e);
        }

        public MeterKernel()
        {
            GpibDictionary = new Dictionary<CommPort, List<int>>();
            MeterContents = new Dictionary<BaseMeter, DockContent>();
        }
    }
}
