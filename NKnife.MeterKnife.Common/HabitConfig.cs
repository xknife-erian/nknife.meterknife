using System.IO;
using System.Xml;
using Newtonsoft.Json;
using NKnife.MeterKnife.Base;
using NKnife.XML;
using NLog;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     软件的“使用习惯记录文件”的记录保存与读取服务
    /// </summary>
    public class HabitConfig : IHabitConfig
    {
        /// <summary>
        /// 用户测量数据的保存路径。关键选项。
        /// </summary>
        public const string KEY_MetricalData_Path = "MetricalData_Path";

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
            var ele = _optionXmlDocument.DocumentElement?.SelectSingleNode(key) as XmlElement ?? _optionXmlDocument.CreateElement(key);
            ele.RemoveAll();
            var json = JsonConvert.SerializeObject(value);
            ele.SetCDataElement(json);
            _optionXmlDocument.DocumentElement?.AppendChild(ele);
            _optionXmlDocument.Save(_optionFile);
        }

    }
}