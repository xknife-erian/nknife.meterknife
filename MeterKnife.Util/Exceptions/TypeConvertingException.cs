using System;

namespace MeterKnife.Util.Exceptions
{
    /// <summary>
    /// 类型转换异常
    /// </summary>
    [Serializable]
    public class TypeConvertingException : NKnifeException
    {
        public TypeConvertingException(string exceptionMsg, Exception e)
            : base(exceptionMsg, e)
        {

        }
    }
}
