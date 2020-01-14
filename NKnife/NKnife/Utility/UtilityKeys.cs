using System;
using System.Diagnostics;

namespace NKnife.Utility
{
    /// <summary>
    /// 相关键盘的键(Key)封装的类
    /// </summary>
    public static class UtilityKeys
    {
        /// <summary>
        /// 解析一个描述键值的字符串为键，例如: ctrl+shift+a
        /// </summary>
        /// <param name="shortcutChar">描述键值的字符串,例如: ctrl+shift+a</param>
        /// <returns>Windows的键</returns>
        public static System.Windows.Forms.Keys ParseByShortcutChar(string shortcutChar)
        {
            if (string.IsNullOrEmpty(shortcutChar))
            {
                return System.Windows.Forms.Keys.None;
            }

            var key = System.Windows.Forms.Keys.None;
            string[] strs = shortcutChar.Split('+');
            foreach (string str in strs)
            {
                string output;

                //用Switch处理一部份可能会简写的键
                switch (str.ToLowerInvariant())
                {
                    case "ctrl":
                        output = "Control";
                        break;
                    case "esc":
                        output = "Escape";
                        break;
                    case "del":
                        output = "Delete";
                        break;
                    default:
                        string firstchar = str.Substring(0, 1).ToUpperInvariant();
                        output = firstchar + str.ToLowerInvariant().Substring(1);
                        break;
                }
                try
                {
                    key |= (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), output);
                }
                catch (Exception ex)//如解析失败，将返回的是未按键
                {
                    Debug.Fail(ex.Message);
                }
            }
            return key;
        }

    }
}
