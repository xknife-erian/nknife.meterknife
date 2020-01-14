using System.Windows.Forms;

namespace System
{
    public static class ControlExtension
    {
        public static bool IsEmptyText(this Control control)
        {
            return string.IsNullOrWhiteSpace(control.Text);
        }

        public delegate void InvokeHandler();

        /// <summary>非本线程安全访问控件
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="async"> </param>
        public static void ThreadSafeInvoke(this Control control, InvokeHandler handler, bool async)
        {
            if (control.InvokeRequired)
            {
                if (async)
                {
                    control.BeginInvoke(handler);
                }
                else
                {
                    control.Invoke(handler);
                }
            }
            else
            {
                handler();
            }
        }

        public static void ThreadSafeInvoke(this Control control, InvokeHandler handler)
        {
            ThreadSafeInvoke(control, handler, false);
        }

        /// <summary>非本线程安全访问控件
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="async"> </param>
        public static void ThreadSafeInvoke(this UserControl control, InvokeHandler handler, bool async)
        {
            if (control.InvokeRequired)
            {
                if (async)
                {
                    control.BeginInvoke(handler);
                }
                else
                {
                    control.Invoke(handler);
                }
            }
            else
            {
                handler();
            }
        }

        public static void ThreadSafeInvoke(this UserControl control, InvokeHandler handler)
        {
            ThreadSafeInvoke(control, handler, false);
        }

        /// <summary>非本线程安全访问控件
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="invoker"> </param>
        /// <param name="handler">The handler.</param>
        /// <param name="async"> </param>
        public static void ThreadSafeInvoke(this ToolStripStatusLabel control, Control invoker, InvokeHandler handler, bool async)
        {
            if (invoker.InvokeRequired)
            {
                if (async)
                {
                    invoker.BeginInvoke(handler);
                }
                else
                {
                    invoker.Invoke(handler);
                }
            }
            else
            {
                handler();
            }
        }

        public static void ThreadSafeInvoke(this ToolStripStatusLabel control, Control invoker, InvokeHandler handler)
        {
            ThreadSafeInvoke(control, invoker, handler, false);
        }
    }
}
