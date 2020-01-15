using System.Data;

namespace NKnife.Databases.Interface
{
    public interface IDataReaderProcess<out T>
    {
        T Process(IDataReader data);
    }
}
