using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.DemoApplication;

namespace DemoApplication
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

            var list = new List<byte[]>();
            list.Add(new byte[] { 0x01 });
            list.Add(new byte[] { 0x02,0x03 });

            Console.WriteLine(list.Contains(new byte[] { 0x01 }));
            Console.WriteLine(list.Contains(new byte[] { 0x02, 0x03 }));

            Application.Run(new Form1());
        }
    }
}
