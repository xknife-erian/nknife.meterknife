using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Contents;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench
{
    public partial class Workbench : Form, IWorkbench
    {
        public Workbench()
        {
            InitializeComponent();
            InitializeDockPanel();
            // var content = new PlotContent();
            // content.Show(MainDockPanel, DockState.Document);
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
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

        #endregion
    }
}
