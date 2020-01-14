using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace NKnife.Xaml
{

    /// <summary>
    /// 对XAML进行编辑操作的辅助类:
    /// 对选中的XAML进行操作; 对XAML代码进行对齐整理; 对XAML标记进行着色显示等
    /// </summary>
    public class XamlHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="range">TextRange</param>
        /// <returns>return a string serialized from the TextRange</returns>
        public static string GetTextRangeXaml(TextRange range)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }

            var mstream = new MemoryStream();
            range.Save(mstream, DataFormats.Xaml);

            //must move the stream pointer to the beginning since range.save() will move it to the end.
            mstream.Seek(0, SeekOrigin.Begin);

            //Create a stream reader to read the xaml.
            var stringReader = new StreamReader(mstream);

            return stringReader.ReadToEnd();
        }

        /// <summary>
        /// Set XML to TextRange.Xml property.
        /// </summary>
        /// <param name="range">TextRange</param>
        /// <param name="xaml">XAML to be set</param>
        public static void SetTextRangeXaml(TextRange range, string xaml)
        {
            if (null == xaml)
            {
                throw new ArgumentNullException("xaml");
            }
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }

            var mstream = new MemoryStream();
            var sWriter = new StreamWriter(mstream);

            mstream.Seek(0, SeekOrigin.Begin); //this line may not be needed.
            sWriter.Write(xaml);
            sWriter.Flush();

            //move the stream pointer to the beginning.
            mstream.Seek(0, SeekOrigin.Begin);

            range.Load(mstream, DataFormats.Xaml);
        }

        /// <summary>
        /// Parse a string to WPF object.
        /// </summary>
        /// <param name="str">string to be parsed</param>
        /// <returns>return an object</returns>
        public static object ParseXaml(string str)
        {
            var ms = new MemoryStream(str.Length);
            var sw = new StreamWriter(ms);
            sw.Write(str);
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var pc = new ParserContext();
            pc.BaseUri = new Uri(System.Environment.CurrentDirectory + "/");

            return XamlReader.Load(ms, pc);
        }

        public static string IndentXaml(string xaml)
        {
            //open the string as an XML node
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xaml);
            var nodeReader = new XmlNodeReader(xmlDoc);

            //write it back onto a stringWriter
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter)
                                {
                                    Formatting = Formatting.Indented,
                                    Indentation = 4,
                                    IndentChar = ' '
                                };
            xmlWriter.WriteNode(nodeReader, false);

            string result = stringWriter.ToString();
            xmlWriter.Close();

            return result;
        }

        public static string RemoveIndentation(string xaml)
        {
            if (xaml.Contains("\r\n    "))
            {
                return RemoveIndentation(xaml.Replace("\r\n    ", "\r\n"));
            }
            else
            {
                return xaml.Replace("\r\n", "");
            }
        }

        public static string ColoringXaml(string xaml)
        {
            string value = "";
            const string s1 = "<Section xml:space=\"preserve\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>";
            const string s2 = "</Paragraph></Section>";

            string[] strs = xaml.Split(new char[] { '<' });
            for (int i = 1; i < strs.Length; i++)
            {
                value += ProcessEachTag(strs[i]);
            }
            return s1 + value + s2;
        }

        static string ProcessEachTag(string str)
        {
            const string front = "<Run Foreground=\"Blue\">&lt;</Run>";
            const string end = "<Run Foreground=\"Blue\">&gt;</Run>";
            const string frontWithSlash = "<Run Foreground=\"Blue\">&lt;/</Run>";
            const string endWithSlash = "<Run Foreground=\"Blue\"> /&gt;</Run>";
            const string tagNameStart = "<Run FontWeight=\"Bold\">";
            const string propertynameStart = "<Run Foreground=\"Red\">";
            const string propertyValueStart = "\"<Run Foreground=\"Blue\">";
            const string endRun = "</Run>";
            string returnValue;
            int i = 0;

            if (str.StartsWith("/"))
            {   //if the tag is an end tag, remove the "/"
                returnValue = frontWithSlash;
                str = str.Substring(1).TrimStart();
            }
            else
            {
                returnValue = front;
            }
            string[] strs = str.Split(new char[] { '>' });
            str = strs[0];
            i = (str.EndsWith("/")) ? 1 : 0;

            str = str.Substring(0, str.Length - i).Trim();

            if (str.Contains("="))//the tag has a property
            {
                //set tagName
                returnValue += tagNameStart + str.Substring(0, str.IndexOf(" ")) + endRun + " ";
                str = str.Substring(str.IndexOf(" ")).Trim();
            }
            else //no property
            {
                returnValue += tagNameStart + str.Trim() + endRun + " ";
                //nothing left to parse
                str = "";
            }

            //Take care of properties:
            while (str.Length > 0)
            {
                returnValue += propertynameStart + str.Substring(0, str.IndexOf("=")) + endRun + "=";
                str = str.Substring(str.IndexOf("\"") + 1).Trim();
                returnValue += propertyValueStart + str.Substring(0, str.IndexOf("\"")) + endRun + "\" ";
                str = str.Substring(str.IndexOf("\"") + 1).Trim();
            }

            if (returnValue.EndsWith(" "))
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            }

            returnValue += (i == 1) ? endWithSlash : end;

            //Add the content after the ">"
            returnValue += strs[1];

            return returnValue;
        }
    }
}