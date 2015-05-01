using MonitorKnife.Common.Interfaces;

namespace MeterKnife.Kernel.Interfaces
{
    public interface IInstrumentFinder
    {
        IInstrument Find(int port, int gpib);
    }
}