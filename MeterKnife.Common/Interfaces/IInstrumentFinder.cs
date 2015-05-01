namespace MeterKnife.Common.Interfaces
{
    public interface IInstrumentFinder
    {
        IInstrument Find(int port, int gpib);
    }
}