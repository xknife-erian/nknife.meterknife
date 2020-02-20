using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using NKnife;
using NLog.LayoutRenderers;

// ReSharper disable once CheckNamespace
namespace System.Windows.Forms
{
    /// <summary>
    /// 窗体应用的语言管理扩展方法类
    /// </summary>
    public static class Language
    {
        /// <summary>
        /// 控件简单文本在语言文件的LocalName
        /// </summary>
        private const string TEXT = nameof(TEXT);
        /// <summary>
        /// 长文本在语言文件的LocalName
        /// </summary>
        private const string SECTION = nameof(SECTION);
        /// <summary>
        /// 语言管理文件(XML文件)
        /// </summary>
        private static readonly string _XmlFile;
        /// <summary>
        /// 语言管理文件(XML文件)的根节点
        /// </summary>
        private static readonly XmlElement _RootElement;
        /// <summary>
        /// 语言管理文件(XML文件)的<see cref="XmlDocument"/>
        /// </summary>
        private static readonly XmlDocument _Xml;

        static Language()
        {
            string fileName = "Languages.xml";
            _XmlFile = Path.Combine(Application.StartupPath, fileName);
            _Xml = new XmlDocument();
            if (File.Exists(_XmlFile))
            {
                _Xml.Load(_XmlFile);
            }
            else
            {
                var fn = $"{Application.ProductName}.{fileName}";
                _XmlFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fn);
                if (!File.Exists(_XmlFile))
                {
                    _Xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Languages />");
                    _Xml.Save(_XmlFile);
                }
            }
            _RootElement = _Xml.DocumentElement;
        }

        public static void Res(this Control ctr, params Control[] controls)
        {
            foreach (var control in controls)
            {
                Get(TEXT, $"{control.Text}");
            }
        }

        public static void Res(this Control ctr, params ToolStripItem[] items)
        {
            foreach (var stripItem in items)
            {
                Get(TEXT, $"{stripItem.Text}");
            }
        }

        public static void Res(this Control ctr, params ColumnHeader[] items)
        {
            foreach (var header in items)
            {
                Get(TEXT, $"{header.Text}");
            }
        }

        public static string Res(this ToolStripItem item)
        {
            return Get(TEXT, item.Text);
        }

        public static string Res(this ToolStripItem item, string key)
        {
            return Get(TEXT, key);
        }

        public static string Res(this Control control, string key)
        {
            return Get(TEXT, key);
        }

        public static string Res(string key)
        {
            return Get(TEXT, key);
        }

        public static string ResF(this ToolStripItem item, string key, params object[] values)
        {
            return Get(TEXT, string.Format($"{key}", values));
        }

        public static string ResF(this Control control, string key, params object[] values)
        {
            return Get(TEXT, string.Format($"{key}", values));
        }

        public static string ResF(string key, params object[] values)
        {
            return Get(TEXT, string.Format($"{key}", values));
        }

        public static string ResSection(this ToolStripItem item, string key, string defaultValue = "")
        {
            return Get(SECTION, $"{key}", defaultValue);
        }

        public static string ResSection(this Control control, string key, string defaultValue = "")
        {
            return Get(SECTION, $"{key}", defaultValue);
        }

        public static string ResSection(string key, string defaultValue = "")
        {
            return Get(SECTION, $"{key}", defaultValue);
        }

        private static string Get(string localName, string keyName, string defaultValue = "")
        {
            string end = string.Empty;
            string key;
            if (keyName.EndsWith(":") || keyName.EndsWith("："))
            {
                key = keyName.Substring(0, keyName.Length - 1);
                end = keyName[keyName.Length - 1].ToString();
            }
            else
            {
                key = keyName;
            }
            var ele = (XmlElement) _RootElement?.SelectSingleNode($"//{localName}[@key='{key}']");
            if (ele == null || !ele.HasAttribute(Global.Culture))
            {
                if (ele == null)
                {
                    ele = _Xml.CreateElement(localName);
                    ele.SetAttribute("key", key);
                }

                switch (localName)
                {
                    case TEXT:
                    {
                        ele.SetAttribute(Global.Culture, key);
                        break;
                    }
                    case SECTION:
                    {
                        ele.SetAttribute(Global.Culture, defaultValue);
                        break;
                    }
                }

                _RootElement?.AppendChild(ele);
                _RootElement?.OwnerDocument?.Save(_XmlFile);
            }

            return $"{ele?.GetAttribute(Global.Culture)}{end}";
        }
    }
}