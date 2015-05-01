using System;
using MeterKnife.Kernel.DataModels;
using MonitorKnife.Common.DataModels;
using MonitorKnife.Common.Interfaces;
using NKnife.Configuring.Interfaces;
using NKnife.Events;

namespace MeterKnife.Kernel.Interfaces
{
    public interface ICentre
    {
        IUserApplicationData UserData { get; }
        bool IsFirstOpen { get; set; }
        InstrumentCollection Instruments { get; set; }
        IInstrument CurrentInstrument { get; set; }
        event EventHandler<EventArgs<IInstrument>> CurrentInstrumentChanged;

    }
}