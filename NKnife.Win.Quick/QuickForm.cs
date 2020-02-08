using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
                    Font = new Font("Tahoma", 9F, FontStyle.Regular);
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

        protected void BindMainMenu(params ToolStripMenuItem[] meunItems)
        {
            _MenuStrip.Items.AddRange(meunItems.Cast<ToolStripItem>().ToArray());
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
            _notifyIcon.ContextMenuStrip.Close();
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
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

        public DockPanel MainDockPanel { get; } = new DockPanel();

        public bool HideOnClosing { get; set; } = false;

        #endregion
    }
}
