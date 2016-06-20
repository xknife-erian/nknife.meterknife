using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.DemoApplication
{
    public class DemoMeterKernel : IMeterKernel
    {
        public string DataPath { get; set; }
        public Dictionary<CommPort, List<int>> GpibDictionary { get; private set; }
        public Dictionary<BaseMeter, DockContent> MeterContents { get; private set; }

        public void UpdateCollectState(CommPort carePort, int address, bool isCollected, string key)
        {
            OnCollected(new CollectedEventArgs(carePort, address, isCollected, "key"));
        }

        public event EventHandler<CollectedEventArgs> Collected;

        protected virtual void OnCollected(CollectedEventArgs e)
        {
            EventHandler<CollectedEventArgs> handler = Collected;
            if (handler != null)
                handler(this, e);
        }
    }
}
