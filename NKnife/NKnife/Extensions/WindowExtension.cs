using System.Windows.Threading;

namespace System.Windows
{
    public static class WindowExtension
    {
        /// <summary>
        /// 以模式方式打开一个窗口，并且在新打开的窗口关闭后返回
        /// </summary>
        /// <param name="win"></param>
        /// <param name="owner">拥有模式窗口的父级</param>
        public static bool? ShowDialog(this Window win, Window owner)
        {
            win.ShowInTaskbar = false;
            win.Topmost = true;
            win.ResizeMode = ResizeMode.NoResize;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.Owner = owner;
            return win.ShowDialog();
        }

        public static void DispatcherInvoke(this Window window, InvokeHandler handler)
        {
            window.Dispatcher.Invoke(DispatcherPriority.Normal, handler);
        }
    }

    public delegate void InvokeHandler();

}
