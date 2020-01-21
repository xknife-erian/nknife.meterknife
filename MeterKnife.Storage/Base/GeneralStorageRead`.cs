using NKnife.MeterKnife.Common;

namespace NKnife.MeterKnife.Storage.Base
{
    public class GeneralStorageRead<T> : BaseStorageRead<T>
    {
        public GeneralStorageRead(IStorageManager storageManager) : base(storageManager)
        {
        }
    }
}