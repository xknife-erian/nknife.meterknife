using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Util.Utility
{


    /// <summary>
    ///     本类封装了Windows消息的帮助方法
    /// </summary>
    public class UtilityWndMsg
    {
        /// <summary>
        ///     通过WM_COPYDATA发送消息，传递一个自定义的消息ID和消息字符串，
        /// </summary>
        /// <param name="fromWndHandler"></param>
        /// <param name="toWndHandler">目标窗口的句柄</param>
        /// <param name="msg"></param>
        public static void SendMessage(int fromWndHandler, int toWndHandler, string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;

            API.API.User32.CopyDataStruct cds;
            cds.dwData = (IntPtr) 100; //这里可以传入一些自定义的数据，但只能是4字节整数     
            cds.lpData = msg; //消息字符串
            cds.cbData = Encoding.Default.GetBytes(msg).Length + 1; //注意，这里的长度是按字节来算的

            API.API.User32.SendMessage(toWndHandler, (int)API.API.User32.WMsg.WM_COPYDATA, fromWndHandler, ref cds);
        }

        public static void SendMessage(int toWndHandler, string msg)
        {
            //发送方的窗口的句柄, 由于本系统中的接收方不关心是该消息是从哪个窗口发出的，所以就直接填0了
            SendMessage(0,toWndHandler,msg);
        }

        public static void SendMessage(string toWndName, string msg)
        {
            int toWndHandler = GetWndHandleByWindowName(toWndName);
            if (toWndHandler < 0)
                return;
            SendMessage(toWndHandler,msg);
        }

        /// 接收消息，得到消息字符串
        /// System.Windows.Forms.Message m
        /// 接收到的消息字符串
        public static string ReceiveMessage(ref Message m)
        {
            var cds = (API.API.User32.CopyDataStruct) m.GetLParam(typeof (API.API.User32.CopyDataStruct));
            return cds.lpData;
        }

        /// <summary>
        /// 根据窗口名称找窗口句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetWndHandleByWindowName(string name)
        {
            return API.API.User32.FindWindow(null, name);
        }

        /// <summary>
        /// 根据进程名称找窗口句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetWndHandleByProcessName(string name)
        {
            Process[] foundProcess = Process.GetProcessesByName(name);
            foreach (Process p in foundProcess)
            {
                return p.MainWindowHandle.ToInt32(); //返回找到的第一个
            }
            return -1; //没找到
        }

    }
}