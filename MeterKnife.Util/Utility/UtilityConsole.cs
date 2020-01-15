using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.Utility
{
    public class UtilityConsole
    {
        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        /// <summary>
        /// 释放控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        /// <summary>
        /// 在一个winform程序里启动一个控制台窗口，可以用于输出日志（只能用于输出，无法用于输入）
        /// </summary>
        public static void OpenConsole()
        {
            AllocConsole();
            var standardOutput = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.Default)
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
        }

        /// <summary>
        /// 和上面的OpenConsole对应，关闭并释放启动的控制台窗口
        /// </summary>
        public static void CloseConsole()
        {
            FreeConsole();
        }
    }
}
