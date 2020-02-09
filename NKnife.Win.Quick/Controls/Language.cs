using System.IO;
using System.Xml;
using NKnife;

// ReSharper disable once CheckNamespace
namespace System.Windows.Forms
{
    public static class Language
    {
        private const string Text = nameof(Text);
        private const string Section = nameof(Section);
        private static readonly XmlElement _LangRoot;
        private static readonly string _XmlFile;

        static Language()
        {
            _XmlFile = Path.Combine(Application.StartupPath, "language.xml");
            var xml = new XmlDocument();
            xml.Load(_XmlFile);
            _LangRoot = xml.DocumentElement;
        }

        public static string Res(this ToolStripItem item, string key)
        {
            return Get(Text, $"{item.GetType().Name}.{key}");
        }

        public static string Res(this Control control, string key)
        {
            return Get(Text, $"{control.GetType().Name}.{key}");
        }

        public static string Res(string key)
        {
            return Get(Text, $"INFO.{key}");
        }

        public static string ResSection(this ToolStripItem item, string key, string defaultValue = "")
        {
            return Get(Section, $"{item.GetType().Name}.{key}", defaultValue);
        }

        public static string ResSection(this Control control, string key, string defaultValue = "")
        {
            return Get(Section, $"{control.GetType().Name}.{key}", defaultValue);
        }

        public static string ResSection(string key, string defaultValue = "")
        {
            return Get(Section, $"INFO.{key}", defaultValue);
        }

        private static string Get(string localName, string key, string defaultValue = "")
        {
            var ele = (XmlElement) _LangRoot.SelectSingleNode($"//{localName}[@key='{key}']");
            if (ele == null || !ele.HasAttribute(Global.Culture))
            {
                if (ele == null)
                {
                    ele = _LangRoot.OwnerDocument.CreateElement(localName);
                    ele.SetAttribute("key", key);
                }

                switch (localName)
                {
                    case Text:
                    {
                        var value = key.Substring(key.IndexOf('.') + 1);
                        ele.SetAttribute(Global.Culture, value);
                        break;
                    }
                    case Section:
                    {
                        ele.SetAttribute(Global.Culture, defaultValue);
                        break;
                    }
                }

                _LangRoot.AppendChild(ele);
                _LangRoot.OwnerDocument?.Save(_XmlFile);
            }

            return ele.GetAttribute(Global.Culture);
        }
    }
}