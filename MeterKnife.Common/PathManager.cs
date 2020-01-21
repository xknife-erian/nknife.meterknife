using System;
using System.IO;
using NKnife.Util;

namespace NKnife.MeterKnife.Common
{
    public class PathManager
    {
        /// <summary>
        /// 程序员配置文件路径
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string NKnife_MeterKnife_ANT = "NKnife\\MeterKnife\\Ant\\";

        /// <summary>
        /// 用户路径。eg. C:\Users\xxx\AppData\Roaming
        /// </summary>
        public string UserApplicationDataPath
        {
            get
            {
                string userAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), NKnife_MeterKnife_ANT);
                if (!Directory.Exists(userAppData))
                    UtilFile.CreateDirectory(userAppData);
                return userAppData;
            }
        }
    }
}
