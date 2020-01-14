using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NKnife.Utility;

namespace NKnife.Entities
{
    /// <summary>对应用程序的运行在整个平台中隶属于何种位置的相关信息的封装。eg: 单机，多机中主机，多机中从机等。
    /// </summary>
    [Obsolete("该类型已更新为Gean.Base.MultiMachine")]
    [Serializable]
    public class ApplicationStateWrapper : IXmlSerializable
    {
        public ApplicationStateWrapper()
        {
            Id = string.Format("{0}-{1}", UtilityHardware.GetMacAddress(), UtilityHardware.GetCpuID());
            State = ApplicationState.Single;
            ServerIpAddress = "127.0.0.1";
        }

        /// <summary>当前应用程序的ID
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>应用程序位置状态
        /// </summary>
        /// <value>The state of the application.</value>
        public ApplicationState State { get; set; }

        /// <summary>当为从机时，主服务的IP地址
        /// </summary>
        /// <value>The server ip address.</value>
        public string ServerIpAddress { get; set; }

        public override string ToString()
        {
            var ms = new MemoryStream();
            XmlWriter writer = new XmlTextWriter(ms, Encoding.Default);
            writer.WriteStartDocument();
            writer.WriteStartElement("ROOT");
            writer.WriteStartElement("ApplicationStateWrapper");
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("ApplicationState", State.ToString());
            writer.WriteAttributeString("ServerIpAddress", ServerIpAddress);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            return Encoding.Default.GetString(ms.ToArray());
        }

        public static ApplicationStateWrapper Parse(string content)
        {
            var asw = new ApplicationStateWrapper();
            var doc = new XmlDocument();
            doc.LoadXml(content);
            if (doc.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement.SelectSingleNode("ApplicationStateWrapper");
                if (null != node)
                {
                    var ele = (XmlElement) node;
                    asw.State = (ApplicationState) Enum.Parse(typeof (ApplicationState), ele.GetAttribute("ApplicationState"));
                    asw.Id = asw.State == ApplicationState.Slave ? ele.GetAttribute("Id") : "00";
                    asw.ServerIpAddress = ele.GetAttribute("ServerIpAddress");
                }
            }
            return asw;
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute("Id");
            State = (ApplicationState) Enum.Parse(typeof (ApplicationState), reader.GetAttribute("ApplicationState"));
            ServerIpAddress = reader.GetAttribute("ServerIpAddress");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("ApplicationState", State.ToString());
            writer.WriteAttributeString("ServerIpAddress", ServerIpAddress);
        }

        #endregion
    }

    /// <summary>
    /// 应用程序状态
    /// </summary>
    public enum ApplicationState
    {
        /// <summary>
        /// 单机处理
        /// </summary>
        Single,

        /// <summary>
        /// 主从模式的主机
        /// </summary>
        Master,

        /// <summary>
        /// 主从模式的从机
        /// </summary>
        Slave
    }
}