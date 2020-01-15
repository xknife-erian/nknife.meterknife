using System.Diagnostics;
using System.IO;
using System.Xml;

namespace NKnife.XML
{
    /// <summary>
    /// 对XmlDocument的类的封装
    /// </summary>
    public abstract class AbstractXmlDocument : AbstractBaseXmlNode
    {
        #region 构造函数

        protected AbstractXmlDocument()
        {
        }

        /// <summary>
        /// 基础的XmlDocument扩展(组合)类
        /// </summary>
        /// <param name="filePath">XML文件的物理绝对路径</param>
        protected AbstractXmlDocument(string filePath)
        {
            FilePath = filePath;
            CheckFile();
        }

        protected void CheckFile()
        {
            if (!File.Exists(FilePath))
            {
                //如果文件不存在，建立这个文件
                BaseXmlNode = XmlHelper.CreatNewDoucmnet(FilePath, RootNodeLocalName);
            }
            else
            {
                BaseXmlNode = new XmlDocument();
                (BaseXmlNode as XmlDocument).Load(FilePath);
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement。
        /// </summary>
        public XmlElement DocumentElement
        {
            get { return ((XmlDocument) BaseXmlNode).DocumentElement; }
        }

        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement 的本地名。
        /// </summary>
        public virtual string RootNodeLocalName
        {
            get { return "Root"; }
        }

        /// <summary>
        /// 获取文档的根 System.Xml.XmlElement。
        /// </summary>
        public XmlNodeList ChildNodes
        {
            get { return BaseXmlNode.ChildNodes; }
        }

        /// <summary>
        /// 获取文档的绝对路径
        /// </summary>
        public string FilePath { get; protected set; }

        #endregion

        #region 方法

        public XmlElement NewElement(string localName)
        {
            return ((XmlDocument) BaseXmlNode).CreateElement(localName);
        }

        /// <summary>
        /// 保存当前XmlDocument
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                Debug.Fail("this.FilePath is Null!");
                return;
            }
            FileAttributes fileAtts = FileAttributes.Normal;
            if (File.Exists(FilePath))
            {
                fileAtts = File.GetAttributes(FilePath); //先获取此文件的属性
                File.SetAttributes(FilePath, FileAttributes.Normal); //将文件属性设置为普通（即没有只读和隐藏等）
            }
            ((XmlDocument) BaseXmlNode).Save(FilePath); //在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            File.SetAttributes(FilePath, fileAtts); //恢复文件属性
        }

        #endregion
    }
}