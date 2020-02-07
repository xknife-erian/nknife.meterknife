using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Workbench.Base.Gateways
{
    /// <summary>
    ///     当仪器集合的中item数量发生变化时发生的事件数据类
    /// </summary>
    public class InstrumentCollectionChangedEventArgs : EventArgs
    {
        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, Instrument instrument)
        {
            ChangedAction = action;
            Instruments = new[] {instrument};
        }

        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, params Instrument[] instruments)
        {
            ChangedAction = action;
            Instruments = instruments;
        }

        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, ICollection<Instrument> instruments)
        {
            ChangedAction = action;
            Instruments = new Instrument[instruments.Count];
            instruments.CopyTo(Instruments, 0);
        }

        public Instrument[] Instruments { get; set; }
        public NotifyCollectionChangedAction ChangedAction { get; set; }
    }
}