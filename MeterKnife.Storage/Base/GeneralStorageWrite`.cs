using NKnife.MeterKnife.Common;

namespace NKnife.MeterKnife.Storage.Base
{
    public class GeneralStorageWrite<T> : BaseStorageWrite<T>
    {
        public GeneralStorageWrite(IStorageManager storageManager) : base(storageManager)
        {
        }
    }
}