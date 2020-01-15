using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NKnife.FTP
{
    /// <summary>FTP处理操作类
    /// 功能：
    /// 下载文件
    /// 上传文件
    /// 上传文件的进度信息
    /// 下载文件的进度信息
    /// 删除文件
    /// 列出文件
    /// 列出目录
    /// 进入子目录
    /// 退出当前目录返回上一层目录
    /// 判断远程文件是否存在
    /// 判断远程文件是否存在
    /// 删除远程文件    
    /// 建立目录
    /// 删除目录
    /// 文件（目录）改名
    /// </summary>
    /// <remarks>
    /// NSimpler, 08-11-25 14:00:53
    /// </remarks>
    public class FtpClient
    {
        #region 属性信息

        /// <summary>
        /// 当前工作目录
        /// </summary>
        private string _DirectoryPath;

        /// <summary>
        /// 是否需要删除临时文件
        /// </summary>
        private bool _IsDeleteTempFile;

        /// <summary>
        /// FTP请求对象
        /// </summary>
        private FtpWebRequest _Request;

        /// <summary>
        /// FTP响应对象
        /// </summary>
        private FtpWebResponse _Response;

        /// <summary>
        /// 异步上传所临时生成的文件
        /// </summary>
        private string _UploadTempFile = "";

        /// <summary>
        /// FTP服务器地址
        /// </summary>
        private Uri _Uri;

        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public Uri Uri
        {
            get
            {
                if (_DirectoryPath == "/")
                {
                    return _Uri;
                }
                string strUri = _Uri.ToString();
                if (strUri.EndsWith("/"))
                {
                    strUri = strUri.Substring(0, strUri.Length - 1);
                }
                return new Uri(strUri + DirectoryPath);
            }
            set
            {
                if (value.Scheme != Uri.UriSchemeFtp)
                {
                    throw new FtpException("Ftp 地址格式错误!");
                }
                _Uri = new Uri(value.GetLeftPart(UriPartial.Authority));
                _DirectoryPath = value.AbsolutePath;
                if (!_DirectoryPath.EndsWith("/"))
                {
                    _DirectoryPath += "/";
                }
            }
        }

        /// <summary>
        /// 当前工作目录
        /// </summary>
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; }
        }

        /// <summary>
        /// FTP登录用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// FTP登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 连接FTP服务器的代理服务
        /// </summary>
        public WebProxy Proxy { get; set; }

        #endregion

        #region 事件

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        public event FtpDownloadProgressChangedDelegate DownloadProgressChanged;

        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        public event FtpDownloadDataCompletedDelegate DownloadDataCompleted;

        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        public event FtpUploadProgressChangedDelegate UploadProgressChanged;

        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        public event FtpUploadFileCompletedDelegate UploadFileCompleted;

        #endregion

        #region 构造析构函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ftpUri">FTP地址</param>
        /// <param name="strUserName">登录用户名</param>
        /// <param name="strPassword">登录密码</param>
        /// <param name="objProxy">连接代理</param>
        public FtpClient(Uri ftpUri, string strUserName, string strPassword, WebProxy objProxy = null)
        {
            var leftPart = ftpUri.GetLeftPart(UriPartial.Authority);
            _Uri = new Uri(leftPart);
            _DirectoryPath = ftpUri.AbsolutePath;
            if (!_DirectoryPath.EndsWith("/"))
            {
                _DirectoryPath += "/";
            }
            UserName = strUserName;
            Password = strPassword;
            Proxy = objProxy;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FtpClient()
        {
            UserName = "anonymous"; //匿名用户
            Password = "@anonymous";
            _Uri = null;
            Proxy = null;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~FtpClient()
        {
            if (_Response != null)
            {
                _Response.Close();
                _Response = null;
            }
            if (_Request != null)
            {
                _Request.Abort();
                _Request = null;
            }
        }

        #endregion

        #region 建立连接

        /// <summary>
        /// 建立FTP链接,返回响应对象
        /// </summary>
        /// <param name="uri">FTP地址</param>
        /// <param name="ftpMathod">操作命令</param>
        private FtpWebResponse Open(Uri uri, string ftpMathod)
        {
            try
            {
                _Request = (FtpWebRequest) WebRequest.Create(uri);
                _Request.Method = ftpMathod;
                _Request.UseBinary = true;
                _Request.Credentials = new NetworkCredential(UserName, Password);
                if (Proxy != null)
                {
                    _Request.Proxy = Proxy;
                }
                return (FtpWebResponse) _Request.GetResponse();
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 建立FTP链接,返回请求对象
        /// </summary>
        /// <param name="uri">FTP地址</param>
        /// <param name="ftpMathod">操作命令</param>
        private FtpWebRequest OpenRequest(Uri uri, string ftpMathod)
        {
            try
            {
                _Request = (FtpWebRequest) WebRequest.Create(uri);
                _Request.Method = ftpMathod;
                _Request.UseBinary = true;
                _Request.Credentials = new NetworkCredential(UserName, Password);
                if (Proxy != null)
                {
                    _Request.Proxy = Proxy;
                }
                return _Request;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 下载文件

        /// <summary>
        /// 从FTP服务器下载文件，使用与远程文件同名的文件名来保存文件
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localPath">本地路径</param>
        public bool DownloadFile(string remoteFileName, string localPath)
        {
            return DownloadFile(remoteFileName, localPath, remoteFileName);
        }

        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localPath">本地路径</param>
        /// <param name="localFileName">保存本地的文件名</param>
        public bool DownloadFile(string remoteFileName, string localPath, string localFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(localFileName) || !IsValidPathChars(localPath))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                if (!Directory.Exists(localPath))
                {
                    throw new FtpException("本地文件路径不存在!");
                }

                string localFullPath = Path.Combine(localPath, localFileName);
                if (File.Exists(localFullPath))
                {
                    throw new FtpException("当前路径下已经存在同名文件！");
                }
                byte[] bt = DownloadFile(remoteFileName);
                if (bt != null)
                {
                    var stream = new FileStream(localFullPath, FileMode.Create);
                    stream.Write(bt, 0, bt.Length);
                    stream.Flush();
                    stream.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 从FTP服务器下载文件，返回文件二进制数据
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        public byte[] DownloadFile(string remoteFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                _Response = Open(new Uri(Uri + remoteFileName), WebRequestMethods.Ftp.DownloadFile);
                Stream reader = _Response.GetResponseStream();

                var mem = new MemoryStream(1024*500);
                var buffer = new byte[1024];
                int bytesRead = 0;
                while (true)
                {
                    if (reader != null)
                        bytesRead = reader.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    mem.Write(buffer, 0, bytesRead);
                }
                if (mem.Length > 0)
                {
                    return mem.ToArray();
                }
                return null;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 异步下载文件

        /// <summary>
        /// 从FTP服务器异步下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>        
        /// <param name="localPath">保存文件的本地路径,后面带有"\"</param>
        /// <param name="localFileName">保存本地的文件名</param>
        public void DownloadFileAsync(string remoteFileName, string localPath, string localFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(localFileName) || !IsValidPathChars(localPath))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                if (!Directory.Exists(localPath))
                {
                    throw new FtpException("本地文件路径不存在!");
                }

                string localFullPath = Path.Combine(localPath, localFileName);
                if (File.Exists(localFullPath))
                {
                    throw new FtpException("当前路径下已经存在同名文件！");
                }
                DownloadFileAsync(remoteFileName, localFullPath);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 从FTP服务器异步下载文件，指定本地完整路径文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localFullPath">本地完整路径文件名</param>
        public void DownloadFileAsync(string remoteFileName, string localFullPath)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                if (File.Exists(localFullPath))
                {
                    throw new FtpException("当前路径下已经存在同名文件！");
                }
                var client = new FtpWebClient();

                client.DownloadProgressChanged += ClientDownloadProgressChanged;
                client.DownloadFileCompleted += ClientDownloadFileCompleted;
                client.Credentials = new NetworkCredential(UserName, Password);
                if (Proxy != null)
                {
                    client.Proxy = Proxy;
                }
                client.DownloadFileAsync(new Uri(Uri + remoteFileName), localFullPath);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">数据信息对象</param>
        private void ClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (DownloadDataCompleted != null)
            {
                DownloadDataCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">进度信息对象</param>
        private void ClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(sender, e);
            }
        }

        #endregion

        #region 上传文件

        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="localFullPath">本地带有完整路径的文件名</param>
        public bool UploadFile(string localFullPath)
        {
            return UploadFile(localFullPath, Path.GetFileName(localFullPath));
        }

        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="localFullPath">本地带有完整路径的文件</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public bool UploadFile(string localFullPath, bool overWriteRemoteFile)
        {
            return UploadFile(localFullPath, Path.GetFileName(localFullPath), overWriteRemoteFile);
        }

        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="localFullPath">本地带有完整路径的文件名</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public bool UploadFile(string localFullPath, string remoteFileName, bool overWriteRemoteFile = false)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(Path.GetFileName(localFullPath)) || !IsValidPathChars(Path.GetDirectoryName(localFullPath)))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                if (File.Exists(localFullPath))
                {
                    var stream = new FileStream(localFullPath, FileMode.Open, FileAccess.Read);
                    var bt = new byte[stream.Length];
                    stream.Read(bt, 0, (Int32) stream.Length); //注意，因为Int32的最大限制，最大上传文件只能是大约2G多一点
                    stream.Close();
                    return UploadFile(bt, remoteFileName, overWriteRemoteFile);
                }
                throw new FtpException("本地文件不存在!");
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="fileBytes">文件二进制内容</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public bool UploadFile(byte[] fileBytes, string remoteFileName, bool overWriteRemoteFile = false)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("非法文件名！");
                }
                if (!overWriteRemoteFile && FileExist(remoteFileName))
                {
                    throw new FtpException("FTP服务上面已经存在同名文件！");
                }
                _Response = Open(new Uri(Uri + remoteFileName), WebRequestMethods.Ftp.UploadFile);
                Stream requestStream = _Request.GetRequestStream();
                var mem = new MemoryStream(fileBytes);

                var buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = mem.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    requestStream.Write(buffer, 0, bytesRead);
                }
                requestStream.Close();
                _Response = (FtpWebResponse) _Request.GetResponse();
                mem.Close();
                mem.Dispose();
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 异步上传文件

        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="localFullPath">本地带有完整路径的文件</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(string localFullPath, bool overWriteRemoteFile = false)
        {
            UploadFileAsync(localFullPath, Path.GetFileName(localFullPath), overWriteRemoteFile);
        }

        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="localFullPath">本地带有完整路径的文件名</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(string localFullPath, string remoteFileName, bool overWriteRemoteFile = false)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(Path.GetFileName(localFullPath)) || !IsValidPathChars(Path.GetDirectoryName(localFullPath)))
                {
                    throw new FtpException("非法文件名或目录名!");
                }
                if (!overWriteRemoteFile && FileExist(remoteFileName))
                {
                    throw new FtpException("FTP服务上面已经存在同名文件！");
                }
                if (File.Exists(localFullPath))
                {
                    var client = new FtpWebClient();

                    client.UploadProgressChanged += ClientUploadProgressChanged;
                    client.UploadFileCompleted += ClientUploadFileCompleted;
                    client.Credentials = new NetworkCredential(UserName, Password);
                    if (Proxy != null)
                    {
                        client.Proxy = Proxy;
                    }
                    client.UploadFileAsync(new Uri(Uri + remoteFileName), localFullPath);
                }
                else
                {
                    throw new FtpException("本地文件不存在!");
                }
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="fileBytes">上传的二进制数据</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        public void UploadFileAsync(byte[] fileBytes, string remoteFileName)
        {
            if (!IsValidFileChars(remoteFileName))
            {
                throw new FtpException("非法文件名或目录名!");
            }
            UploadFileAsync(fileBytes, remoteFileName, false);
        }

        /// <summary>
        /// 异步上传文件到FTP服务器
        /// </summary>
        /// <param name="fileBytes">文件二进制内容</param>
        /// <param name="remoteFileName">要在FTP服务器上面保存文件名</param>
        /// <param name="overWriteRemoteFile">是否覆盖远程服务器上面同名的文件</param>
        public void UploadFileAsync(byte[] fileBytes, string remoteFileName, bool overWriteRemoteFile)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("非法文件名！");
                }
                if (!overWriteRemoteFile && FileExist(remoteFileName))
                {
                    throw new FtpException("FTP服务上面已经存在同名文件！");
                }
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                if (!tempPath.EndsWith("\\"))
                {
                    tempPath += "\\";
                }
                string tempFile = tempPath + Path.GetRandomFileName();
                tempFile = Path.ChangeExtension(tempFile, Path.GetExtension(remoteFileName));
                using (var stream = new FileStream(tempFile, FileMode.CreateNew, FileAccess.Write))
                {
                    stream.Write(fileBytes, 0, fileBytes.Length); //注意，因为Int32的最大限制，最大上传文件只能是大约2G多一点
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
                _IsDeleteTempFile = true;
                _UploadTempFile = tempFile;
                UploadFileAsync(tempFile, remoteFileName, overWriteRemoteFile);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">数据信息对象</param>
        private void ClientUploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (_IsDeleteTempFile)
            {
                if (File.Exists(_UploadTempFile))
                {
                    File.SetAttributes(_UploadTempFile, FileAttributes.Normal);
                    File.Delete(_UploadTempFile);
                }
                _IsDeleteTempFile = false;
            }
            if (UploadFileCompleted != null)
            {
                UploadFileCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        /// <param name="sender">下载对象</param>
        /// <param name="e">进度信息对象</param>
        private void ClientUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(sender, e);
            }
        }

        #endregion

        #region 列出目录文件信息

        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件和目录
        /// </summary>
        public FtpFileStruct[] ListFilesAndDirectories()
        {
            _Response = Open(Uri, WebRequestMethods.Ftp.ListDirectoryDetails);
            Stream rs = _Response.GetResponseStream();
            if (rs != null)
            {
                var stream = new StreamReader(rs, Encoding.Default);
                string datastring = stream.ReadToEnd();
                FtpFileStruct[] list = GetList(datastring);
                return list;
            }
            return null;
        }

        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件
        /// </summary>
        public FtpFileStruct[] ListFiles()
        {
            FtpFileStruct[] listAll = ListFilesAndDirectories();
            return listAll.Where(file => !file.IsDirectory).ToArray();
        }

        /// <summary>
        /// 列出FTP服务器上面当前目录的所有的目录
        /// </summary>
        public FtpFileStruct[] ListDirectories()
        {
            FtpFileStruct[] listAll = ListFilesAndDirectories();
            return listAll.Where(file => file.IsDirectory).ToArray();
        }

        /// <summary>
        /// 获得文件和目录列表
        /// </summary>
        /// <param name="datastring">FTP返回的列表字符信息</param>
        private FtpFileStruct[] GetList(string datastring)
        {
            var myListArray = new List<FtpFileStruct>();
            string[] dataRecords = datastring.Split('\n');
            FtpFileListStyle directoryListStyle = GuessFileListStyle(dataRecords);
            foreach (string s in dataRecords)
            {
                if (directoryListStyle != FtpFileListStyle.Unknown && s != "")
                {
                    var f = new FtpFileStruct {Name = ".."};
                    switch (directoryListStyle)
                    {
                        case FtpFileListStyle.UnixStyle:
                            f = ParseFileStructFromUnixStyleRecord(s);
                            break;
                        case FtpFileListStyle.WindowsStyle:
                            f = ParseFileStructFromWindowsStyleRecord(s);
                            break;
                    }
                    if (!(f.Name == "." || f.Name == ".."))
                    {
                        myListArray.Add(f);
                    }
                }
            }
            return myListArray.ToArray();
        }

        /// <summary>
        /// 从Windows格式中返回文件信息
        /// </summary>
        /// <param name="record">文件信息</param>
        private FtpFileStruct ParseFileStructFromWindowsStyleRecord(string record)
        {
            var f = new FtpFileStruct();
            string processstr = record.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
            var myDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            myDtfi.ShortTimePattern = "t";
            f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, myDtfi);
            if (processstr.Substring(0, 5) == "<DIR>")
            {
                f.IsDirectory = true;
                processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
            }
            else
            {
                string[] strs = processstr.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries); // true);
                processstr = strs[1];
                f.IsDirectory = false;
            }
            f.Name = processstr;
            return f;
        }

        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        /// <param name="recordList">文件信息列表</param>
        private FtpFileListStyle GuessFileListStyle(IEnumerable<string> recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FtpFileListStyle.UnixStyle;
                }
                if (s.Length > 8 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FtpFileListStyle.WindowsStyle;
                }
            }
            return FtpFileListStyle.Unknown;
        }

        /// <summary>
        /// 从Unix格式中返回文件信息
        /// </summary>
        /// <param name="record">文件信息</param>
        private FtpFileStruct ParseFileStructFromUnixStyleRecord(string record)
        {
            var f = new FtpFileStruct();
            string processstr = record.Trim();
            f.Flags = processstr.Substring(0, 10);
            f.IsDirectory = (f.Flags[0] == 'd');
            processstr = (processstr.Substring(11)).Trim();
            CutSubstringFromStringWithTrim(ref processstr, ' ', 0); //跳过一部分

            f.Owner = CutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            f.Group = CutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            CutSubstringFromStringWithTrim(ref processstr, ' ', 0); //跳过一部分

            string yearOrTime = processstr.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)[2];
            if (yearOrTime.IndexOf(":") >= 0) //time
            {
                processstr = processstr.Replace(yearOrTime, DateTime.Now.Year.ToString());
            }
            f.CreateTime = DateTime.Parse(CutSubstringFromStringWithTrim(ref processstr, ' ', 8));
            f.Name = processstr; //最后就是名称

            return f;
        }

        /// <summary>
        /// 按照一定的规则进行字符串截取
        /// </summary>
        /// <param name="s">截取的字符串</param>
        /// <param name="c">查找的字符</param>
        /// <param name="startIndex">查找的位置</param>
        private string CutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }

        #endregion

        #region 文件属性

        /// <summary>
        /// 获取指定文件的大小
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string filename)
        {
            long fileSize = 0;
            try
            {
                _Response = Open(new Uri(Uri + filename), WebRequestMethods.Ftp.GetFileSize);
                _Response.GetResponseStream();
                fileSize = _Response.ContentLength;
            }
            catch (Exception ex)
            {
                throw new FtpException(string.Format("获取指定文件{0}大小出现异常。", filename), ex);
            }
            return fileSize;
        }

        #endregion

        #region 目录或文件存在的判断

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="remoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string remoteDirectoryName)
        {
            try
            {
                if (!IsValidPathChars(remoteDirectoryName))
                {
                    throw new FtpException("目录名非法！");
                }
                FtpFileStruct[] listDir = ListDirectories();
                return listDir.Any(dir => dir.Name == remoteDirectoryName);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 判断一个远程文件是否存在服务器当前目录下面
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        public bool FileExist(string remoteFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("文件名非法！");
                }
                FtpFileStruct[] listFile = ListFiles();
                return listFile.Any(file => file.Name == remoteFileName);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 从FTP服务器上面删除一个文件
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        public void DeleteFile(string remoteFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName))
                {
                    throw new FtpException("文件名非法！");
                }
                _Response = Open(new Uri(Uri + remoteFileName), WebRequestMethods.Ftp.DeleteFile);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 重命名文件

        /// <summary>
        /// 更改一个文件的名称或一个目录的名称
        /// </summary>
        /// <param name="remoteFileName">原始文件或目录名称</param>
        /// <param name="newFileName">新的文件或目录的名称</param>
        public bool ReName(string remoteFileName, string newFileName)
        {
            try
            {
                if (!IsValidFileChars(remoteFileName) || !IsValidFileChars(newFileName))
                    throw new FtpException("文件名非法！");
                if (remoteFileName == newFileName)
                    return true;
                if (FileExist(remoteFileName))
                {
                    _Request = OpenRequest(new Uri(Uri + remoteFileName), WebRequestMethods.Ftp.Rename);
                    _Request.RenameTo = newFileName;
                    _Response = (FtpWebResponse) _Request.GetResponse();
                }
                else
                    throw new FtpException("文件在服务器上不存在！");
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 拷贝、移动文件

        /// <summary>
        /// 把当前目录下面的一个文件拷贝到服务器上面另外的目录中，注意，拷贝文件之后，当前工作目录还是文件原来所在的目录
        /// </summary>
        /// <param name="remoteFile">当前目录下的文件名</param>
        /// <param name="directoryName">新目录名称。
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ；
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        /// <returns></returns>
        public bool CopyFileToAnotherDirectory(string remoteFile, string directoryName)
        {
            string currentWorkDir = DirectoryPath;
            try
            {
                byte[] bt = DownloadFile(remoteFile);
                GotoDirectory(directoryName);
                bool success = UploadFile(bt, remoteFile);
                DirectoryPath = currentWorkDir;
                return success;
            }
            catch (Exception ep)
            {
                DirectoryPath = currentWorkDir;
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 把当前目录下面的一个文件移动到服务器上面另外的目录中，注意，移动文件之后，当前工作目录还是文件原来所在的目录
        /// </summary>
        /// <param name="remoteFile">当前目录下的文件名</param>
        /// <param name="directoryName">新目录名称。
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ；
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        /// <returns></returns>
        public bool MoveFileToAnotherDirectory(string remoteFile, string directoryName)
        {
            string currentWorkDir = DirectoryPath;
            try
            {
                if (directoryName == "")
                    return false;
                if (!directoryName.StartsWith("/"))
                    directoryName = "/" + directoryName;
                if (!directoryName.EndsWith("/"))
                    directoryName += "/";
                bool success = ReName(remoteFile, directoryName + remoteFile);
                DirectoryPath = currentWorkDir;
                return success;
            }
            catch (Exception ep)
            {
                DirectoryPath = currentWorkDir;
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 建立、删除子目录

        /// <summary>
        /// 在FTP服务器上当前工作目录建立一个子目录
        /// </summary>
        /// <param name="directoryName">子目录名称</param>
        public bool MakeDirectory(string directoryName)
        {
            try
            {
                if (!IsValidPathChars(directoryName))
                {
                    throw new FtpException("目录名非法！");
                }
                if (DirectoryExist(directoryName))
                {
                    throw new FtpException("服务器上面已经存在同名的文件名或目录名！");
                }
                _Response = Open(new Uri(Uri + directoryName), WebRequestMethods.Ftp.MakeDirectory);
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 从当前工作目录中删除一个子目录
        /// </summary>
        /// <param name="directoryName">子目录名称</param>
        public bool RemoveDirectory(string directoryName)
        {
            try
            {
                if (!IsValidPathChars(directoryName))
                {
                    throw new FtpException("目录名非法！");
                }
                if (!DirectoryExist(directoryName))
                {
                    throw new FtpException("服务器上面不存在指定的文件名或目录名！");
                }
                _Response = Open(new Uri(Uri + directoryName), WebRequestMethods.Ftp.RemoveDirectory);
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        #endregion

        #region 文件、目录名称有效性判断

        /// <summary>
        /// 判断目录名中字符是否合法
        /// </summary>
        /// <param name="directoryName">目录名称</param>
        public bool IsValidPathChars(string directoryName)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] dirChar = directoryName.ToCharArray();
            return dirChar.All(c => Array.BinarySearch(invalidPathChars, c) < 0);
        }

        /// <summary>
        /// 判断文件名中字符是否合法
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public bool IsValidFileChars(string fileName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            char[] nameChar = fileName.ToCharArray();
            return nameChar.All(c => Array.BinarySearch(invalidFileChars, c) < 0);
        }

        #endregion

        #region 目录切换操作

        /// <summary>
        /// 进入一个目录
        /// </summary>
        /// <param name="directoryName">
        /// 新目录的名字。 
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ； 
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </param>
        public bool GotoDirectory(string directoryName)
        {
            string currentWorkPath = DirectoryPath;
            try
            {
                directoryName = directoryName.Replace("\\", "/");
                string[] directoryNames = directoryName.Split(new[] {'/'});
                if (directoryNames[0] == ".")
                {
                    DirectoryPath = "/";
                    if (directoryNames.Length == 1)
                    {
                        return true;
                    }
                    Array.Clear(directoryNames, 0, 1);
                }
                bool success = false;
                foreach (string dir in directoryNames)
                {
                    if (dir != null)
                    {
                        success = EnterOneSubDirectory(dir);
                        if (!success)
                        {
                            DirectoryPath = currentWorkPath;
                            return false;
                        }
                    }
                }
                return success;
            }
            catch (Exception ep)
            {
                DirectoryPath = currentWorkPath;
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 从当前工作目录进入一个子目录
        /// </summary>
        /// <param name="directoryName">子目录名称</param>
        private bool EnterOneSubDirectory(string directoryName)
        {
            try
            {
                if (directoryName.IndexOf("/") >= 0 || !IsValidPathChars(directoryName))
                {
                    throw new FtpException("目录名非法!");
                }
                if (directoryName.Length > 0 && DirectoryExist(directoryName))
                {
                    if (!directoryName.EndsWith("/"))
                    {
                        directoryName += "/";
                    }
                    _DirectoryPath += directoryName;
                    return true;
                }
                return false;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw;
            }
        }

        /// <summary>
        /// 从当前工作目录往上一级目录
        /// </summary>
        public bool ComeoutDirectory()
        {
            if (_DirectoryPath == "/")
            {
                ErrorMsg = "当前目录已经是根目录！";
                throw new FtpException("当前目录已经是根目录！");
            }
            var sp = new[] {'/'};
            string[] strDir = _DirectoryPath.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            _DirectoryPath = strDir.Length == 1 ? "/" : String.Join("/", strDir, 0, strDir.Length - 1);
            return true;
        }

        #endregion

    }
}