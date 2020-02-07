using System;
using System.IO;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.Win.Forms.Forms;

namespace NKnife.MeterKnife.Workbench
{
    public class FileService : IFileService
    {
        public void ClearFile(string assemblyFileName, CleanType cleanType = CleanType.All)
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

        public enum CleanType
        {
            All,
            Dll,
            Exe
        }

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get; } = 0;
        public string Description { get; } = "文件清理服务";

        #endregion
    }
}
