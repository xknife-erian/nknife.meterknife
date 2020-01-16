using System;
using System.IO;
using System.Xml;

namespace MeterKnife.Util.Protocol.Generic.Xml
{
    /// <summary>
    /// 定义协议XML节点中，根节点的节点名是协议的命令字
    /// </summary>
    public class XmlFirstElementCommandParser : StringProtocolCommandParser
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        
        public override string GetCommand(string protocol)
        {
            string command = string.Empty;
            //使用XML的Reader方式进行流模式的读取，避免以DOM的方式进行XML的整体加载，以加快读取的速度
            //这种方式不对XML进行验证，当流读取出第一个XML节点后即退出
            using (var reader = new XmlTextReader(new StringReader(protocol)))
            {
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            command = reader.Name;
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.Warn(string.Format("无法解析的协议字符串。{0}，异常：{1}", protocol, e));
                }
            }
            return command;
        }
    }
}
