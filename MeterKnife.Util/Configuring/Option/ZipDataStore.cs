using System;
using System.Collections.Concurrent;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using NKnife.Configuring.Common;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.OptionCase;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;
using NKnife.Wrapper.Files;

namespace NKnife.Configuring.Option
{
    /// <summary>通过ZIP打包方式实现的选项持久化。
    /// 将所有的子选项分别通过DataTable的模式保存成Xml文件，然后压缩并打包所有文件。
    /// </summary>
    public class ZipDataStore : IOptionDataStore
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        /// <summary>Gean.Configuring框架中Option部份的程序员配置
        /// </summary>
        private static readonly OptionServiceCoderSetting _Setting = OptionServiceCoderSetting.ME;

        /// <summary>多个子选项信息的集合
        /// </summary>
        /// <returns></returns>
        private ConcurrentDictionary<string, IOption> _DataTableMap;

        /// <summary>Option.info文件的XML实例
        /// </summary>
        private XmlDocument _InfoDocument;

        /// <summary>本实例是否执行了初始化
        /// </summary>
        private bool _IsInitalizeSuccess;

        /// <summary>选项持久化文件被解压后的工作目录
        /// </summary>
        private string _OptionWorkDateDirectory;

        /// <summary>选项持久化文件被解压后的工作目录的父目录，即Work目录
        /// </summary>
        private string _OptionWorkDirectory;

        /// <summary>子选项信息所在的DataTable在持久化时的后缀名
        /// </summary>
        private string _TablePostfix = string.Empty;

        /// <summary>文件压缩器
        /// </summary>
        private readonly IFileCompress _FileCompress = DI.Get<IFileCompress>();

        /// <summary>本实例总的初始化
        /// </summary>
        private void Initialize()
        {
            _TablePostfix = "." + _Setting.OptionDataTableFlagName;
            var fileInfo = (FileInfo) StoreObject;
            if (string.IsNullOrWhiteSpace(_OptionWorkDateDirectory))
                WorkDirectoryNameBuilder(fileInfo, ref _OptionWorkDirectory, ref _OptionWorkDateDirectory);
            _IsInitalizeSuccess = InitializeFiles();
            _logger.Info("初始化载入子选项个数：" + _DataTableMap.Count);
        }

