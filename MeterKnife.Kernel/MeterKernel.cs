using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.Base;
using NKnife.Events;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Kernel
{
    public class MeterKernel : IMeterKernel
    {
        private bool _OnCollected;

        public string DataPath { get; set; }

        public Dictionary<int, List<int>> GpibDictionary { get; private set; }
        public Dictionary<BaseMeter, DockContent> MeterContents { get; private set; }

        public void CollectBeginning(int address, bool value)
        {
            _OnCollected = value;
            OnCollectedEvent(new CollectedEventArgs(address, value));
        }

        public event EventHandler<CollectedEventArgs> Collected;

        protected virtual void OnCollectedEvent(CollectedEventArgs e)
        {
            EventHandler<CollectedEventArgs> handler = Collected;
            if (handler != null) handler(this, e);
        }

        public MeterKernel()
        {
            GpibDictionary = new Dictionary<int, List<int>>();
            MeterContents = new Dictionary<BaseMeter, DockContent>();
        }
    }
}
