using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NKnife.Wrapper
{
    /// <summary>
    /// 由于.net并没有提供全局快捷键的库，所以要使用该功能得通过api实现。
    /// 在Windows API中，注册和注销全局快捷键分别是通过RegisterHotKey和UnregisterHotKey函数实现。
    /// 在c#中直接使用该api显得不够简洁，这里提供了一个友好的封装。 
    /// </summary>
    public static class GlobalHotKey
    {
        /// <summary> 
        /// 注册快捷键 
        /// </summary> 
        /// <param name="hWnd">持有快捷键窗口的句柄</param> 
        /// <param name="fsModifiers">组合键</param> 
        /// <param name="vk">快捷键的虚拟键码</param> 
        /// <param name="callBack">回调函数</param> 
        public static void Regist(IntPtr hWnd, NKnife.API.API.User32.GlobalHotkeyModifiers fsModifiers, Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!NKnife.API.API.User32.RegisterHotKey(hWnd, id, fsModifiers, vk))
                throw new Exception("regist hotkey fail.");
            keymap[id] = callBack;
        }

        /// <summary> 
        /// 注销快捷键 
        /// </summary> 
        /// <param name="hWnd">持有快捷键窗口的句柄</param> 
        /// <param name="callBack">回调函数</param> 
        public static void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            {
                if (var.Value == callBack)
                    NKnife.API.API.User32.UnregisterHotKey(hWnd, var.Key);
            }
        }

        /// <summary> 
        /// 快捷键消息处理 
        /// </summary> 
        public static void ProcessHotKey(System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                HotKeyCallBackHanlder callback;
                if (keymap.TryGetValue(id, out callback))
                {
                    callback();
                }
            }
        }

        const int WM_HOTKEY = 0x312;
        static int keyid = 10;
        static Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();

        /// <summary>
        /// 热键回调函数
        /// </summary>
        public delegate void HotKeyCallBackHanlder();
    }

    /* Demo代码
     * 
     * 当程序form1启动时，注册了两个快捷键Alt+T和Ctrl+Shift+K，单击button1的时候会注销快捷键Alt+T。
     * 注：快捷键是通过消息触发的，因此要重载WndProc函数，在里面添加对快捷键回调消息的处理方法Hotkey.ProcessHotKey(m)。
        void Test()
        {
            MessageBox.Show("Test");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            GlobalHotkey.ProcessHotKey(m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalHotkey.UnRegist(this.Handle, Test);
        } 
     */
    
}
