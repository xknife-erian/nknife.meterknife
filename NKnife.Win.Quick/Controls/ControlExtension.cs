using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NKnife;

// ReSharper disable once CheckNamespace
namespace System.Windows.Forms
{
    public static class ControlExtension
    {
        private static readonly XmlElement _LangRoot;
        private static readonly string _XmlFile;

        static ControlExtension()
        {
            _XmlFile = Path.Combine(Application.StartupPath, "language.xml");
            var xml = new XmlDocument();
            xml.Load(_XmlFile);
            _LangRoot = xml.DocumentElement; 
        }

        public static string Language(this ToolStripItem item, string key)
        {
            return GetTxtValue($"{item.GetType().Name}.{key}");
        }

        public static string Language(this Control control, string key)
        {
            return GetTxtValue($"{control.GetType().Name}.{key}");
        }

        private static string GetTxtValue(string key)
        {
            var ele = (XmlElement) _LangRoot.SelectSingleNode($"//text[@key='{key}']");
            if (ele == null || !ele.HasAttribute(Global.Culture))
            {
                var value = key.Substring(key.IndexOf('.') + 1);
                if (ele == null)
                {
                    ele = _LangRoot.OwnerDocument.CreateElement("text");
                    ele.SetAttribute("key", key);
                }

                ele.SetAttribute(Global.Culture, value);
                _LangRoot.AppendChild(ele);
                _LangRoot.OwnerDocument?.Save(_XmlFile);
                return value;
            }

            return ele.GetAttribute(Global.Culture);
        }
    }
}
