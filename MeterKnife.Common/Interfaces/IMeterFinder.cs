namespace MeterKnife.Common.Interfaces
{
    public interface IMeterFinder
    {
        IMeter Find(int port, int gpib);
    }
}