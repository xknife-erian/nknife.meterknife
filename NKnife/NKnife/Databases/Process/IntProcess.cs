using System.Data;
using NKnife.Databases.Interface;

namespace NKnife.Databases.Process
{
    public class IntProcess : IDataReaderProcess<int>
    {
        public int Process(IDataReader data)
        {
            while (data.Read())
            {
                var i = data.GetInt32(0);
            }
            return -1;
        }
    }
}
