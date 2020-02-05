﻿using System;
using System.IO;
using System.Windows.Forms;
using NKnife.Win.Forms.Forms;

namespace NKnife.MeterKnife.App
{
    class FileCleaner
    {
        public static void Run()
        {
        }

        private static void ClearSingleFile(string assemblyFileName, CleanType cleanType = CleanType.All)
        {
            string file = assemblyFileName;
            try
            {
                if (cleanType == CleanType.All || cleanType == CleanType.Dll)
                {
                    file = Path.Combine(Application.StartupPath, assemblyFileName + ".dll");
                    if (File.Exists(file))
                        File.Delete(file);
                }
                if (cleanType == CleanType.All || cleanType == CleanType.Exe)
                {
                    file = Path.Combine(Application.StartupPath, assemblyFileName + ".exe");
                    if (File.Exists(file))
                        File.Delete(file);
                }
            }
            catch (Exception)
            {
                Splasher.Status = ("删除过时程序文件异常:" + file);
            }
        }

        enum CleanType
        {
            All,
            Dll,
            Exe
        }
    }
}
