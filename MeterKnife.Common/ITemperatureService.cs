using MeterKnife.Common.DataModels;

namespace MeterKnife.Common
{
    public interface ITemperatureService
    {
        int Interval { get; }
        double[] TemperatureValues { get; }
        bool StartCollect(Slot carePort);
        bool CloseCollect(Slot carePort);
    }
}