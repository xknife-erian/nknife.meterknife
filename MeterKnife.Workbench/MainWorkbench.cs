using System;
using System.IO;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Workbench.Views;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench
{
    public partial class MainWorkbench : Form
    {
        private const string DOCK_PANEL_CONFIG = "dockpanel.config";
        private static readonly ILog _logger = LogManager.GetLogger<MainWorkbench>();

        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly string _DockPath = Path.Combine(Application.StartupPath, DOCK_PANEL_CONFIG);
        private readonly DockContent _LoggerView = new LoggerView();

        public MainWorkbench()
        {
            InitializeComponent();
            InitializeDockPanel();
            Closing += (s, e) => DockPanelSaveAsXml();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _DockPanel.SaveAsXml(_DockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }
            base.OnFormClosing(e);
        }

        #region DockPanel

        private static string LayoutConfigFile
        {
            get
            {
                string dir = Path.GetDirectoryName(Application.ExecutablePath);
                return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
            }
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);

            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            DockPanelLoadFromXml();

            //加入日志窗体
            _LoggerView.Text = "运行日志";
            _LoggerView.Show(_DockPanel, DockState.DockBottom);
        }

        /// <summary>
        ///     控件提供了一个保存布局状态的方法，它默认是没有保存的。
        /// </summary>
        private void DockPanelSaveAsXml()
        {
            _DockPanel.SaveAsXml(LayoutConfigFile);
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            string configFile = LayoutConfigFile;
            if (File.Exists(configFile))
            {
                _DockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof (LoggerView).ToString())
                return _LoggerView;
            return null;
        }

        #endregion
    }
}