using System;

namespace NKnife.Exceptions
{
    /// <summary>
    ///     NKnife的的基础异常类，所有的异常从本类派生
    /// </summary>
    [Serializable]
    public class NKnifeException : ApplicationException
    {
        /// <summary>
        ///     异常编号
        /// </summary>
        protected int _ErrorNo;

        /// <summary>
        ///     构造函数
        /// </summary>
        public NKnifeException()
            : this(0, null, null)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public NKnifeException(string message, Exception innerException)
            : this(0, message, innerException)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public NKnifeException(string message)
            : this(0, message)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="errorNo">异常编号</param>
        /// <param name="message">异常消息</param>
        public NKnifeException(int errorNo, string message)
            : this(errorNo, message, null)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="errorNo">异常编号</param>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public NKnifeException(int errorNo, string message, Exception innerException)
            : base(message, innerException)
        {
            _ErrorNo = errorNo;
        }

        /// <summary>
        ///     异常编号
        /// </summary>
        public int ErrorNo
        {
            get { return _ErrorNo; }
        }

        /// <summary>
        ///     查找原始的异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns>原始的异常</returns>
        public static Exception FindSourceException(Exception e)
        {
            Exception e1 = e;
            while (e1 != null)
            {
                e = e1;
                e1 = e1.InnerException;
            }
            return e;
        }

        /// <summary>
        ///     从异常树种查找指定类型的异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="expectedExceptionType">期待的异常类型</param>
        /// <returns>所要求的异常，如果找不到，返回null</returns>
        public static Exception FindSourceException(Exception e, Type expectedExceptionType)
        {
            while (e != null)
            {
                if (e.GetType() == expectedExceptionType)
                {
                    return e;
                }
                e = e.InnerException;
            }
            return null;
        }
    }
}