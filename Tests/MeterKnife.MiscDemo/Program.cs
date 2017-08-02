using System;
using System.Windows.Forms;
using MeterKnife.UITests;
using NKnife.IoC;

namespace MeterKnife.MiscDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.Initialize();
            Application.Run(new MainForm());
        }
    }
}
