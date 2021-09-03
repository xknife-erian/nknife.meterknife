using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Newtonsoft.Json;

namespace NKnife.MeterKnife.Storage
{
    public class ObjectToJsonTypeHandler : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }

        public object Parse(Type destinationType, object value)
        {
            return JsonConvert.DeserializeObject(value as string, destinationType);
        }
    }

}
