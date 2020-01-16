using System.Collections.Generic;

namespace MeterKnife.Util.Protocol
{
    /// <summary>
    /// 描述一个通讯会话中一次交易的内容的封装。
    /// </summary>
    /// <typeparam name="TData">内容在编程过程所使用的数据形式,可视化的读取形式</typeparam>
    public interface IProtocol<TData> //: IEquatable<TData>
    {
        /// <summary>本协议的命令字.
        /// </summary>
        /// <value>The command.</value>
        TData Command { get; set; }

        /// <summary>
        ///     协议命令字参数
        /// </summary>
        /// <value>The command param.</value>
        TData CommandParam { get; set; }

        /// <summary>
        ///     获取协议的固定数据
        ///     Infomations,Tags均属于协议的内容
        ///     Infomations:固定数据，按协议规定的必须每次携带的数据
        ///     Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        Dictionary<string, TData> Infomations { get; }

        /// <summary>
        ///     获取协议的大数据
        ///     Infomations,Tags均属于协议的内容
        ///     Infomations:固定数据，按协议规定的必须每次携带的数据
        ///     Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        List<object> Tags { get; set; }

        /// <summary>本协议的家族.
        /// </summary>
        /// <value>The family.</value>
        string Family { get; set; }

        IProtocol<TData> NewInstance();
    }
}