using System;
using System.IO;
using NKnife.Util;

namespace NKnife.MeterKnife.Common
{
    public class PathManager
    {
        /// <summary>
        /// 本软件“使用习惯记录文件”与“软件选项数据文件”的子路径
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string NKnife_MeterKnife_ANT = "NKnife|MeterKnife|Ant|";

        /// <summary>
        /// 用户应用程序数据路径。“使用习惯记录文件”与“软件选项数据文件”将会被保存在这里。eg. C:\Users\xxx\AppData\Roaming
        /// </summary>
        public string UserApplicationDataPath
        {
            get
            {
                var sub = NKnife_MeterKnife_ANT.Replace('|', Path.DirectorySeparatorChar);
                string userAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), sub);
                if (!Directory.Exists(userAppData))
                    UtilFile.CreateDirectory(userAppData);
                return userAppData;
            }
        }

        /// <summary>
        /// 用户操作系统的文档目录路径。eg. C:\Users\xxx\Documents
        /// </summary>
        public string UserDocumentsPath
        {
            get
            {
                var sub = NKnife_MeterKnife_ANT.Replace('|', '-').TrimEnd('-');
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), sub);
                if (!Directory.Exists(path))
                    UtilFile.CreateDirectory(path);
                return path;
            }
        }
    }
}
