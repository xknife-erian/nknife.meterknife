using System;
using System.IO;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Properties;
using MeterKnife.Common.Winforms.Dialogs;
using MeterKnife.Workbench.Views;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench
{
    public sealed partial class MainWorkbench : Form
    {
        private const string DOCK_PANEL_CONFIG = "dockpanel.config";
        private static readonly ILog _Logger = LogManager.GetLogger<MainWorkbench>();

        private readonly DockContent _commandConsoleView = new CommandConsoleView();
        private readonly DockContent _dataManagerView = new DataMangerView();

        private readonly DockPanel _dockPanel = new DockPanel();
        private readonly DockContent _interfaceTreeView = new InterfaceTreeView();
        private readonly DockContent _loggerView = new LoggerView();

        public MainWorkbench()
        {
            InitializeComponent();
#if DEBUG
            _LoggerViewMenuItem.Checked = true;
#endif
            ViewMenuItemClickMethod();
            var about = DI.Get<IAbout>();
            var title = about.AssemblyTitle;
            Text = $"{title}2020 - {about.AssemblyVersion}";
            Icon = GlobalResources.main_icon;
            _Logger.Info("主窗体构建完成");
            InitializeDockPanel();
            _Logger.Info("添加Dock面板完成");
            Closing += (s, e) => DockPanelSaveAsXml();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CheckDataPath();
        }

        private void ViewMenuItemClickMethod()
        {
            _ResetViewMenuItem.Click += (s, e) =>
            {
                //重置视图(删除视图配置文件,然后重新Load所有视图)
                var configFile = GetLayoutConfigFile();
                File.Delete(configFile);
                DockPanelLoadFromXml();
            };
            _DataManagerViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_DataManagerViewMenuItem.Checked)
                    _dataManagerView.Show(_dockPanel, DockState.DockRight);
                else
                    _dataManagerView.Hide();
            };
            _InterfaceTreeViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_InterfaceTreeViewMenuItem.Checked)
                    _interfaceTreeView.Show(_dockPanel, DockState.DockRight);
                else
                    _interfaceTreeView.Hide();
            };
            _CommandConsoleViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_CommandConsoleViewMenuItem.Checked)
                    _commandConsoleView.Show(_dockPanel, DockState.DockRight);
                else
                    _commandConsoleView.Hide();
            };
            _LoggerViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_LoggerViewMenuItem.Checked)
                    _loggerView.Show(_dockPanel, DockState.DockRight);
                else
                    _loggerView.Hide();
            };
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutDialog();
            about.ShowDialog(this);
        }

        private void CheckDataPath() // 检查数据路径是否设置,当第一次使本软件时是未设置的状态的.
        {
//            var userData = DI.Get<MeterKnifeUserData>();
//            object path;
//            if (!userData.TryGetValue(MeterKnifeUserData.DATA_PATH, out path))
//            {
//                var dialog = new DataPathSetterDialog();
//                if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
//                {
//                    path = dialog.DataPath;
//                    userData.SetValue(MeterKnifeUserData.DATA_PATH, path);
                    _Logger.Warn($"设置用户数据路径");
                    //:{path}");
//                }
//            }
//            else
//            {
//                if (!Directory.Exists(path.ToString()))
//                {
//                    UtilityFile.CreateDirectory(path.ToString());
//                    _Logger.Info($"用户数据路径丢失{path},重新创建");
//                }
//            }
        }

        #region DockPanel

        private static string GetLayoutConfigFile()
        {
            var dir = Path.GetDirectoryName(Application.ExecutablePath);
            return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_dockPanel);

            _dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _dockPanel.Theme = new VS2015BlueTheme();
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.BringToFront();

            DockPanelLoadFromXml();
        }

        /// <summary>
        ///     控件提供了一个保存布局状态的方法，它默认是没有保存的。
        /// </summary>
        private void DockPanelSaveAsXml()
        {
            _dockPanel.SaveAsXml(GetLayoutConfigFile());
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            var configFile = GetLayoutConfigFile();
            if (File.Exists(configFile))
            {
                _dockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
            else
            {
                if (_DataManagerViewMenuItem.Checked)
                    _dataManagerView.Show(_dockPanel, DockState.DockRight);
                if (_InterfaceTreeViewMenuItem.Checked)
                    _interfaceTreeView.Show(_dockPanel, DockState.DockRight);

                if (_CommandConsoleViewMenuItem.Checked)
                    _commandConsoleView.Show(_dockPanel, DockState.DockBottom);
                if (_LoggerViewMenuItem.Checked)
                    _loggerView.Show(_dockPanel, DockState.DockBottom);

                // var collectDataView = new CollectDataView();
                // collectDataView.Show(_DockPanel, DockState.Document);
            }
        }

        private IDockContent GetViewFromPersistString(string persistString)
        {
            if (persistString == typeof(LoggerView).ToString())
                if (_LoggerViewMenuItem.Checked)
                    return _loggerView;
            if (persistString == typeof(InterfaceTreeView).ToString())
                if (_InterfaceTreeViewMenuItem.Checked)
                    return _interfaceTreeView;
            if (persistString == typeof(DataMangerView).ToString())
                if (_DataManagerViewMenuItem.Checked)
                    return _dataManagerView;
            if (persistString == typeof(CommandConsoleView).ToString())
                if (_CommandConsoleViewMenuItem.Checked)
                    return _commandConsoleView;
            return null;
        }

        #endregion
    }
}