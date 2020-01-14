using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NKnife.Base;
using NKnife.Configuring.Interfaces;
using NKnife.Entities;
using NKnife.Utility;

namespace NKnife.Configuring
{
    /// <summary>Gean.Configuring框架中Option服务的程序员配置
    /// </summary>
    public class OptionServiceCoderSetting// : XmlCoderSetting
    {
        #region 单件实例

        private OptionServiceCoderSetting()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static OptionServiceCoderSetting ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly OptionServiceCoderSetting Instance;

            static Singleton()
            {
                Instance = new OptionServiceCoderSetting();
            }
        }

        #endregion

        /// <summary>获取选项实例的管理器。
        /// </summary>
        public IOptionCaseManager OptionCaseManager { get; private set; }

        /// <summary>应用程序选项信息存储空间操作方法的封装的实现
        /// </summary>
        /// <value>The option data store.</value>
        public IOptionDataStore OptionDataStore { get; private set; }

        /// <summary>返回选项存储的文件名
        /// </summary>
        /// <value>The name of the option XML file.</value>
        public string OptionFileName { get; private set; }

        /// <summary>在本选项框架中各子选项采用DataTable进行保存，这里返回对应各DataTable的标记名.
        /// 当采用大XML存储时，做为XML的节点名；当采用ZIP进行存储时，做为文件的后缀名
        /// </summary>
        /// <value>The name of the data table node.</value>
        public string OptionDataTableFlagName { get; private set; }

        /// <summary>返回选项的一些信息存储的文件名
        /// </summary>
        /// <value>The option info file.</value>
        public string OptionInfoFileName { get; private set; }

        public MultiMachine State { get; set; }

        protected void Load(XmlElement source)
        {
            XmlNode node = source.SelectSingleNode("//class[@name='IOptionCaseManager']");
            if (null != node)
                OptionCaseManager = UtilityType.InterfaceBuilder<IOptionCaseManager>(node).Second;

            node = source.SelectSingleNode("//class[@name='IOptionDataStore']");
            if (null != node)
                OptionDataStore = UtilityType.InterfaceBuilder<IOptionDataStore>(node).Second;

            node = source.SelectSingleNode("//OptionXmlFileName");
            if (node != null)
                OptionFileName = node.InnerText.Trim();

            node = source.SelectSingleNode("//OptionDataTableFlagName");
            if (node != null)
                OptionDataTableFlagName = node.InnerText.Trim();

            node = source.SelectSingleNode("//OptionInfoFileName");
            if (node != null)
                OptionInfoFileName = node.InnerText.Trim();

            ParseAppStateNode(source.SelectSingleNode("//ApplicationStateWrapper"));
        }

        private void ParseAppStateNode(XmlNode source)
        {
            if (source == null) return;
            string srcstring = source.InnerText;
            if (!string.IsNullOrWhiteSpace(srcstring))
            {
                try
                {
                    State = DeserializeApplicationState(srcstring);
                }
                catch
                {
                    State = new MultiMachine();
                }
            }
            else
            {
                State = new MultiMachine();
            }
        }

        public bool Save()
        {
            return true;
//            XmlNode node = Element.SelectSingleNode("//ApplicationStateWrapper");
//            if (node != null)
//                node.InnerText = SerializeApplicationState();
//            return base.Save();
        }

        private string SerializeApplicationState()
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var xs = new XmlSerializer(typeof (MultiMachine));
            xs.Serialize(sw, State);
            sw.Flush();
            sw.Close();
            return sb.ToString();
        }

        private MultiMachine DeserializeApplicationState(string wrapperStr)
        {
            var sw = new StringReader(wrapperStr);
            var xs = new XmlSerializer(typeof (MultiMachine));
            var wrapper = (MultiMachine) xs.Deserialize(sw);
            return wrapper;
        }
    }
}