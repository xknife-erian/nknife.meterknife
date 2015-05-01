using MeterKnife.Kernel.DataModels;
using MonitorKnife.Common.DataModels;
using MonitorKnife.Common.Interfaces;

namespace MeterKnife.Kernel.Interfaces
{
    public interface ICollectDataDoor
    {
        void SetSource(ICollectSource source);
        bool Enable { get; set; }
        void SaveTemperatureData(CollectData collectData);
        void SaveCollectData(CollectData collectData);
    }
}
