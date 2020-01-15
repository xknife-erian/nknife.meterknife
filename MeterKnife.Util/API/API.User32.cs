using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NKnife.API
{
    public sealed partial class API
    {
        /// <summary>
        /// 面向C#使用API的封装:对User32.dll的封装
        /// </summary>
        public class User32
        {
            #region 常量

            static readonly IntPtr FALSE = new IntPtr(0);
            static readonly IntPtr TRUE = new IntPtr(1);

            #endregion

            #region 面向C#使用API的封装

            /// <summary>
            /// 是否是指定的键的按下
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns>
            /// 	<c>true</c> if [is key pressed] [the specified key]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsKeyPressed(Keys key)
            {
                return GetKeyState((int)key) < 0;
            }

            /// <summary>
            /// 从一个文件中获取一个鼠标指针
            /// </summary>
            /// <param name="fileName">含有鼠标指针的一个文件</param>
            /// <returns></returns>
            public static Cursor LoadCursor(string fileName)
            {
                return new Cursor(LoadCursorFromFile(fileName));
            }

            /// <summary>
            /// 通过API设置窗体的阴影
            /// </summary>
            /// <param name="window">The window.</param>
            public static void SetWindowShadow(Form window)
            {
                SetClassLong(window.Handle, (int)WindowShadowParm.GCL_STYLE, GetClassLong(window.Handle, (int)WindowShadowParm.GCL_STYLE) | (int)WindowShadowParm.CS_DROPSHADOW);
            }

            /// <summary>
            /// 当无标题栏的Form无法使用窗体移动，调用APT来实现
            /// </summary>
            /// <param name="form">The form.</param>
            public static void SetFormMoveing(Form form)
            {
                ReleaseCapture();
                API.User32.SendMessage(form.Handle, (int)WMsg.WM_SYSCOMMAND, (int)WMsg.SC_MOVE + (int)WMsg.HTCAPTION, 0);
            }

            /// <summary>
            /// API激活控件的是否重绘（刷新）
            /// </summary>
            /// <param name="control">The h WND.</param>
            /// <param name="allowRedraw">if set to <c>true</c> [allow redraw].</param>
            public static void SetWindowRedraw(Control control, bool allowRedraw)
            {
                SendMessage(control.Handle, (int)WMsg.WM_SETREDRAW, (allowRedraw ? TRUE : FALSE).ToInt32(), IntPtr.Zero.ToInt32());
            }

            #region AnimateWindow 窗口动画

            /// <summary>
            /// 实现窗体的淡入
            /// </summary>
            /// <param name="control">The form.</param>
            /// <param name="time">淡入窗体的时间,毫秒.</param>
            /// <returns></returns>
            public static bool SetBlendWindowTo(Control control, int time)
            {
                return AnimateWindow(control.Handle.ToInt32(), time, (int)AnimateParm.AW_BLEND | (int)AnimateParm.AW_ACTIVATE);
            }

            /// <summary>
            /// 实现窗体的淡出,一般应在FormClosing中添加代码
            /// </summary>
            /// <param name="control">The form.</param>
            /// <param name="time">The time.</param>
            /// <returns></returns>
            public static bool SetBlendWindowHide(Control control, int time)
            {
                return AnimateWindow(control.Handle.ToInt32(), time, (int)AnimateParm.AW_HOR_NEGATIVE | (int)AnimateParm.AW_HIDE);
            }

            /// <summary>
            /// API设置控件滑动显示
            /// </summary>
            /// <param name="contorl">The contorl.</param>
            /// <param name="time">The time.</param>
            /// <param name="slide">The slide.</param>
            /// <returns></returns>
            public static bool SetSlidingWindow(Control contorl, int time, Slide slide)
            {
                return AnimateWindow(contorl.Handle.ToInt32(), time, (int)slide);
            }

            #endregion

            /// <summary>
            /// 让树控件向上滚动
            /// </summary>
            static public void ScrollTreeViewLineUp(TreeView treeView)
            {
                SendMessage(treeView.Handle, (int)ScrollTreeView.WM_VSCROLL, (int)ScrollTreeView.SB_LINEUP, 0);
            }
            /// <summary>
            /// 让树控件向下滚动
            /// </summary>
            static public void ScrollTreeViewLineDown(TreeView treeView)
            {
                SendMessage(treeView.Handle, (int)ScrollTreeView.WM_VSCROLL, (int)ScrollTreeView.SB_LINEDOWN, 0);
            }

            /// <summary>
            /// 面向C#使用API的封装:Sets the window hide.
            /// </summary>
            /// <param name="form">The form.</param>
            public static void SetWindowHide(Form form)
            {
                SetWindowPos(form.Handle, (IntPtr)0, 0, 0, 0, 0, (int)SetWindow.SWP_NOSIZE | (int)SetWindow.SWP_NOMOVE | (int)SetWindow.SWP_HIDEWINDOW | (int)SetWindow.SWP_NOSENDCHANGING);
            }
            /// <summary>
            /// 面向C#使用API的封装:Sets the window show.
            /// </summary>
            /// <param name="form">The form.</param>
            /// <param name="formAfter">The form after.</param>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            public static void SetWindowShow(Form form, Form formAfter, int x, int y, int width, int height)
            {
                SetWindowPos(form.Handle, (formAfter == null ? (IntPtr)0 : formAfter.Handle), x, y, width, height, (int)SetWindow.SWP_SHOWWINDOW | (int)SetWindow.SWP_NOACTIVATE);
            }

            #endregion

            #region int -> enum

            enum WindowShadowParm : int
            {
                CS_DROPSHADOW = 0x20000,
                GCW_ATOM = -32,

                GCL_CBCLSEXTRA = -20,
                GCL_CBWNDEXTRA = -18,
                GCL_HBRBACKGROUND = -10,
                GCL_HCURSOR = -12,
                GCL_HICON = -14,
                GCL_HMODULE = -16,
                GCL_MENUNAME = -8,
                GCL_WNDPROC = -24,
                GCL_STYLE = (-26),
            }

            public enum GlobalHotkeyModifiers
            {
                MOD_ALT = 0x1,
                MOD_CONTROL = 0x2,
                MOD_SHIFT = 0x4,
                MOD_WIN = 0x8
            }

            enum SetWindow : int
            {
                SWP_NOSIZE = 0x0001,
                SWP_NOMOVE = 0x0002,
                SWP_NOZORDER = 0x0004,
                SWP_NOREDRAW = 0x0008,
                SWP_NOACTIVATE = 0x0010,
                SWP_FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
                SWP_SHOWWINDOW = 0x0040,
                SWP_HIDEWINDOW = 0x0080,
                SWP_NOCOPYBITS = 0x0100,
                SWP_NOOWNERZORDER = 0x0200,  /* Don't do owner Z ordering */
                SWP_NOSENDCHANGING = 0x0400,  /* Don't send WM_WINDOWPOSCHANGING */
            }

            /// <summary>
            /// Show Window Command
            /// </summary>
            public enum ShowWindowCommand : int
            {
                /// <summary>
                /// Hides the window and activates another window.
                /// </summary>
                Hide = 0,
                /// <summary>
                /// Activates and displays a window. If the window is minimized or
                /// maximized, the system restores it to its original size and position.
                /// An application should specify this flag when displaying the window
                /// for the first time.
                /// </summary>
                Normal = 1,
                /// <summary>
                /// Activates the window and displays it as a minimized window.
                /// </summary>
                ShowMinimized = 2,
                /// <summary>
                /// Maximizes the specified window.
                /// </summary>
                Maximize = 3, // is this the right value?
                /// <summary>
                /// Activates the window and displays it as a maximized window.
                /// </summary>      
                ShowMaximized = 3,
                /// <summary>
                /// Displays a window in its most recent size and position. This value
                /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
                /// the window is not actived.
                /// </summary>
                ShowNoActivate = 4,
                /// <summary>
                /// Activates the window and displays it in its current size and position.
                /// </summary>
                Show = 5,
                /// <summary>
                /// Minimizes the specified window and activates the next top-level
                /// window in the Z order.
                /// </summary>
                Minimize = 6,
                /// <summary>
                /// Displays the window as a minimized window. This value is similar to
                /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
                /// window is not activated.
                /// </summary>
                ShowMinNoActive = 7,
                /// <summary>
                /// Displays the window in its current size and position. This value is
                /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
                /// window is not activated.
                /// </summary>
                ShowNA = 8,
                /// <summary>
                /// Activates and displays the window. If the window is minimized or
                /// maximized, the system restores it to its original size and position.
                /// An application should specify this flag when restoring a minimized window.
                /// </summary>
                Restore = 9,
                /// <summary>
                /// Sets the show state based on the SW_* value specified in the
                /// STARTUPINFO structure passed to the CreateProcess function by the
                /// program that started the application.
                /// </summary>
                ShowDefault = 10,
                /// <summary>
                ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
                /// that owns the window is not responding. This flag should only be
                /// used when minimizing windows from a different thread.
                /// </summary>
                ForceMinimize = 11
            }

            enum ScrollTreeView : int
            {
                WM_VSCROLL = 0x0115,
                SB_LINEUP = 0,
                SB_LINELEFT = 0,
                SB_LINEDOWN = 1,
                SB_LINERIGHT = 1,
                SB_PAGEUP = 2,
                SB_PAGELEFT = 2,
                SB_PAGEDOWN = 3,
                SB_PAGERIGHT = 3,
                SB_THUMBPOSITION = 4,
                SB_THUMBTRACK = 5,
                SB_TOP = 6,
                SB_LEFT = 6,
                SB_BOTTOM = 7,
                SB_RIGHT = 7,
                SB_ENDSCROLL = 8,
            }

            /// <summary>
            /// SendMessage中wMsg参数常量值
            /// </summary>
            public enum WMsg : int
            {
                /// <summary>
                /// WM_KEYDOWN 按下一个键
                /// </summary>
                WM_KEYDOWN = 0x0100,
                /// <summary>释放一个键</summary>
                WM_KEYUP = 0x0101,
                /// <summary>按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息</summary>
                WM_CHAR = 0x102,
                /// <summary>当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口</summary>
                WM_DEADCHAR = 0x103,
                /// <summary>当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口</summary>
                WM_SYSKEYDOWN = 0x104,
                /// <summary>当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口</summary>
                WM_SYSKEYUP = 0x105,
                /// <summary>当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口</summary>
                WM_SYSCHAR = 0x106,
                /// <summary>当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口</summary>
                WM_SYSDEADCHAR = 0x107,
                /// <summary>在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务</summary>
                WM_INITDIALOG = 0x110,
                /// <summary>当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译</summary>
                WM_COMMAND = 0x111,
                /// <summary>当用户选择窗口菜单的一条命令，或当用户选择最大化或最小化时那个窗口会收到此消息</summary>
                WM_SYSCOMMAND = 0x0112,
                /// <summary>发生了定时器事件</summary>
                WM_TIMER = 0x113,
                /// <summary>当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件</summary>
                WM_HSCROLL = 0x114,
                /// <summary>当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件</summary>
                WM_VSCROLL = 0x115,
                /// <summary>当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单</summary>
                WM_INITMENU = 0x116,
                /// <summary>当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部</summary>
                WM_INITMENUPOPUP = 0x117,
                /// <summary>当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）</summary>
                WM_MENUSELECT = 0x11F,
                /// <summary>当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者</summary>
                WM_MENUCHAR = 0x120,
                /// <summary>当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待</summary>
                WM_ENTERIDLE = 0x121,
                /// <summary>在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色</summary>
                WM_CTLCOLORMSGBOX = 0x132,
                /// <summary>当一个编辑型控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色</summary>
                WM_CTLCOLOREDIT = 0x133,
                /// <summary>当一个列表框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色</summary>
                WM_CTLCOLORLISTBOX = 0x134,
                /// <summary>当一个按钮控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色</summary>
                WM_CTLCOLORBTN = 0x135,
                /// <summary>当一个对话框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色</summary>
                WM_CTLCOLORDLG = 0x136,
                /// <summary>当一个滚动条控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色</summary>
                WM_CTLCOLORSCROLLBAR = 0x137,
                /// <summary>当一个静态控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以 通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色</summary>
                WM_CTLCOLORSTATIC = 0x138,
                /// <summary>当鼠标轮子转动时发送此消息个当前有焦点的控件</summary>
                WM_MOUSEWHEEL = 0x20A,
                /// <summary>双击鼠标中键</summary>
                WM_MBUTTONDBLCLK = 0x209,
                /// <summary>释放鼠标中键</summary>
                WM_MBUTTONUP = 0x208,
                /// <summary>移动鼠标时发生，同WM_MOUSEFIRST</summary>
                WM_MOUSEMOVE = 0x200,
                /// <summary>按下鼠标左键</summary>
                WM_LBUTTONDOWN = 0x201,
                /// <summary>释放鼠标左键</summary>
                WM_LBUTTONUP = 0x202,
                /// <summary>双击鼠标左键</summary>
                WM_LBUTTONDBLCLK = 0x203,
                /// <summary>按下鼠标右键</summary>
                WM_RBUTTONDOWN = 0x204,
                /// <summary>释放鼠标右键</summary>
                WM_RBUTTONUP = 0x205,
                /// <summary>双击鼠标右键</summary>
                WM_RBUTTONDBLCLK = 0x206,
                /// <summary>按下鼠标中键</summary>
                WM_MBUTTONDOWN = 0x207,
                /// <summary>创建一个窗口</summary>
                WM_CREATE = 0x01,
                /// <summary>当一个窗口被破坏时发送</summary>
                WM_DESTROY = 0x02,
                /// <summary>移动一个窗口</summary>
                WM_MOVE = 0x03,
                /// <summary>改变一个窗口的大小</summary>
                WM_SIZE = 0x05,
                /// <summary>一个窗口被激活或失去激活状态</summary>
                WM_ACTIVATE = 0x06,
                /// <summary>一个窗口获得焦点</summary>
                WM_SETFOCUS = 0x07,
                /// <summary>一个窗口失去焦点</summary>
                WM_KILLFOCUS = 0x08,
                /// <summary>一个窗口改变成Enable状态</summary>
                WM_ENABLE = 0x0A,
                /// <summary>设置窗口是否能重画</summary>
                WM_SETREDRAW = 0x0B,
                /// <summary>应用程序发送此消息来设置一个窗口的文本</summary>
                WM_SETTEXT = 0x0C,
                /// <summary>应用程序发送此消息来复制对应窗口的文本到缓冲区</summary>
                WM_GETTEXT = 0x0D,
                /// <summary>得到与一个窗口有关的文本的长度（不包含空字符）</summary>
                WM_GETTEXTLENGTH = 0x0E,
                /// <summary>要求一个窗口重画自己</summary>
                WM_PAINT = 0x0F,
                /// <summary>当一个窗口或应用程序要关闭时发送一个信号</summary>
                WM_CLOSE = 0x10,
                /// <summary>当用户选择结束对话框或程序自己调用ExitWindows函数</summary>
                WM_QUERYENDSESSION = 0x11,
                /// <summary>用来结束程序运行</summary>
                WM_QUIT = 0x12,
                /// <summary>当用户窗口恢复以前的大小位置时，把此消息发送给某个图标</summary>
                WM_QUERYOPEN = 0x13,
                /// <summary>当窗口背景必须被擦除时（例在窗口改变大小时）</summary>
                WM_ERASEBKGND = 0x14,
                /// <summary>当系统颜色改变时，发送此消息给所有顶级窗口</summary>
                WM_SYSCOLORCHANGE = 0x15,
                /// <summary>当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束</summary>
                WM_ENDSESSION = 0x16,
                /// <summary>当隐藏或显示窗口是发送此消息给这个窗口</summary>
                WM_SHOWWINDOW = 0x18,
                /// <summary>发此消息给应用程序哪个窗口是激活的，哪个是非激活的</summary>
                WM_ACTIVATEAPP = 0x1C,
                /// <summary>当系统的字体资源库变化时发送此消息给所有顶级窗口</summary>
                WM_FONTCHANGE = 0x1D,
                /// <summary>当系统的时间变化时发送此消息给所有顶级窗口</summary>
                WM_TIMECHANGE = 0x1E,
                /// <summary>发送此消息来取消某种正在进行的摸态（操作）</summary>
                WM_CANCELMODE = 0x1F,
                /// <summary>如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口</summary>
                WM_SETCURSOR = 0x20,
                /// <summary>当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口</summary>
                WM_MOUSEACTIVATE = 0x21,
                /// <summary>发送此消息给MDI子窗口,当用户点击此窗口的标题栏，或当窗口被激活，移动，改变大小</summary>
                WM_CHILDACTIVATE = 0x22,
                /// <summary>此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息</summary>
                WM_QUEUESYNC = 0x23,
                /// <summary>此消息发送给窗口当它将要改变大小或位置</summary>
                WM_GETMINMAXINFO = 0x24,
                /// <summary>发送给最小化窗口当它图标将要被重画</summary>
                WM_PAINTICON = 0x26,
                /// <summary>此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画</summary>
                WM_ICONERASEBKGND = 0x27,
                /// <summary>发送此消息给一个对话框程序去更改焦点位置</summary>
                WM_NEXTDLGCTL = 0x28,
                /// <summary>每当打印管理列队增加或减少一条作业时发出此消息</summary>
                WM_SPOOLERSTATUS = 0x2A,
                /// <summary>当button，combobox，listbox，menu的可视外观改变时发送</summary>
                WM_DRAWITEM = 0x2B,
                /// <summary>当button, combo box, list box, list view control, or menu item 被创建时</summary>
                WM_MEASUREITEM = 0x2C,
                /// <summary>此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息</summary>
                WM_VKEYTOITEM = 0x2E,
                /// <summary>此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息</summary>
                WM_CHARTOITEM = 0x2F,
                /// <summary>当绘制文本时程序发送此消息得到控件要用的颜色</summary>
                WM_SETFONT = 0x30,
                /// <summary>应用程序发送此消息得到当前控件绘制文本的字体</summary>
                WM_GETFONT = 0x31,
                /// <summary>应用程序发送此消息让一个窗口与一个热键相关连</summary>
                WM_SETHOTKEY = 0x32,
                /// <summary>应用程序发送此消息来判断热键与某个窗口是否有关联</summary>
                WM_GETHOTKEY = 0x33,
                /// <summary>此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标</summary>
                WM_QUERYDRAGICON = 0x37,
                /// <summary>发送此消息来判定combobox或listbox新增加的项的相对位置</summary>
                WM_COMPAREITEM = 0x39,
                /// <summary>显示内存已经很少了</summary>
                WM_COMPACTING = 0x41,
                /// <summary>发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数</summary>
                WM_WINDOWPOSCHANGING = 0x46,
                /// <summary>发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数</summary>
                WM_WINDOWPOSCHANGED = 0x47,
                /// <summary>当系统将要进入暂停状态时发送此消息</summary>
                WM_POWER = 0x48,
                /// <summary>当一个应用程序传递数据给另一个应用程序时发送此消息</summary>
                WM_COPYDATA = 0x4A,
                /// <summary>当某个用户取消程序日志激活状态，提交此消息给程序</summary>
                WM_CANCELJOURNA = 0x4B,
                /// <summary>当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口</summary>
                WM_NOTIFY = 0x4E,
                /// <summary>当用户选择某种输入语言，或输入语言的热键改变</summary>
                WM_INPUTLANGCHANGEREQUEST = 0x50,
                /// <summary>当平台现场已经被改变后发送此消息给受影响的最顶级窗口</summary>
                WM_INPUTLANGCHANGE = 0x51,
                /// <summary>当程序已经初始化windows帮助例程时发送此消息给应用程序</summary>
                WM_TCARD = 0x52,
                /// <summary>此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就发送给有焦点的窗口，如果
                /// 当前都没有焦点，就把此消息发送给当前激活的窗口</summary>
                WM_HELP = 0x53,
                /// <summary>当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息</summary>
                WM_USERCHANGED = 0x54,
                /// <summary>公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构</summary>
                WM_NOTIFYFORMAT = 0x55,
                /// <summary>当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口</summary>
                WM_STYLECHANGING = 0x7C,
                /// <summary>当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口</summary>
                WM_STYLECHANGED = 0x7D,
                /// <summary>当显示器的分辨率改变后发送此消息给所有的窗口</summary>
                WM_DISPLAYCHANGE = 0x7E,
                /// <summary>此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄</summary>
                WM_GETICON = 0x7F,
                /// <summary>程序发送此消息让一个新的大图标或小图标与某个窗口关联</summary>
                WM_SETICON = 0x80,
                /// <summary>当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送</summary>
                WM_NCCREATE = 0x81,
                /// <summary>此消息通知某个窗口，非客户区正在销毁</summary>
                WM_NCDESTROY = 0x82,
                /// <summary>当某个窗口的客户区域必须被核算时发送此消息</summary>
                WM_NCCALCSIZE = 0x83,
                /// <summary>移动鼠标，按住或释放鼠标时发生</summary>
                WM_NCHITTEST = 0x84,
                /// <summary>程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时</summary>
                WM_NCPAINT = 0x85,
                /// <summary>此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态</summary>
                WM_NCACTIVATE = 0x86,
                /// <summary>发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件通过应</summary>
                WM_GETDLGCODE = 0x87,
                /// <summary>当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 非客户区为：窗体的标题栏及窗 的边框体</summary>
                WM_NCMOUSEMOVE = 0xA0,
                /// <summary>当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息</summary>
                WM_NCLBUTTONDOWN = 0xA1,
                /// <summary>当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息</summary>
                WM_NCLBUTTONUP = 0xA2,
                /// <summary>当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息</summary>
                WM_NCLBUTTONDBLCLK = 0xA3,
                /// <summary>当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息</summary>
                WM_NCRBUTTONDOWN = 0xA4,
                /// <summary>当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息</summary>
                WM_NCRBUTTONUP = 0xA5,
                /// <summary>当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息</summary>
                WM_NCRBUTTONDBLCLK = 0xA6,
                /// <summary>当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息</summary>
                WM_NCMBUTTONDOWN = 0xA7,
                /// <summary>当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息</summary>
                WM_NCMBUTTONUP = 0xA8,
                /// <summary>当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息</summary>
                WM_NCMBUTTONDBLCLK = 0xA9,

                /// <summary>
                /// 
                /// </summary>
                WM_USER = 0x0400,
                /// <summary>
                /// 
                /// </summary>
                MK_LBUTTON = 0x0001,
                /// <summary>
                /// 
                /// </summary>
                MK_RBUTTON = 0x0002,
                /// <summary>
                /// 
                /// </summary>
                MK_SHIFT = 0x0004,
                /// <summary>
                /// 
                /// </summary>
                MK_CONTROL = 0x0008,
                /// <summary>
                /// 
                /// </summary>
                MK_MBUTTON = 0x0010,
                /// <summary>
                /// 
                /// </summary>
                MK_XBUTTON1 = 0x0020,
                /// <summary>
                /// 
                /// </summary>
                MK_XBUTTON2 = 0x0040,

                /// <summary>
                /// 
                /// </summary>
                WM_SENDTHISHWND = WM_USER + 143,
                /// <summary>
                /// 
                /// </summary>
                WM_WEBVIEWISSTART = WM_USER + 144,
                /// <summary>
                /// 
                /// </summary>
                WM_SENDTOCLOSEFORM = WM_USER + 145,

                /// <summary>
                /// 移动信息 
                /// </summary>
                SC_MOVE = 0xF010,
                /// <summary>
                /// 表示鼠标在窗口标题栏时的系统信息 
                /// </summary>
                HTCAPTION = 0x0002,
                /// <summary>
                /// 表示鼠标在窗口客户区的系统消息
                /// </summary>
                HTCLIENT = 0x01
            }

            /// <summary>
            /// 窗口滑动的方向
            /// </summary>
            public enum Slide : int
            {
                /// <summary>
                /// 上到下
                /// </summary>
                Top2Bottom = AnimateParm.AW_VER_POSITIVE,
                /// <summary>
                /// 右到左
                /// </summary>
                Right2Left = AnimateParm.AW_HOR_NEGATIVE,
                /// <summary>
                /// 左到右
                /// </summary>
                Left2Right = AnimateParm.AW_HOR_POSITIVE,
                /// <summary>
                /// 下到上
                /// </summary>
                Bottom2Top = AnimateParm.AW_VER_NEGATIVE
            }

            public enum AnimateParm : int
            {
                /// <summary>
                /// 自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
                /// </summary>
                AW_HOR_POSITIVE = 0x0001,
                /// <summary>
                /// 自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
                /// </summary>
                AW_HOR_NEGATIVE = 0x0002,
                /// <summary>
                /// 自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
                /// </summary>
                AW_VER_POSITIVE = 0x0004,
                /// <summary>
                /// 自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
                /// </summary>
                AW_VER_NEGATIVE = 0x0008,

                /// <summary>
                /// 若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
                /// </summary>
                AW_CENTER = 0x00000010,
                /// <summary>
                /// 隐藏窗口，缺省则显示窗口
                /// </summary>
                AW_HIDE = 0x00010000,
                /// <summary>
                /// 激活窗口。在使用了AW_HIDE标志后不能使用这个标志
                /// </summary>
                AW_ACTIVATE = 0x00020000,
                /// <summary>
                /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
                /// </summary>
                AW_SLIDE = 0x00040000,
                /// <summary>
                /// 透明度从高到低
                /// </summary>
                AW_BLEND = 0x00080000
            }

            #endregion

            #region DllImport

            /// <summary>
            /// 该函数检索一指定窗口的客户区域或整个屏幕的显示设备上下文环境的句柄，以后可以在GDI函数中使用该句柄来在设备上下文环境中绘图。hWnd：设备上下文环境被检索的窗口的句柄
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetDC(IntPtr hWnd);

            /// <summary>
            /// 函数释放设备上下文环境（DC）供其他应用程序使用。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            /// <summary>
            /// 该函数返回桌面窗口的句柄。桌面窗口覆盖整个屏幕。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            //通过窗口的标题来查找窗口的句柄
            [DllImport("User32.dll", EntryPoint = "FindWindow")]
            public static extern int FindWindow(string lpClassName, string lpWindowName);

            /// <summary>
            /// 该函数设置指定窗口的显示状态。
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool ShowWindow(IntPtr hWnd, short State);

            /// <summary>
            /// 通过发送重绘消息 WM_PAINT 给目标窗体来更新目标窗体客户区的无效区域。
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool UpdateWindow(IntPtr hWnd);

            /// <summary>
            /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool SetForegroundWindow(IntPtr hWnd);

            /// <summary>
            /// 该函数改变一个子窗口，弹出式窗口式顶层窗口的尺寸，位置和Z序。
            /// 一般可用来让窗口置于最前(SetWindowPos(this.Handle,-1,0,0,0,0,0x4000|0x0001|0x0002);)
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, int flags);

            /// <summary>
            /// 打开剪切板
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool OpenClipboard(IntPtr hWndNewOwner);

            /// <summary>
            /// 关闭剪切板
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool CloseClipboard();

            /// <summary>
            /// 打开并清空剪切板
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool EmptyClipboard();

            /// <summary>
            /// 将存放有数据的内存块放入剪切板的资源管理中
            /// </summary>
            [DllImport("user32.dll")]
            static public extern IntPtr SetClipboardData(uint Format, IntPtr hData);

            /// <summary>
            /// 在一个矩形中装载指定菜单条目的屏幕坐标信息 
            /// </summary>
            [DllImport("user32.dll")]
            static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);

            /// <summary>
            /// 该函数获得一个指定子窗口的父窗口句柄。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetParent(IntPtr hWnd);

            /// <summary>
            /// 该函数将指定的消息同步发送到一个或多个窗口。
            /// 此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。在同一线程中发送消息并不入线程消息队列。　
            /// </summary>
            /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄</param>
            /// <param name="wMsg">指定被发送的消息</param>
            /// <param name="wParam">指定附加的消息指定信息</param>
            /// <param name="lParam">指定附加的消息指定信息</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);

            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);

            [DllImport("user32.dll")]
            public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);

            [DllImport("user32.dll")]
            public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);

            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);

            [DllImport("user32.dll")]
            public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TVITEM lParam);

            [DllImport("User32.dll")]
            public static extern int SendMessage(int hWnd,int msg, int wParam, ref CopyDataStruct lParam);
            /// <summary>
            /// 该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里,与SendMessage对应。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);

            [DllImport("user32.dll")]
            public static extern bool UnhookWindowsHookEx(IntPtr hhook);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);

            /// <summary>
            /// 该函数对指定的窗口设置键盘焦点。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr SetFocus(IntPtr hWnd);

            /// <summary>
            /// 该函数在指定的矩形里写入格式化文本，根据指定的方法对文本格式化（扩展的制表符，字符对齐、折行等）。
            /// </summary>
            [DllImport("user32.dll")]
            public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);

            /// <summary>
            /// 该函数改变指定子窗口的父窗口。
            /// </summary>
            [DllImport("user32.dll")]
            public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);

            /// <summary>
            /// 获取对话框中子窗口控件的句柄
            /// </summary>
            [DllImport("user32.dll")]
            public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);

            /// <summary>
            /// 该函数获取窗口客户区的坐标。
            /// </summary>
            [DllImport("user32.dll")]
            public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);

            /// <summary>
            /// 该函数向指定的窗体添加一个矩形，然后窗口客户区域的这一部分将被重新绘制。
            /// </summary>
            [DllImport("user32.dll")]
            public extern static int InvalidateRect(IntPtr hWnd, IntPtr rect, int bErase);

            /// <summary>
            /// 该函数产生对其他线程的控制，如果一个线程没有其他消息在其消息队列里。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool WaitMessage();

            /// <summary>
            /// 该函数为一个消息检查线程消息队列，并将该消息（如果存在）放于指定的结构。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);

            /// <summary>
            /// 该函数从调用线程的消息队列里取得一个消息并将其放于指定的结构。此函数可取得与指定窗口联系的消息和由PostThreadMesssge寄送的线程消息。此函数接收一定范围的消息值。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);

            /// <summary>
            /// 该函数将虚拟键消息转换为字符消息。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool TranslateMessage(ref MSG msg);

            /// <summary>
            /// 该函数调度一个消息给窗口程序。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool DispatchMessage(ref MSG msg);

            /// <summary>
            /// 该函数从一个与应用事例相关的可执行文件（EXE文件）中载入指定的光标资源.
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);

            /// <summary>
            /// 该函数确定光标的形状。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr SetCursor(IntPtr hCursor);

            /// <summary>
            /// 确定当前焦点位于哪个控件上。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetFocus();

            /// <summary>
            /// 该函数从当前线程中的窗口释放鼠标捕获，并恢复通常的鼠标输入处理。捕获鼠标的窗口接收所有的鼠标输入（无论光标的位置在哪里），除非点击鼠标键时，光标热点在另一个线程的窗口中。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();

            /// <summary>
            /// 获得鼠标拖动
            /// </summary>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern IntPtr SetCapture(IntPtr hWnd);

            /// <summary>
            /// 准备指定的窗口来重绘并将绘画相关的信息放到一个PAINTSTRUCT结构中。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

            /// <summary>
            /// 标记指定窗口的绘画过程结束,每次调用BeginPaint函数之后被请求
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

            /// <summary>
            /// 半透明窗体
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

            /// <summary>
            /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

            /// <summary>
            /// 该函数将指定点的用户坐标转换成屏幕坐标。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

            /// <summary>
            /// 当在指定时间内鼠标指针离开或盘旋在一个窗口上时，此函数寄送消息。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT tme);

            /// <summary>
            /// 
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

            /// <summary>
            /// 该函数检取指定虚拟键的状态。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern ushort GetKeyState(int virtKey);

            /// <summary>
            /// 该函数改变指定窗口的位置和尺寸。对于顶层窗口，位置和尺寸是相对于屏幕的左上角的：对于子窗口，位置和尺寸是相对于父窗口客户区的左上角坐标的。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

            /// <summary>
            /// 该函数获得指定窗口所属的类的类名。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int GetClassName(IntPtr hWnd, out StringBuilder ClassName, int nMaxCount);

            /// <summary>
            /// 该函数改变指定窗口的属性
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

            /// <summary>
            /// 该函数检索指定窗口客户区域或整个屏幕的显示设备上下文环境的句柄，在随后的GDI函数中可以使用该句柄在设备上下文环境中绘图。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);

            /// <summary>
            /// 获取整个窗口（包括边框、滚动条、标题栏、菜单等）的设备场景 返回值 Long。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            /// <summary>
            /// 该函数用指定的画刷填充矩形，此函数包括矩形的左上边界，但不包括矩形的右下边界。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);

            /// <summary>
            /// 该函数返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT wp);

            /// <summary>
            /// 该函数改变指定窗口的标题栏的文本内容
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int SetWindowText(IntPtr hWnd, string text);

            /// <summary>
            /// 该函数将指定窗口的标题条文本（如果存在）拷贝到一个缓存区内。如果指定的窗口是一个控制，则拷贝控制的文本。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, out StringBuilder text, int maxCount);

            /// <summary>
            /// 用于得到被定义的系统数据或者系统配置信息.
            /// </summary>
            [DllImport("user32.dll")]
            static public extern int GetSystemMetrics(int nIndex);

            /// <summary>
            /// 该函数设置滚动条参数，包括滚动位置的最大值和最小值，页面大小，滚动按钮的位置。
            /// </summary>
            [DllImport("user32.dll")]
            static public extern int SetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si, int fRedraw);

            /// <summary>
            /// 该函数显示或隐藏所指定的滚动条。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);

            /// <summary>
            /// 该函数可以激活一个或两个滚动条箭头或是使其失效。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);

            /// <summary>
            /// 该函数将指定的窗口设置到Z序的顶部。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int BringWindowToTop(IntPtr hWnd);

            /// <summary>
            /// 该函数滚动指定窗体客户区域的目录。
            /// </summary>
            [DllImport("user32.dll")]
            static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref RECT rcScroll, ref RECT rcClip, IntPtr UpdateRegion, ref RECT rcInvalidated, uint flags);

            /// <summary>
            /// 该函数确定给定的窗口句柄是否识别一个已存在的窗口。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int IsWindow(IntPtr hWnd);

            /// <summary>
            /// 该函数将256个虚拟键的状态拷贝到指定的缓冲区中。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int GetKeyboardState(byte[] pbKeyState);

            /// <summary>
            /// 该函数将指定的虚拟键码和键盘状态翻译为相应的字符或字符串。该函数使用由给定的键盘布局句柄标识的物理键盘布局和输入语言来翻译代码。
            /// </summary>
            [DllImport("user32.dll")]
            public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

            /// <summary>
            /// Provides access to function required to delete handle. This method is used internally
            /// and is not required to be called separately.
            /// </summary>
            /// <param name="hIcon">Pointer to icon handle.</param>
            /// <returns>N/A</returns>
            [DllImport("User32.dll")]
            public static extern int DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll")]
            public static extern IntPtr LoadCursorFromFile(string fileName);

            /// <summary>
            /// The SetClassLong function replaces the specified 32-bit (long) value at the 
            /// specified offset into the extra class memory or the WNDCLASSEX structure 
            /// for the class to which the specified window belongs.
            /// NOTE: This function has been superseded by the SetClassLongPtr function. 
            /// To write code that is compatible with both 32-bit and 64-bit versions of 
            /// Microsoft Windows, use SetClassLongPtr.
            /// </summary>
            /// <param name="hwnd">The HWND.</param>
            /// <param name="nIndex">Index of the n.</param>
            /// <param name="dwNewLong">The dw new long.</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

            /// <summary>
            /// GetClassLong retrieves a single 32-bit value from the information 
            /// about the window class to which the specified window belongs. 
            /// The class's properties may not necessarily match perfectly with 
            /// the actual properties of the window. 
            /// This function can also retrieve a 32-bit value from the extra 
            /// memory area associated with the window class. GetClassLong Retrieves 
            /// the specified 32-bit value from the WNDCLASSEX structure associated 
            /// with the specified window. 
            /// </summary>
            /// <param name="hwnd">The HWND.</param>
            /// <param name="nIndex">Index of the n.</param>
            /// <returns>If an error occured, 
            /// the function returns 0 (use GetLastError to get the error code). 
            /// If successful, the function returns the desired 32-bit value. </returns>
            [DllImport("user32.dll")]
            public static extern int GetClassLong(IntPtr hwnd, int nIndex);

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(int hwnd, int dwTime, int dwFlags);

            #region Global Hotkey

            /// <summary>
            /// 注册全局热键
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <param name="id">The id.</param>
            /// <param name="fsModifiers">The fs modifiers.</param>
            /// <param name="vk">The vk.</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RegisterHotKey(IntPtr hWnd, int id, GlobalHotkeyModifiers fsModifiers, Keys vk);

            /// <summary>
            /// 取消全局热键
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <param name="id">The id.</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            #endregion
            #endregion

            #region Struct

            [StructLayout(LayoutKind.Sequential)]
            public struct BLENDFUNCTION
            {
                public byte BlendOp;
                public byte BlendFlags;
                public byte SourceConstantAlpha;
                public byte AlphaFormat;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TRACKMOUSEEVENT
            {
                public UInt32 cbSize;
                public UInt32 dwFlags;
                public IntPtr hWnd;
                public UInt32 dwHoverTime;

                public TRACKMOUSEEVENT(UInt32 dwFlags, IntPtr hWnd, UInt32 dwHoverTime)
                {
                    this.cbSize = 16;
                    this.dwFlags = dwFlags;
                    this.hWnd = hWnd;
                    this.dwHoverTime = dwHoverTime;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SIZE
            {
                public int cx;
                public int cy;
                public SIZE(int cx, int cy)
                {
                    this.cx = cx;
                    this.cy = cy;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MSG
            {
                public IntPtr hwnd;
                public uint message;
                public IntPtr wParam;
                public IntPtr lParam;
                public uint time;
                public POINT pt;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct RECT
            {
                [FieldOffset(0)]
                public int left;
                [FieldOffset(4)]
                public int top;
                [FieldOffset(8)]
                public int right;
                [FieldOffset(12)]
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PAINTSTRUCT
            {
                public IntPtr hdc;
                public bool fErase;
                public RECT rcPaint;
                public bool fRestore;
                public bool fIncUpdate;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                public byte[] rgbReserved;
            }

            [Serializable, StructLayout(LayoutKind.Sequential)]
            public struct SCROLLINFO
            {
                public int cbSize;
                public int fMask;
                public int nMin;
                public int nMax;
                public int nPage;
                public int nPos;
                public int nTrackPos;
            }

            /// <summary>
            /// Contains information about the placement of a window on the screen.
            /// </summary>
            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public struct WINDOWPLACEMENT
            {
                /// <summary>
                /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
                /// <para>
                /// GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
                /// </para>
                /// </summary>
                public int Length;

                /// <summary>
                /// Specifies flags that control the position of the minimized window and the method by which the window is restored.
                /// </summary>
                public int Flags;

                /// <summary>
                /// The current show state of the window.
                /// </summary>
                public ShowWindowCommand ShowCmd;

                /// <summary>
                /// The coordinates of the window's upper-left corner when the window is minimized.
                /// </summary>
                public POINT MinPosition;

                /// <summary>
                /// The coordinates of the window's upper-left corner when the window is maximized.
                /// </summary>
                public POINT MaxPosition;

                /// <summary>
                /// The window's coordinates when the window is in the restored position.
                /// </summary>
                public RECT NormalPosition;

                /// <summary>
                /// Gets the default (empty) value.
                /// </summary>
                public static WINDOWPLACEMENT Default
                {
                    get
                    {
                        WINDOWPLACEMENT result = new WINDOWPLACEMENT();
                        result.Length = Marshal.SizeOf(result);
                        return result;
                    }
                }
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct TBBUTTON
            {
                public int iBitmap;
                public int idCommand;
                public byte fsState;
                public byte fsStyle;
                public byte bReserved0;
                public byte bReserved1;
                public int dwData;
                public int iString;
            }
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct TBBUTTONINFO
            {
                public int cbSize;
                public int dwMask;
                public int idCommand;
                public int iImage;
                public byte fsState;
                public byte fsStyle;
                public short cx;
                public IntPtr lParam;
                public IntPtr pszText;
                public int cchText;
            }
            [StructLayout(LayoutKind.Sequential)]
            public struct REBARBANDINFO
            {
                public int cbSize;
                public int fMask;
                public int fStyle;
                public int clrFore;
                public int clrBack;
                public IntPtr lpText;
                public int cch;
                public int iImage;
                public IntPtr hwndChild;
                public int cxMinChild;
                public int cyMinChild;
                public int cx;
                public IntPtr hbmBack;
                public int wID;
                public int cyChild;
                public int cyMaxChild;
                public int cyIntegral;
                public int cxIdeal;
                public int lParam;
                public int cxHeader;
            }

            /// <summary>
            ///     WM_COPYDATA消息所要求的数据结构
            /// </summary>
            public struct CopyDataStruct
            {
                public int cbData;
                public IntPtr dwData;

                [MarshalAs(UnmanagedType.LPStr)]
                public string lpData;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TVITEM
            {
                public uint mask;
                public IntPtr hItem;
                public uint state;
                public uint stateMask;
                public IntPtr pszText;
                public int cchTextMax;
                public int iImage;
                public int iSelectedImage;
                public int cChildren;
                public IntPtr lParam;
            }
            #endregion

            #region 回调函数

            public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

            #endregion
        }
    }
}
    
