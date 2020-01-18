using NKnife.MeterKnife.Slots;

namespace NKnife.MeterKnife.Tests.Infrastructure
{
    public class AppMock
    {
        public AppMock(ISlot slot, IDataBus dataBus, IInstrument instrument)
        {
            Slot = slot;
            DataBus = dataBus;
            Instrument = instrument;
        }

        public ISlot Slot { get; }

        public IDataBus DataBus { get; }

        public IInstrument Instrument { get; }
    }
}