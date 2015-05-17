using System.Data;

namespace MeterKnife.Common.Interfaces
{
    public interface IMeterDataService
    {
        bool Save(string fileFullName, DataSet dataSet);
    }
}