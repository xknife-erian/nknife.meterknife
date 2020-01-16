using System;

namespace MeterKnife.Util.Utility
{
    /// <summary>
    /// 相关Guid的封装类
    /// </summary>
    public static class UtilityGuid
    {
        /// <summary>
        /// 获取一个Guid，格式为没有连接符的长字符串。
        /// </summary>
        static public string Get()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
