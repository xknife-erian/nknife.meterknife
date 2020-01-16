using System;
using System.Collections;
using System.IO;

namespace MeterKnife.Util.Wrapper.Files
{
    /// <summary>
    ///     .Net/C# 实现磁盘目录文件搜索的工具类 (搜索事件)
    /// </summary>
    public sealed class FileSearching
    {
        public delegate void SearchEventHandler(FileSearching sender, SearchEventArgs e);

        private readonly string _FileName = null;
        private CancelActions _Cancel; //取消
        private string _CurrentDestinationDirectoryName; //存储相对路径目录,可由于复制目录
        private string _CurrentDirectoryName; //搜索的当前目录名

        private string _DestinationDirectory;
        private int _DirectoriesCount; //搜索目录的次数
        private int _DirectoryId; //搜索的目录在当前目录的父目录的 ID
        private string _DirectoryPatterns = "*"; //目录名匹配模式
        private int _DirectoryUid; //本次搜索的"目录的唯一 ID"
        private int _FileId; //搜索的文件在当前目录的 ID
        private string _FilePatterns = "*"; //文件名匹配模式
        private int _FileUid; //本次搜索的"文件的唯一 ID"
        private int _FilesCount; //搜索文件的次数
        private int _Nest; //递归嵌套层数
        private string _ParentDirectoryName;
        private ArrayList _SearchedDirectories; //存储已搜索的目录
        private ArrayList _SearchedFiles; //存储已搜索的文件

        public ArrayList SearchedDirectories
        {
            get
            {
                //SearchedDirectories is ReadOnly
                return ArrayList.ReadOnly(_SearchedDirectories);
            }
        }

        public ArrayList SearchedFiles
        {
            get
            {
                //SearchedFiles is ReadOnly
                return ArrayList.ReadOnly(_SearchedFiles);
            }
        }

        public int DirectoriesCount
        {
            get { return _DirectoriesCount; }
        }

        public int FilesCount
        {
            get { return _FilesCount; }
        }

        public string DirectoriesPatterns
        {
            get { return _DirectoryPatterns; }
            set { _DirectoryPatterns = value; }
        }

        public string DestinationDirectory
        {
            get { return _DestinationDirectory; }
            set { _DestinationDirectory = value; }
        }

