using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Interfaces
{
    public interface ITemperatureService
    {
        int Interval { get; }
        double[] TemperatureValues { get; }
        bool StartCollect(CommPort carePort);
        bool CloseCollect(CommPort carePort);
    }
}