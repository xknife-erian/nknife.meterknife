using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.Wrapper
{
    /// <summary>
    /// 一个描述通过系统方法读写INI文件的类型
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// Gets or sets INI文件的路径
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; private set; }

        public IniFile(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.FilePath);
        }

        public void WriteValue(string section, string key, int value)
        {
            WritePrivateProfileString(section, key, value.ToString(), this.FilePath);
        }

        public string ReadValue(string section, string key, string @default)
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, @default, sb, 255, this.FilePath);

            return sb.ToString();
        }

        public int ReadValue(string section, string key, int @default)
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, @default.ToString(), sb, 255, this.FilePath);

            return int.Parse(sb.ToString());
        }

        #region DllImport

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion
    }
}