        public string CurrentDirectoryName
        {
            get { return _CurrentDirectoryName + (_CurrentDirectoryName.EndsWith(@"\") ? "" : @"\"); }
            set { _CurrentDirectoryName = value; }
        }

        public string FileName
        {
            get { return _FileName; }
        }

        public string ParentDirectoryName
        {
            get { return _ParentDirectoryName; }
        }

        /// <summary>
        ///     根据源目录的目录结构信息存储相对路径信息
        /// </summary>
        public string CurrentDestinationDirectoryName
        {
            get { return _CurrentDestinationDirectoryName + (_CurrentDestinationDirectoryName.EndsWith(@"\") ? "" : @"\"); }
        }

        public int FileID
        {
            get { return _FileId; }
        }

        public int DirectoryID
        {
            get { return _DirectoryId; }
        }

        public CancelActions Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }

        public int DirectoryUID
        {
            get { return _DirectoryUid; }
        }

        public int FileUID
        {
            get { return _FileUid; }
        }

        public string FilesPatterns
        {
            get { return _FilePatterns; }
            set { _FilePatterns = value; }
        }

        /// <summary>
        ///     递归FileSearching
        /// </summary>
        /// <param name="sourceDirectory">被搜索的源目录</param>
        /// <param name="directoryPatterns">源目录下面的所有子目录的搜索匹配模式</param>
        /// <param name="filePatterns">源目录下面的所有文件的搜索匹配模式</param>
        /// <param name="destinationDirectory">存储相对路径</param>
        private void Searching(string sourceDirectory, string directoryPatterns, string filePatterns, string destinationDirectory)
        {
            _DirectoryPatterns = directoryPatterns;
            _FilePatterns = filePatterns;

            string[] Patterns = _DirectoryPatterns.Split(';');
            string[] patterns = _FilePatterns.Split(';');

            _DirectoryId = 0;
            _FileId = 0;

            destinationDirectory += (destinationDirectory.EndsWith(@"\") ? "" : @"\");

            if (_DirectoriesCount == 0) //处理源目录的当前的文件
            {
                _DirectoriesCount++;
                _DirectoryId++;
                _CurrentDirectoryName = sourceDirectory;

                if (sourceDirectory.EndsWith(@"\"))
                {
                    _CurrentDirectoryName = sourceDirectory.Substring(0, sourceDirectory.Length - 1);
                }

                _ParentDirectoryName = _CurrentDirectoryName.Substring(_CurrentDirectoryName.LastIndexOf(@"\") + 1);
                destinationDirectory += _ParentDirectoryName + @"\";
                _CurrentDestinationDirectoryName = destinationDirectory;

                if (AddSearchedDirectory(_CurrentDirectoryName))
                {
                    _DirectoryUid++;
                }

                if (SearchedDirectory != null) //触发一次找到源目录的事件
                {
                    OnSearchedDirectory(sourceDirectory, _DirectoriesCount, _DirectoryId, _CurrentDestinationDirectoryName);
                }
                foreach (string p in patterns)
                {
                    foreach (string f in Directory.GetFiles(sourceDirectory, p.Trim()))
                    {
                        _FilesCount++;
                        _FileId++;

                        if (AddSearchedFile(f))
                        {
                            _FileUid++;
                        }

                        if (SearchedFile != null)
                        {
                            OnSearchedFile(f, DirectoryUID, FileUID, _DirectoryId, FileID, _CurrentDestinationDirectoryName);
                        }
                        if (_Cancel != CancelActions.No)
                        {
                            break;
                        }
                    }
                    if (_Cancel != CancelActions.No)
                    {
                        break;
                    }
                }
            }
            if (_Cancel != CancelActions.AllDirectories)
            {
                _FileId = 0;
                _DirectoryId = 0;

                foreach (string P in Patterns)
                {
                    if (_Cancel != CancelActions.AllDirectories)
                    {
                        foreach (string d in Directory.GetDirectories(sourceDirectory, P.Trim()))
                        {
                            if (_Cancel != CancelActions.AllDirectories)
                            {
                                _DirectoriesCount++;
                                _DirectoryId++;

                                _CurrentDirectoryName = d + (d.EndsWith(@"\") ? "" : @"\");
                                _CurrentDestinationDirectoryName = destinationDirectory + d.Substring(d.LastIndexOf(@"\") + 1) + @"\";

                                if (AddSearchedDirectory(_CurrentDirectoryName))
                                {
                                    _DirectoryUid++;
                                }

                                if (SearchedDirectory != null)
                                {
                                    OnSearchedDirectory(d, DirectoryUID, _DirectoryId, _CurrentDestinationDirectoryName);
                                }
                                if (_Cancel == CancelActions.CurrentDirectory)
                                {
                                    _Cancel = CancelActions.No;
                                    continue;
                                }
                                if (_Cancel == CancelActions.AllDirectories)
                                {
                                    break;
                                }
                                if (_Cancel != CancelActions.AllDirectories)
                                    foreach (string p in patterns)
                                    {
                                        foreach (string f in Directory.GetFiles(d, p.Trim()))
                                        {
                                            _FilesCount++;
                                            _FileId++;

                                            if (AddSearchedFile(f))
                                            {
                                                _FileUid++;
                                            }

                                            if (SearchedFile != null)
                                            {
                                                OnSearchedFile(f, DirectoryUID, FileUID, _DirectoryId, FileID, _CurrentDestinationDirectoryName);
                                            }
                                            if (_Cancel != CancelActions.No)
                                            {
                                                break;
                                            }
                                        }
                                        if (_Cancel != CancelActions.No)
                                        {
                                            break;
                                        }
                                    }
                                if (_Cancel == CancelActions.CurrentDirectory)
                                {
                                    _Cancel = CancelActions.No;
                                    continue;
                                }
                                if (_Cancel == CancelActions.AllDirectories)
                                {
                                    break;
                                }
                                if (_Cancel != CancelActions.AllDirectories)
                                {
                                    _Nest++;
                                    Searching(d, _DirectoryPatterns, _FilePatterns, _CurrentDestinationDirectoryName);
                                    _Nest--;
                                }
                            }
                        }
                    }
                    if (_Cancel == CancelActions.CurrentDirectory)
                    {
                        _Cancel = CancelActions.No;
                    }
                    if (_Cancel == CancelActions.AllDirectories)
                    {
                        break;
                    }
                }
            }

            if ((_Nest == 0))
            {
                if (Searched != null)
                {
                    OnSearched(DirectoryUID, FileUID, _CurrentDestinationDirectoryName);
                }
            }
        }

        public void Searching(string sourceDirectory)
        {
            Searching(sourceDirectory, "*", "*", _DestinationDirectory);
        }

        public void Searching(string sourceDirectory, string filePatterns)
        {
            Searching(sourceDirectory, "*", filePatterns, _DestinationDirectory);
        }

        public void Searching(string sourceDirectory, string directoryPatterns, string filePatterns)
        {
            Searching(sourceDirectory, directoryPatterns, filePatterns, _DestinationDirectory);
        }

        private void OnSearched(int directoryUid, int fileUid, string currentDestinationDirectoryName)
        {
            var sea = new SearchEventArgs(directoryUid, fileUid, currentDestinationDirectoryName);
            Searched(this, sea);
        }

        private void OnSearchedFile(string f, int directoryUid, int fileUid, int directoryId, int fileId, string currentDestinationDirectoryName)
        {
            var sea = new SearchEventArgs(f, directoryUid, fileUid, directoryId, fileId, currentDestinationDirectoryName);
            //new SearchEventHandler(SearchedFile).BeginInvoke(this,sea,new System.AsyncCallback(this.SearchedFileCallBack),sea);
            SearchedFile(this, sea);
        }

        private void SearchedFileCallBack(IAsyncResult iar)
        {
            throw new NotImplementedException();
        }

        private void SearchedDirectoryCallBack(IAsyncResult iar)
        {
            throw new NotImplementedException();
        }

        private void OnSearchedDirectory(string d, int directoryUid, int directoryId, string currentDestinationDirectoryName)
        {
            var sea = new SearchEventArgs(d, directoryUid, directoryId, currentDestinationDirectoryName);
            //new SearchEventHandler(SearchedDirectory).BeginInvoke(this,sea,new System.AsyncCallback(this.SearchedDirectoryCallBack),sea);
            SearchedDirectory(this, sea);
        }

        public event SearchEventHandler SearchedDirectory; //"搜索到某个目录" 的事件
        public event SearchEventHandler SearchedFile; //"搜索到某个文件" 的事件
        public event SearchEventHandler Searched; //"搜索完毕" 的事件

        private bool AddSearchedDirectory(string key)
        {
            if (_SearchedDirectories == null)
            {
                _SearchedDirectories = new ArrayList();
            }
            bool b = _SearchedDirectories.Contains(key);
            if (!b)
            {
                _SearchedDirectories.Add(key);
            }
            return !b;
        }

        private bool AddSearchedFile(string key)
        {
            if (_SearchedFiles == null)
            {
                _SearchedFiles = new ArrayList();
            }
            bool b = _SearchedFiles.Contains(key);
            if (!b)
            {
                _SearchedFiles.Add(key);
            }
            return !b;
        }
    }

    public enum CancelActions
    {
        /// <summary>
        ///     不取消,继续
        /// </summary>
        No,

        /// <summary>
        ///     只取消当前目录
        /// </summary>
        CurrentDirectory,

        /// <summary>
        ///     取消后面的所有搜索
        /// </summary>
        AllDirectories,
    }

    public class SearchEventArgs : EventArgs
    {
        private readonly string _CurrentDestinationDirectoryName;
        private readonly string _CurrentDirectoryName;
        private readonly int _DirectoryId;
        private readonly int _DirectoryUid;
        private readonly int _FileId;
        private readonly string _FileName;
        private readonly int _FileUid;
        private const int DIRECTORIES_COUNT = 0;
        private const int FILES_COUNT = 0;

        internal SearchEventArgs(int DirectoryUID, int FileUID, string CurrentDestinationDirectoryName)
        {
            _FileUid = FileUID;
            _DirectoryUid = DirectoryUID;
            _CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }

        internal SearchEventArgs(string FileName, int DirectoryUID, int FileUID, int DirectoryID, int FileID, string CurrentDestinationDirectoryName)
        {
            _FileName = Path.GetFileName(FileName);
            _CurrentDirectoryName = Path.GetDirectoryName(FileName);
            _FileUid = FileUID;
            _DirectoryUid = DirectoryUID;
            _DirectoryId = DirectoryID;
            _FileId = FileID;
            _CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }

        internal SearchEventArgs(string DirectoryName, int DirectoryUID, int DirectoryID, string CurrentDestinationDirectoryName)
        {
            _CurrentDirectoryName = DirectoryName;
            _DirectoryUid = DirectoryUID;
            _DirectoryId = DirectoryID;
            _CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }

        public int FilesCount
        {
            get { return FILES_COUNT; }
        }

        public int DirectoriesCount
        {
            get { return DIRECTORIES_COUNT; }
        }

        public string CurrentDirectoryName
        {
            get { return _CurrentDirectoryName + (_CurrentDirectoryName.EndsWith(@"\") ? "" : @"\"); }
        }

        public string FileName
        {
            get { return _FileName; }
        }

        public string ParentDirectoryName
        {
            get { return _CurrentDirectoryName.Substring(_CurrentDirectoryName.LastIndexOf(@"\") + 1); }
        }

        public string CurrentDestinationDirectoryName
        {
            get { return _CurrentDestinationDirectoryName + (_CurrentDestinationDirectoryName.EndsWith(@"\") ? "" : @"\"); }
        }

        public int FileUID
        {
            get { return _FileUid; }
        }

        public int DirectoryUID
        {
            get { return _DirectoryUid; }
        }

        public int FileID
        {
            get { return _FileId; }
        }

        public int DirectoryID
        {
            get { return _DirectoryId; }
        }
    }
}

/* 下面是针对搜索方法的测试程序
namespace Test
{
    using Microshaoft.Utils;
    public class AppTest
    {
        private static void Main()
        {
            Search x = new Search();
            AppTest a = new AppTest();
            //订阅 "搜索到某个目录" 的事件
            x.SearchedDirectory += new Search.SearchEventHandler(a.x_SearchedDirectory);
            //订阅 "搜索到某个文件" 的事件
            x.SearchedFile += new Search.SearchEventHandler(a.x_SearchedFile);
            //订阅 "搜索完毕" 的事件
            x.Searched += new Search.SearchEventHandler(a.x_Searched);

            //指定目标目录
            x.DestinationDirectory = @"E:\temp\temp1\新建文件夹";

            x.Searching(@"E:\myc#\", "*", "*.cs;*.exe");

            System.Console.WriteLine("处理了 {0} 个目录中的 {1} 个文件!", x.DirectoryUID, x.FileUID);
            System.Console.ReadLine();
        }

        private void x_SearchedDirectory(Search Sender, SearchEventArgs e)
        {
            System.Console.WriteLine("{0}:\n{1}\n{2}", Sender.DirectoryUID, Sender.CurrentDirectoryName, e.CurrentDestinationDirectoryName);
            //根据搜索到的原目录,在指定文件夹下创建同名新目录 (复制目录)
            if (!System.IO.Directory.Exists(Sender.CurrentDestinationDirectoryName))
            {
                System.IO.Directory.CreateDirectory(e.CurrentDestinationDirectoryName);
            }
            if (Sender.DirectoriesCount > 5) //该事件将被触发6次
            {
                //找到 6 个目录就不继续找了
                //Sender.Cancel = CancelActions.AllDirectories;
            }
            //Sender.Cancel = CancelActions.CurrentDirectory;
        }

        private void x_SearchedFile(Search Sender, SearchEventArgs e)
        {
            if (e.FileID == 1) //如果找到某目录下的第一个文件创建该新目录
            {
                //如果找到第一个文件创建该新目录
                //if (!System.IO.Directory.Exists(Sender.CurrentDestinationDirectoryName))
                //{
                //System.IO.Directory.CreateDirectory(e.CurrentDestinationDirectoryName);
                //}
                //System.Console.WriteLine("{0}", e.CurrentDestinationDirectoryName);
                //Sender.Cancel = CancelActions.AllDirectories;
            }

            //处理搜索到的文件
            //在该处理程序中可实现纯文本文件的全文检索关键字(如: 有非法言论可处理该文件)
            if (System.IO.Path.GetExtension(e.FileName) != ".exe")
            {
                //这里实现的是将搜索到的纯文本文件 *.cs 的文字从简体中文转换到繁体中文并另存到指定目录的功能
                FileProcess(Sender.CurrentDirectoryName + e.FileName, e.CurrentDestinationDirectoryName + e.FileName);
            }
            else
            {
                //实现 *.exe 文件复制到指定目录的功能
                System.IO.File.Copy(Sender.CurrentDirectoryName + e.FileName, e.CurrentDestinationDirectoryName + e.FileName, true);
            }


            if (Sender.FilesCount > 100) //该事件将被触发101次
            {
                //找到 101 次文件就不继续找了
                //Sender.Cancel = CancelActions.AllDirectories;
            }
            //Sender.Cancel = CancelActions.AllDirectories;
            System.Console.WriteLine("\t{0}: {1}", e.FileUID, e.FileName);
        }

        private void FileProcess(string Source, string Destination)
        {
            //这里实现的是将搜索到的纯文本文件的文字从简体中文转换到繁体中文并另存到指定目录的功能
            System.IO.StreamReader sr = new System.IO.StreamReader(Source, System.Text.Encoding.Default);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Destination, false, System.Text.Encoding.Default);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                sw.WriteLine(Microsoft.VisualBasic.Strings.StrConv(s, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, System.Globalization.CultureInfo.CurrentCulture.LCID));
            }
            sr.Close();
            sr = null;
            sw.Close();
            sw = null;
        }
        private void x_Searched(Search Sender, SearchEventArgs e)
        {
            System.Console.WriteLine("Finished 处理了 {0} 次目录, {1} 次文件!", Sender.DirectoriesCount, Sender.FilesCount);
            System.Console.WriteLine("按 \"Y\" 键列印: 已搜索的目录");
            if (System.Console.ReadLine().ToLower() == "y")
            {
                foreach (string s in Sender.SearchedDirectories)
                {
                    System.Console.WriteLine(s);
                    //Sender.SearchedDirectories is ReadOnly
                    //Sender.SearchedDirectories.Add("kkk"); //如果执行此句将跑出异常
                }
            }
        }
    }
}
*/