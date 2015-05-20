using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;
using NKnife.Events;

namespace MeterKnife.Kernel
{
    public class MeterKernel : IMeterKernel
    {
        private bool _OnCollected;

        public string DataPath { get; set; }

        public Dictionary<int, List<int>> GpibDictionary { get; set; }

        public bool CollectBeginning
        {
            get { return _OnCollected; }
            set
            {
                _OnCollected = value;
                OnCollectedEvent(new EventArgs<bool>(value));
            }
        }

        public event EventHandler<EventArgs<bool>> Collected;

        protected virtual void OnCollectedEvent(EventArgs<bool> e)
        {
            EventHandler<EventArgs<bool>> handler = Collected;
            if (handler != null) handler(this, e);
        }

        public MeterKernel()
        {
            GpibDictionary = new Dictionary<int, List<int>>();
        }
    }
}
