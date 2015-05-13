using System.IO;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Properties;
using MeterKnife.Workbench.Dialogs;
using MeterKnife.Workbench.Views;
using NKnife.Interface;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench
{
    public sealed partial class MainWorkbench : Form
    {
        private const string DOCK_PANEL_CONFIG = "dockpanel.config";
        private static readonly ILog _logger = LogManager.GetLogger<MainWorkbench>();

        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _InterfaceTreeView = new InterfaceTreeView();
        private readonly DockContent _LoggerView = new LoggerView();
        private readonly DockContent _DataManagerView = new DataMangerView();

        public MainWorkbench()
        {
            InitializeComponent();
            var about = DI.Get<IAbout>();
            string title = about.AssemblyTitle;
            Text = string.Format("{0}2015 - {1}", title, about.AssemblyVersion);
            Icon = GlobalResources.main_icon;
            _logger.Info("主窗体构建完成");
            InitializeDockPanel();
            _logger.Info("添加Dock面板完成");
            Closing += (s, e) => DockPanelSaveAsXml();
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
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            string configFile = LayoutConfigFile;
            if (File.Exists(configFile))
            {
                _DockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
            else
            {
                _LoggerView.Show(_DockPanel, DockState.DockBottom);
                _DataManagerView.Show(_DockPanel,DockState.DockRight);
                _InterfaceTreeView.Show(_DockPanel, DockState.DockRight);
                // var collectDataView = new CollectDataView();
                // collectDataView.Show(_DockPanel, DockState.Document);
            }
        }

        private IDockContent GetViewFromPersistString(string persistString)
        {
            if (persistString == typeof (LoggerView).ToString())
                return _LoggerView;
            if (persistString == typeof(InterfaceTreeView).ToString())
                return _InterfaceTreeView;
            if (persistString == typeof(DataMangerView).ToString())
                return _DataManagerView;
            return null;
        }

        #endregion

        private void _AboutMenuItem_Click(object sender, System.EventArgs e)
        {
            var about = new AboutDialog();
            about.ShowDialog(this);
        }
    }
}