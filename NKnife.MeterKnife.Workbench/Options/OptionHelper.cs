using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.MeterKnife.Workbench.Options
{
    public static class OptionHelper
    {
        /// <summary>
        /// 向字典中添加键值对，如果已有键，则更新值；如果没有，则添加。
        /// </summary>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Update(this IDictionary<string, object> map, string key, object value)
        {
            if (map == null)
                map = new Dictionary<string, object>();
            if (map.ContainsKey(key))
                map[key] = value;
            else
                map.Add(key, value);
        }
    }
}