        /// <summary>初始化过程中的选项文件的初始化，如压缩,解压,载入文件等
        /// </summary>
        /// <returns></returns>
        private bool InitializeFiles()
        {
            var fileInfo = (FileInfo) StoreObject;
            lock (StoreObject)
            {
                _DataTableMap = new ConcurrentDictionary<string, IOption>();
                if (File.Exists(fileInfo.FullName))
                {
                    try
                    {
                        if (Directory.Exists(_OptionWorkDirectory)) //如果目录存在，将目录先删除(包括其中的文件)
                            UtilityFile.DeleteDirectory(_OptionWorkDirectory);
                        UtilityFile.CreateDirectory(_OptionWorkDateDirectory);
                        _FileCompress.UnZipFiles(fileInfo.FullName, _OptionWorkDateDirectory); //解压文件到目录
                    }
                    catch (Exception e)
                    {
                        _logger.Error("解压选项的持久化文件异常.", e);
                        return false;
                    }

                    //激活即将开始载入选项事件
                    OnOptionLoading(new OptionLoadEventArgs(_OptionWorkDateDirectory));
                    try
                    {
                        _InfoDocument = new XmlDocument();
                        _InfoDocument.Load(Path.Combine(_OptionWorkDateDirectory, _Setting.OptionInfoFileName));
                    }
                    catch (Exception e)
                    {
                        _logger.Error("载入Option.info文件异常.", e);
                        return false;
                    }
                    try
                    {
                        string[] files = Directory.GetFiles(_OptionWorkDateDirectory, "*" + _TablePostfix);
                        foreach (string file in files)
                        {
                            IOption optionTable = UtilityOption.ParseTableNode(this, file);
                            string filename = Path.GetFileNameWithoutExtension(file);
                            if (!string.IsNullOrWhiteSpace(filename))
                                _DataTableMap.TryAdd(filename, optionTable);
                            else
                                _logger.Error(string.Format("无法从{0}中得出文件名。", filename));
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error("DataTable的反序列化过程异常.", e);
                        return false;
                    }
                    //激活选项载入后事件
                    OnOptionLoaded(new OptionLoadEventArgs(_OptionWorkDateDirectory));
                }
                else
                {
                    BuildBlankZipFile();
                    InitializeFiles(); //递归，重新载入所有文件
                }
            }
            return true;
        }

        /// <summary>返回指定的数据表对应的文件名
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        private string GetTableFileName(IOption dataTable)
        {
            string filename = string.Format("{0}{1}", dataTable.Category, _TablePostfix);
            string s = Path.Combine(_OptionWorkDateDirectory, filename);
            return s;
        }

        /// <summary>按照指定的选项持久化文件进行参照，计算选项持久化文件被解压后的工作目录名
        /// </summary>
        /// <param name="fileInfo">指定的选项持久化文件</param>
        /// <param name="parentDir">指定的选项持久化文件所在目录</param>
        /// <param name="workDir">选项持久化文件被解压后的工作目录</param>
        private static void WorkDirectoryNameBuilder(FileInfo fileInfo, ref string parentDir, ref string workDir)
        {
            string now = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), "\\");
            if (fileInfo.DirectoryName != null)
            {
                parentDir = Path.Combine(fileInfo.DirectoryName, "Work\\");
                workDir = Path.Combine(parentDir, now);
            }
        }

        /// <summary>一般是指当第一次启动应用程序时程序目录中没有相关的文件时的创建方法
        /// </summary>
        private void BuildBlankZipFile()
        {
            try
            {
                if (Directory.Exists(_OptionWorkDirectory)) //如果目录存在，将目录先删除(包括其中的文件)
                    UtilityFile.DeleteDirectory(_OptionWorkDirectory);
                UtilityFile.CreateDirectory(_OptionWorkDateDirectory);
                string infofile = Path.Combine(_OptionWorkDateDirectory, _Setting.OptionInfoFileName);
                using (var writer = new XmlTextWriter(infofile, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof (OptionCaseItem));
                    var infodoc = new XmlDocument();
                    XmlDeclaration declaration = infodoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XmlElement rootele = infodoc.CreateElement("OptionInfos");
                    infodoc.AppendChild(declaration);
                    infodoc.AppendChild(rootele);
                    XmlElement caseListElement = infodoc.CreateElement("CaseList");
                    XmlElement itemElement = infodoc.CreateElement("Item");

                    var sb = new StringBuilder();
                    var tw = new StringWriter(sb);
                    serializer.Serialize(tw, OptionCaseItem.GetBase());
                    itemElement.SetAttribute("Name", "基本设置");
                    itemElement.SetCDataElement(sb.ToString());

                    caseListElement.AppendChild(itemElement);

                    if (infodoc.DocumentElement != null)
                        infodoc.DocumentElement.AppendChild(caseListElement);
                    infodoc.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                }
                ZipSave();
                UtilityFile.DeleteDirectory(_OptionWorkDirectory); //删除选项工作目录
            }
            catch (Exception e)
            {
                _logger.Error("首次创建新的选项包时异常", e);
            }
        }

        /// <summary>保存Option.info文件
        /// </summary>
        private void OptionInfoFileSave()
        {
            _InfoDocument.Save(Path.Combine(_OptionWorkDateDirectory, _Setting.OptionInfoFileName));
        }

        /// <summary>采用压缩打包所有文件的方式保存选项文件
        /// </summary>
        /// <returns></returns>
        private string ZipSave()
        {
            string outpath = ((FileInfo) StoreObject).DirectoryName;
            if (string.IsNullOrWhiteSpace(outpath))
                throw new FileNotFoundException(outpath);
            string filename = Path.Combine(outpath, OptionServiceCoderSetting.ME.OptionFileName);
            _FileCompress.ZipFiles(Directory.GetFiles(_OptionWorkDateDirectory), filename, _OptionWorkDateDirectory);
            return filename;
        }

        #region IOptionDataStore实现

        /// <summary>是否已经初始化完成
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialize success; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitializeSuccess
        {
            get { return _IsInitalizeSuccess; }
        }

        /// <summary>更新解决方案的保存(当已存在时就更新，否则添加)
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void AddOrUpdateCaseStore(OptionCaseItem solution)
        {
            var serializer = new XmlSerializer(typeof (OptionCaseItem));
            if (_InfoDocument.DocumentElement != null)
            {
                XmlNode srcElement = _InfoDocument.DocumentElement.SelectSingleNode("CaseList");
                if (srcElement != null && srcElement.OwnerDocument != null)
                {
                    XmlElement ele = srcElement.OwnerDocument.CreateElement("Item");
                    ele.SetAttribute("Name", solution.Name);
                    var sb = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    XmlWriter xw = XmlWriter.Create(sb, settings);
                    serializer.Serialize(xw, solution);
                    ele.SetCDataElement(sb.ToString());
                    srcElement.AppendChild(ele);
                    OptionInfoFileSave();
                }
            }
        }

        /// <summary>删除一个解决方案的保存
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void RemoveCaseStore(OptionCaseItem solution)
        {
            if (_InfoDocument.DocumentElement != null)
            {
                XmlNode srcElement = _InfoDocument.DocumentElement.SelectSingleNode("CaseList");
                if (srcElement != null)
                {
                    XmlNode node = srcElement.SelectSingleNode(string.Format("./Item[@Name='{0}']", solution.Name));
                    if (node != null)
                    {
                        srcElement.RemoveChild(node);
                        OptionInfoFileSave();
                    }
                }
            }
        }

        /// <summary>应用程序选项信息存储空间
        /// </summary>
        /// <value>The store.</value>
        public object StoreObject { get; private set; }

        /// <summary>多个子选项信息的集合
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<string, IOption> DataTables
        {
            get
            {
                if (!_IsInitalizeSuccess)
                    Initialize();
                return _DataTableMap ?? (_DataTableMap = new ConcurrentDictionary<string, IOption>());
            }
        }

        /// <summary>保存一个节点的选项信息
        /// </summary>
        /// <returns></returns>
        public bool Update(IOption option)
        {
            option.Update();
            try
            {
                lock (StoreObject)
                {
                    string filefullname = GetTableFileName(option);
                    option.WriteXml(filefullname, XmlWriteMode.WriteSchema);
                    string file = ZipSave();
                    return File.Exists(file);
                }
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("更新选项信息节点时异常。{0}，{1}", option.Category, e.Message), e);
                return false;
            }
        }

        /// <summary>保存整个存储器
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                string filefullname;
                lock (StoreObject)
                {
                    foreach (IOption option in _DataTableMap.Values)
                    {
                        if (option.IsModified)
                        {
                            string s = GetTableFileName(option);
                            option.WriteXml(s, XmlWriteMode.WriteSchema);
                        }
                        option.IsModified = false;
                    }
                    filefullname = ZipSave();
                }
                return File.Exists(filefullname);
            }
            catch (Exception e)
            {
                _logger.Error("持久化选项异常", e);
                return false;
            }
        }

        public bool ReLoad()
        {
            return InitializeFiles();
        }

        public bool Clean()
        {
            if (Directory.Exists(_OptionWorkDirectory)) //如果目录存在，将目录先删除(包括其中的文件)
                UtilityFile.DeleteDirectory(_OptionWorkDirectory);
            return true;
        }

        public object Backup()
        {
            string filename = string.Format("{0}.{1}", _Setting.OptionFileName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            _FileCompress.ZipFiles(Directory.GetFiles(_OptionWorkDateDirectory), filename, _OptionWorkDateDirectory);
            return filename;
        }

        /// <summary>初始化选项的存储目标
        /// </summary>
        /// <param name="target">The target.</param>
        public void InitializeStoreTarget(object target)
        {
            var basepath = (string) target;
            string filePath = Path.Combine(basepath, OptionServiceCoderSetting.ME.OptionFileName);
            StoreObject = new FileInfo((filePath));
            if(!_IsInitalizeSuccess)
                Initialize();
        }

        /// <summary>
        /// 获得一份选项实例的管理器。
        /// 往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例。
        /// </summary>
        public IOptionCaseManager CaseManager
        {
            get { return _Setting.OptionCaseManager; }
        }

        /// <summary>选项信息即将载入前发生的事件
        /// </summary>
        public event OptionLoadingEventHandler OptionLoadingEvent;

        /// <summary>选项信息载入后发生的事件
        /// </summary>
        public event OptionLoadedEventHandler OptionLoadedEvent;

        /// <summary>当选项信息即将载入
        /// </summary>
        protected virtual void OnOptionLoading(OptionLoadEventArgs e)
        {
            if (OptionLoadingEvent != null)
                OptionLoadingEvent(this, e);
        }

        /// <summary>当选项信息载入后
        /// </summary>
        protected virtual void OnOptionLoaded(OptionLoadEventArgs e)
        {
            if (OptionLoadedEvent != null)
                OptionLoadedEvent(this, e);
        }

        #endregion
    }
}