using System;
using System.IO;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Interfaces;
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
        private static readonly ILog _logger = LogManager.GetLogger<MainWorkbench>();

        private readonly DockContent _CommandConsoleView = new CommandConsoleView();
        private readonly DockContent _DataManagerView = new DataMangerView();

        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _InterfaceTreeView = new InterfaceTreeView();
        private readonly DockContent _LoggerView = new LoggerView();

        public MainWorkbench()
        {
            InitializeComponent();
#if DEBUG
            _LoggerViewMenuItem.Checked = true;
#endif
            ViewMenuItemClickMethod();
            var about = DI.Get<IAbout>();
            string title = about.AssemblyTitle;
            Text = string.Format("{0}2015 - {1}", title, about.AssemblyVersion);
            Icon = GlobalResources.main_icon;
            _logger.Info("主窗体构建完成");
            InitializeDockPanel();
            _logger.Info("添加Dock面板完成");
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
                string configFile = GetLayoutConfigFile();
                File.Delete(configFile);
                DockPanelLoadFromXml();
            };
            _DataManagerViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_DataManagerViewMenuItem.Checked)
                    _DataManagerView.Show(_DockPanel, DockState.DockRight);
                else
                    _DataManagerView.Hide();
            };
            _InterfaceTreeViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_InterfaceTreeViewMenuItem.Checked)
                    _InterfaceTreeView.Show(_DockPanel, DockState.DockRight);
                else
                    _InterfaceTreeView.Hide();
            };
            _CommandConsoleViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_CommandConsoleViewMenuItem.Checked)
                    _CommandConsoleView.Show(_DockPanel, DockState.DockRight);
                else
                    _CommandConsoleView.Hide();
            };
            _LoggerViewMenuItem.CheckedChanged += (s, e) =>
            {
                if (_LoggerViewMenuItem.Checked)
                    _LoggerView.Show(_DockPanel, DockState.DockRight);
                else
                    _LoggerView.Hide();
            };
        }

        #region DockPanel

        private static string GetLayoutConfigFile()
        {
            string dir = Path.GetDirectoryName(Application.ExecutablePath);
            return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
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
            _DockPanel.SaveAsXml(GetLayoutConfigFile());
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            string configFile = GetLayoutConfigFile();
            if (File.Exists(configFile))
            {
                _DockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
            else
            {
                if (_DataManagerViewMenuItem.Checked)
                    _DataManagerView.Show(_DockPanel, DockState.DockRight);
                if (_InterfaceTreeViewMenuItem.Checked)
                    _InterfaceTreeView.Show(_DockPanel, DockState.DockRight);

                if (_CommandConsoleViewMenuItem.Checked)
                    _CommandConsoleView.Show(_DockPanel, DockState.DockBottom);
                if (_LoggerViewMenuItem.Checked)
                    _LoggerView.Show(_DockPanel, DockState.DockBottom);

                // var collectDataView = new CollectDataView();
                // collectDataView.Show(_DockPanel, DockState.Document);
            }
        }

        private IDockContent GetViewFromPersistString(string persistString)
        {
            if (persistString == typeof (LoggerView).ToString())
            {
                if (_LoggerViewMenuItem.Checked)
                    return _LoggerView;
            }
            if (persistString == typeof (InterfaceTreeView).ToString())
            {
                if (_InterfaceTreeViewMenuItem.Checked)
                    return _InterfaceTreeView;
            }
            if (persistString == typeof (DataMangerView).ToString())
            {
                if (_DataManagerViewMenuItem.Checked)
                    return _DataManagerView;
            }
            if (persistString == typeof (CommandConsoleView).ToString())
            {
                if (_CommandConsoleViewMenuItem.Checked)
                    return _CommandConsoleView;
            }
            return null;
        }

        #endregion

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutDialog();
            about.ShowDialog(this);
        }

        private void CheckDataPath()// 检查数据路径是否设置,当第一次使本软件时是未设置的状态的.
        {
            var userData = DI.Get<MeterKnifeUserData>();
            object path;
            if (!userData.TryGetValue(MeterKnifeUserData.DATA_PATH, out path))
            {
                var dialog = new DataPathSetterDialog();
                if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    path = dialog.DataPath;
                    userData.SetValue(MeterKnifeUserData.DATA_PATH, path);
                    _logger.Info(string.Format("设置用户数据路径:{0}", path));
                }
            }
            else
            {
                if (!Directory.Exists(path.ToString()))
                {
                    UtilityFile.CreateDirectory(path.ToString());
                    _logger.Info(string.Format("用户数据路径丢失{0},重新创建", path));
                }
            }
        }

    }
}