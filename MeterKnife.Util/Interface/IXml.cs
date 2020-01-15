using System.Xml;

namespace NKnife.Interface
{
    /// <summary>
    /// 描述一个可以序列化与反序列化为XML节点的接口
    /// </summary>
    public interface IXml
    {
        /// <summary>
        /// 将类型通过xml的一个节点表达出来，
        /// </summary>
        /// <param name="parent">该节点将要附回到的XmlDocument文档</param>
        /// <returns>该节点已创建但暂未被附加到XmlDoucment的任何位置</returns>
        XmlElement ToXml(XmlDocument parent);

        /// <summary>
        /// 从一个对应的XML节点解析出来本类型的数据内容
        /// </summary>
        /// <param name="element">一个对应的XML节点</param>
        void Parse(XmlElement element);
    }
}
