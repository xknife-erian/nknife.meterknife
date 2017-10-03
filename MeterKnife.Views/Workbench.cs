using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces;
using MeterKnife.Views.Menus;
using NKnife.Interface;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views
{
    public sealed partial class Workbench : Form, IWorkbench
    {
        private readonly IAbout _About;

        public Workbench()
        {
            InitializeComponent();
            Text = "MeterKnife自动化测量测试平台";
            Width = 1024;
            Height = 768;
            InitializeMainMenu();
            InitializeDockPanel();
            FormManager();
            ControlEventManager();
#if !DEBUG
            WindowState = FormWindowState.Maximized;
#endif
            _About = DI.Get<IAbout>();
            var version =  _About.AssemblyVersion.ToString();
            Text = $"{Text} - {version}";
        }

        private void FormManager()
        {
            SizeChanged += (s, e) =>
            {
                if (WindowState == FormWindowState.Minimized) //判断是否最小化
                {
                    ShowInTaskbar = false; //不显示在系统任务栏
                }
            };
            FormClosing += (s, e) =>
            {
                if (!KernelCallFormClose && e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                }
            };
        }

        private void InitializeMainMenu()
        {
            _MenuStrip.SuspendLayout();
            _MenuStrip.Items.Add(DI.Get<FileMenuItem>());
            _MenuStrip.Items.Add(DI.Get<MeasureMenuItem>());
            _MenuStrip.Items.Add(DI.Get<DataMenuItem>());
            _MenuStrip.Items.Add(DI.Get<ToolMenuItem>());
            _MenuStrip.Items.Add(DI.Get<ViewMenuItem>());
            _MenuStrip.Items.Add(DI.Get<HelpMenuItem>());
            _MenuStrip.ResumeLayout(false);
            _MenuStrip.PerformLayout();
        }

        private void InitializeDockPanel()
        {
            MainDockPanel = DI.Get<DockPanel>();
            MainDockPanel.Theme = new VS2015BlueTheme();
            MainDockPanel.Dock = DockStyle.Fill;
            Controls.Add(MainDockPanel);
            MainDockPanel.BringToFront();
        }

        private void ControlEventManager()
        {
            
        }

        #region Implementation of IWorkbench

        public DockPanel MainDockPanel { get; set; }

        /// <summary>
        /// 是否是应用程序正确请求关闭窗体
        /// </summary>
        public bool KernelCallFormClose { get; set; } = false;

        #endregion
    }
}
