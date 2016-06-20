using MeterKnife.Common.DataModels;
using NKnife.Interface;

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