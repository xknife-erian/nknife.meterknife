using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick
{
    public partial class QuickForm : Form, IWorkbench
    {
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _trayMenu;

        public QuickForm()
        {
            InitializeComponent();
            InitializeDockPanel();
            InitializeFont();
        }

        private void InitializeFont()
        {
            switch (Global.Culture.ToLower())
            {
                case "zh-cn":
                    Font = new Font("微软雅黑", 9F, FontStyle.Regular);
                    break;
                case "en":
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                    break;
                case "zh-tw":
                    Font = new Font("PMingLiU", 9F, FontStyle.Regular);
                    break;
            }
        }

        protected void BindNotifyIcon(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;
            _trayMenu = new TrayMenuStrip(this);
            notifyIcon.ContextMenuStrip = _trayMenu;
            notifyIcon.ContextMenuStrip.Show();
            notifyIcon.ContextMenuStrip.Close();
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += NotifyIconMouseClick;
            notifyIcon.MouseDoubleClick += NotifyIconMouseDoubleClick;
            HideOnClosing = true;
        }

        protected void BindMainMenu(params ToolStripMenuItem[] menuItems)
        {
            _MenuStrip.Items.AddRange(menuItems.Cast<ToolStripItem>().ToArray());
        }

        protected void BindTrayMenu(params ToolStripMenuItem[] menuItems)
        {
            foreach (var item in menuItems)
            {
                _trayMenu.Items.Insert(_trayMenu.Items.Count - 2, item);
            }
        }

        private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowInTaskbar = true;  //显示在系统任务栏
                WindowState = FormWindowState.Normal;  //还原窗体
                Show();
                Activate();
            }
        }

        private void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                _notifyIcon.ContextMenuStrip.Show();
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Task.Run(() =>
            {
                Thread.Sleep(10 * 1000);
                var localVersion = Assembly.GetEntryAssembly()?.GetName().Version;
                if (true)//(Helper.TryGetLatestRelease(GithubUpdateUser, GithubUpdateProject, out var latestRelease, out var errorMessage))
                {
                    var latestVersion = localVersion.ToString();//latestRelease.Version.TrimStart('v', 'V', '.', '-', '_').Trim();
                    if (Version.TryParse(latestVersion, out var version))
                    {
                        _StatusStrip.ThreadSafeInvoke(() =>
                        {
                            _VersionUpdateLabel.Text = version <= localVersion ? $"v.{localVersion}" : this.Res("有新版本建议更新");
                        });
                    }
                    else
                    {
                        _StatusStrip.ThreadSafeInvoke(() => { _VersionUpdateLabel.Text = this.Res("有新版本可以更新"); });
                    }
                }
                else
                {
                    _StatusStrip.ThreadSafeInvoke(() => { _VersionUpdateLabel.Text = this.Res("远程更新失败"); });
                }
            });
            _StatusStrip.ThreadSafeInvoke(() => { _StatusStripLabel.Text = this.Res("就绪"); });
        }

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (HideOnClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Closed" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnClosed(EventArgs e)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.ContextMenuStrip?.Close();
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }

            base.OnClosed(e);
        }

        #endregion

        private static int GetDockPortion()
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            var portion = (int)(w / 7);
            if (portion < 200)
                portion = 200;
            return portion;
        }

        private static Size GetWindowSize()
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            int ww = (int)(w * 0.92);
            int hh = (int)(h * 0.86);
            if (w <= 1024)
                ww = 1024;
            if (hh <= 768)
                hh = 768;
            return new Size(ww, hh);
        }

        #region DockPanel

        private void InitializeDockPanel()
        {
            SuspendLayout();

            _StripContainer.ContentPanel.Controls.Add(MainDockPanel);

            MainDockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            MainDockPanel.Theme = new VS2015BlueTheme();
            MainDockPanel.Dock = DockStyle.Fill;
            MainDockPanel.BringToFront();

            PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        #region Implementation of IWorkbench

        /// <summary>
        /// 主窗体使用的Dock控件
        /// </summary>
        public DockPanel MainDockPanel { get; } = new DockPanel();

        /// <summary>
        /// 当主窗体的Close方法被调用后，是立即关闭还是先隐藏窗体
        /// </summary>
        public bool HideOnClosing { get; set; } = false;

        /// <summary>
        /// 窗体的选项面板
        /// </summary>
        public List<IOptionPanel> OptionPanelList { get; set; } = new List<IOptionPanel>();

        /// <summary>
        ///     尝试获取指定Key的使用习惯的值
        /// </summary>
        public Func<string, string, object> GetHabitValueFunc { get; protected set; }

        /// <summary>
        ///     设置指定Key的使用习惯的值，值对象序列化成Json保存
        /// </summary>
        public Action<string, object> SetHabitAction { get; protected set; }

        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        public Func<string, string, object> GetOptionValueFunc { get; protected set; }

        /// <summary>
        ///     设置指定Key的选项的值，值对象序列化成Json保存
        /// </summary>
        public Action<string, object> SetOptionAction { get; protected set; }

        #endregion
    }
}
