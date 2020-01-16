using System;

namespace NKnife.Protocol
{
    /// <summary>
    /// 一个描述协议的集合，协议族
    /// </summary>
    /// <typeparam name="TData">内容在编程过程所使用的数据形式,可视化的读取形式</typeparam>
    public interface IProtocolFamily<TData>
    {
        string FamilyName { get; set; }

        IProtocolCommandParser<TData> CommandParser { get; set; }

        IProtocol<TData> Build(TData command);

        /// <summary>根据远端得到的数据包解析，将数据填充到本实例中，与Generate方法相对
        /// </summary>
        IProtocol<TData> Parse(TData command, TData datagram);

        /// <summary>根据当前协议实例生成为在传输过程所使用的数据形式
        /// </summary>
        TData Generate(IProtocol<TData> protocol);

        void AddPackerGetter(Func<TData, IProtocolPacker<TData>> func);

        void AddPackerGetter(TData command, Func<TData, IProtocolPacker<TData>> func);
    }
}
