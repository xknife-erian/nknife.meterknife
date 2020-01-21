using System.IO;
using System.Xml;
using Newtonsoft.Json;
using NKnife.XML;
using NLog;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     软件的选项与用户习惯保存服务
    /// </summary>
    public class HabitConfig
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private const string OPTION_FILE = "option.xml";
        private readonly string _optionFile;
        private readonly XmlDocument _optionXmlDocument;

        public HabitConfig(PathManager pathManager)
        {
            _optionFile = Path.Combine(pathManager.UserApplicationDataPath, OPTION_FILE);
            if (!File.Exists(_optionFile))
            {
                _optionXmlDocument = XmlHelper.CreateNewDocument(_optionFile, "options");
            }
            else
            {
                _optionXmlDocument = new XmlDocument();
                _optionXmlDocument.Load(_optionFile);
            }

            if (!TryGetOptionValue("firstOpenApp", out bool firstOpenApp))
            {
                SetOptionValue("firstOpenApp", false);
                firstOpenApp = true;
            }
        }

        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        public bool TryGetOptionValue<T>(string key, out T value)
        {
            value = default;
            var ele = _optionXmlDocument.DocumentElement?.SelectSingleNode(key);
            if (ele == null)
                return false;
            value = JsonConvert.DeserializeObject<T>(ele.GetCDataElement().Value);
            return true;
        }

        /// <summary>
        ///     设置指定Key的选项的值，值对象序列化成Json保存
        /// </summary>
        public void SetOptionValue(string key, object value)
        {
            var ele = _optionXmlDocument.DocumentElement?.SelectSingleNode(key) as XmlElement;
            if (ele == null)
                ele = _optionXmlDocument.CreateElement(key);
            ele.RemoveAll();
            var json = JsonConvert.SerializeObject(value);
            ele.SetCDataElement(json);
            _optionXmlDocument.DocumentElement?.AppendChild(ele);
            _optionXmlDocument.Save(_optionFile);
        }


        private static void Initialize(string resXml, string fileName, string rootPath)
        {
            var path = Path.Combine(rootPath, $"{fileName}.xml");
            if (!File.Exists(path))
            {
                var xml = new XmlDocument();
                xml.LoadXml(resXml);
                xml.Save(path);
                _Logger.Info($"首次运行，创建：{path}");
            }
        }
    }
}