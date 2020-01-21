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

        private const string OPTION_FILE = "Habit.xml";
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
        }

        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        public T GetOptionValue<T>(string key, T defaultValue)
        {
            var ele = _optionXmlDocument.DocumentElement?.SelectSingleNode(key);
            if (ele == null)
            {
                SetOptionValue(key, defaultValue);
                ele = _optionXmlDocument.DocumentElement?.SelectSingleNode(key);
            }
            return JsonConvert.DeserializeObject<T>(ele.GetCDataElement().Value);
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

    }
}