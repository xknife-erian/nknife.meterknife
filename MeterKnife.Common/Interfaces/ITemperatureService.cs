using MeterKnife.Common.DataModels;
using NKnife.Interface;

namespace MeterKnife.Common.Interfaces
{
    public interface ITemperatureService
    {
        int Interval { get; }
        double[] TemperatureValues { get; }
        bool StartCollect(CarePort carePort);
        bool CloseCollect(CarePort carePort);
    }
}