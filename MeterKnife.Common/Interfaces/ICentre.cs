using System;
using MeterKnife.Common.DataModels;
using NKnife.Configuring.Interfaces;
using NKnife.Events;

namespace MeterKnife.Common.Interfaces
{
    public interface ICentre
    {
        IUserApplicationData UserData { get; }
        bool IsFirstOpen { get; set; }
        MeterCollection Instruments { get; set; }
        IMeter CurrentInstrument { get; set; }
        event EventHandler<EventArgs<IMeter>> CurrentInstrumentChanged;

    }
}