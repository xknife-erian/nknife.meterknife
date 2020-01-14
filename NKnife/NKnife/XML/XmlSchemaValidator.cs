using System;
using System.Xml;
using System.Xml.Schema;

namespace NKnife.XML
{
    /// <summary>
    /// This class validates an xml string or xml document against an xml schema.
    /// It has public methods that return a boolean value depending on the validation
    /// of the xml.
    /// </summary>
    public class XmlSchemaValidator
    {
        private static string _strErrorMsg;
        /// <summary>
        /// 根据已有的Schema对XmlDocument进行检测
        /// </summary>
        /// <param name="schemaFile">Schema文件路径</param>
        /// <param name="xmlFile">需校验的XmlDocument的路径</param>
        /// <returns></returns>
        public static bool CheckArchXMLContent(string schemaFile, string xmlFile, out string strErrorMsg)
        {

            bool isResult = true;
            strErrorMsg = "";
            try
            {
                _strErrorMsg = "";

                XmlSchemaSet xssArchContent = new XmlSchemaSet();
                xssArchContent.Add("", schemaFile);// 添加架构文件,前面是命名空间，没有为空

                XmlReaderSettings xrsArchContent = new XmlReaderSettings();
                xrsArchContent.ValidationType = ValidationType.Schema;
                xrsArchContent.Schemas = xssArchContent;
                xrsArchContent.ValidationEventHandler += new ValidationEventHandler(DealError);

                XmlReader reader = XmlReader.Create(xmlFile, xrsArchContent);

                while (reader.Read()) { }

                if (_strErrorMsg != "")
                {
                    isResult = false;
                    strErrorMsg = _strErrorMsg;
                }
            }
            catch (Exception e)
            {
                strErrorMsg = e.Message.ToString();
                isResult = false;
            }
            return isResult;
        }

        /// <summary>
        /// 错误处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void DealError(object sender, ValidationEventArgs args)
        {
            _strErrorMsg += args.Message.ToString() + "\r\n\r\n";
        }

    }
}