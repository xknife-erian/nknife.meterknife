using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Interfaces
{
    public interface ICollectDataDoor
    {
        void SetSource(ICollectSource source);
        bool Enable { get; set; }
        void SaveTemperatureData(CollectData collectData);
        void SaveCollectData(CollectData collectData);
    }
}
