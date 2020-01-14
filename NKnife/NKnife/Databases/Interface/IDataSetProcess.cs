using System.Data;

namespace NKnife.Databases.Interface
{
    public interface IDataSetProcess<out T>
    {
        T Process(DataSet data);
    }
}
