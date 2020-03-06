using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Controls;
using NKnife.MeterKnife.Workbench.Dialogs.Engineerings;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    /// <summary>
    /// 工程管理窗体
    /// </summary>
    public sealed partial class EngineeringView : SingletonDockContent
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly IWorkbenchViewModel _viewModel;
        private readonly ContextMenuStrip _contextMenu = new ContextMenuStrip();

        public EngineeringView(IWorkbenchViewModel viewModel, IDialogProvider dialogProvider)
        {
            _viewModel = viewModel;
            _dialogProvider = dialogProvider;

            InitializeComponent();
            InitializeLanguageAndImage();
            InitializeContextMenu();
            RespondToEvents();
            RespondToButtonClick();
            UpdateControlState();

            Shown += EngineeringsBindToTree;
            _TreeView.MouseDown += OnTreeViewMouseDown;
        }

        private void InitializeContextMenu()
        {
            _viewModel.EngineeringStateList.CollectionChanged += delegate(object o, NotifyCollectionChangedEventArgs e) { };
            var startMenu = new ToolStripMenuItem(this.Res("开始测量"));
            startMenu.Click += async delegate { await _viewModel.StartAcquireAsync(); };
            var pauseMenu = new ToolStripMenuItem(this.Res("暂停测量"));
            pauseMenu.Click += delegate
            {
                var isPause = (bool) pauseMenu.Tag;
                if (isPause)
                    _viewModel.ResumeAcquire();
                else
                    _viewModel.PauseAcquire();
            };
            var stopMenu = new ToolStripMenuItem(this.Res("停止测量"));
            stopMenu.Click += delegate { _viewModel.StopAcquire(); };
            _contextMenu.Items.AddRange(new ToolStripItem[] {startMenu, pauseMenu, stopMenu});
            _contextMenu.Opening += delegate(object sender, CancelEventArgs args)
            {
                var eng = _viewModel.CurrentActiveEngineering;
                EngineeringState engState = _viewModel.EngineeringStateList.FirstOrDefault(state => eng.Id.Equals(state.EngineeringId));
                if (engState == null)
                {
                    startMenu.Enabled = true;
                    pauseMenu.Enabled = false;
                    pauseMenu.Tag = false; 
                    pauseMenu.Text = this.Res("暂停测量");
                    stopMenu.Enabled = false;
                    return;
                }
                switch (engState.EngState)
                {
                    case EngineeringState.State.Stop:
                        startMenu.Enabled = true;
                        pauseMenu.Enabled = false;
                        pauseMenu.Tag = false;
                        pauseMenu.Text = this.Res("暂停测量");
                        stopMenu.Enabled = false;
                        break;
                    case EngineeringState.State.Pause:
                        startMenu.Enabled = false;
                        pauseMenu.Enabled = true;
                        pauseMenu.Tag = true;
                        pauseMenu.Text = this.Res("恢复测量");
                        stopMenu.Enabled = false;
                        break;
                    case EngineeringState.State.Start:
                        startMenu.Enabled = false;
                        pauseMenu.Enabled = true;
                        pauseMenu.Tag = false;
                        pauseMenu.Text = this.Res("暂停测量");
                        stopMenu.Enabled = true;
                        break;
                }
            };
        }

        private void InitializeLanguageAndImage()
        {
            _CreateEngStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _CreateEngStripButton.Image = MenuButtonResource.base_add_24px;
            _EditToolStripButton.Image = MenuButtonResource.base_edit_24px;
            _DeleteStripButton.Image = MenuButtonResource.base_delete_24px;

            this.Res(this);
            this.Res(_CreateEngStripButton, _EditToolStripButton, _DeleteStripButton);
        }

        private void OnTreeViewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = _TreeView.GetNodeAt(e.X, e.Y);
                if (node != null) _TreeView.SelectedNode = node;
                if (node is EngineeringTreeNode engNode)
                {
                    _viewModel.CurrentSelectedEngineering = engNode.Engineering;
                    _contextMenu.Show(_TreeView, e.X, e.Y);
                }
            }
        }

        private void RespondToButtonClick()
        {
            _CreateEngStripButton.Click += async (sender, args) =>
            {
                var dialog = _dialogProvider.New<EngineeringDetailDialog>();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var eng = dialog.Engineering;
                    await _viewModel.CreateEngineeringAsync(eng);
                    var simpleDate = new DateTime(eng.CreateTime.Year, eng.CreateTime.Month, 1, 0, 0, 0);
                    TreeNode dateNode;
                    EngineeringCreateTimeTreeNode srcNode = null;
                    if (_TreeView.Nodes.Count > 0) srcNode = (EngineeringCreateTimeTreeNode) _TreeView.Nodes[0];
                    if (srcNode != null && srcNode.CreateTime.Equals(simpleDate))
                    {
                        dateNode = srcNode;
                    }
                    else
                    {
                        dateNode = new EngineeringCreateTimeTreeNode(simpleDate);
                        _TreeView.Nodes.Insert(0, dateNode);
                    }

                    var engNode = new EngineeringTreeNode(eng);
                    dateNode.Nodes.Add(engNode);
                    engNode.Expand();
                    engNode.EnsureVisible();
                    _TreeView.SelectedNode = engNode;
                }
            };
            _EditToolStripButton.Click += async delegate
            {
                if (_TreeView.SelectedNode is EngineeringTreeNode editNode)
                {
                    var eng = editNode.Engineering;
                    var dialog = _dialogProvider.New<EngineeringDetailDialog>();
                    dialog.Engineering = eng;
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        await _viewModel.UpdateEngineeringAsync(eng);
                    }
                }
            };
            _DeleteStripButton.Click += async (sender, args) =>
            {
                if (_TreeView.SelectedNode is EngineeringTreeNode deleteNode)
                {
                    var eng = deleteNode.Engineering;
                    var info = new StringBuilder();
                    info.AppendLine(this.Res("是否删除?"));
                    info.Append(this.Res("工程名:")).AppendLine(eng.Name);
                    foreach (var dut in eng.GetIncludedDUTArray())
                    {
                        var count = await _viewModel.CountDUTDataAsync(eng, dut);
                        info.AppendLine($"    {this.Res("被测物: ")}{dut.Name}\t{this.Res("数据数量:")}{count}");
                    }

                    var mb = MessageBox.Show($"{info}", this.Res("删除确认"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (mb == DialogResult.Yes)
                    {
                        await _viewModel.DeleteEngineeringAsync(eng);
                        _TreeView.Nodes.Remove(deleteNode);
                    }
                }
            };
            _RefreshStripButton.Click += EngineeringsBindToTree;
        }

        private void RespondToEvents()
        {
            _TreeView.AfterSelect += (sender, args) =>
            {
                if (args.Node is EngineeringTreeNode engNode)
                {
                    //当工程节点被点击后，设置全局的当前工程为该工程
                    _viewModel.CurrentActiveEngineering = engNode.Engineering;
                    var evm = new EngineeringVm(engNode.Engineering);
                    _PropertyGrid.SelectedObject = evm;
                }
                else if (args.Node is DUTTreeNode dutNode)
                {
                    var dut = new DUTVm(dutNode.DUT);
                    _PropertyGrid.SelectedObject = dut;
                }
                UpdateControlState();
            };
            _TreeView.MouseDoubleClick += (sender, args) =>
            {
                var node = _TreeView.SelectedNode as EngineeringTreeNode;
                if (node == null)
                    return;
                _viewModel.OpenedEngineerings.Add(node.Engineering);
            };
        }

        private async void EngineeringsBindToTree(object sender, EventArgs e)
        {
            _TreeView.BeginUpdate();
            _TreeView.Nodes.Clear();
            var engList = await _viewModel.GetEngineeringAndDateMapAsync();
            foreach (var pair in engList)
            {
                var date = pair.Key;
                var dateNode = new EngineeringCreateTimeTreeNode(date.ToString("yyyy-MM")) {CreateTime = date};
                _TreeView.Nodes.Add(dateNode);
                foreach (var engineering in pair.Value)
                {
                    var engNode = new EngineeringTreeNode(engineering);
                    dateNode.Nodes.Add(engNode);
                }
            }

            _TreeView.ExpandAll();
            if (_TreeView.Nodes.Count > 0 && _TreeView.Nodes[0].Nodes.Count > 0)
            {
                _TreeView.SelectedNode = _TreeView.Nodes[0].Nodes[0];
                _TreeView.Nodes[0].EnsureVisible();
            }

            _TreeView.Select();
            _TreeView.EndUpdate();
            UpdateControlState();
        }

        private void UpdateControlState()
        {
            if (_TreeView.SelectedNode is EngineeringTreeNode)
            {
                _DeleteStripButton.Enabled = true;
                _EditToolStripButton.Enabled = true;
            }
            else
            {
                _DeleteStripButton.Enabled = false;
                _EditToolStripButton.Enabled = false;
            }
        }

        private class DUTVm
        {
            public DUTVm(DUT dut)
            {
                Id = dut.Id;
                Name = dut.Name;
                Classify = dut.Classify;
                //Unit = dut.Unit.Name;
                ExpectValue = dut.ExpectValue;
                if (dut.MetrologyValues != null && dut.MetrologyValues.Length > 0)
                    MetrologyValues = dut.MetrologyValues[0].Value.ToString();
                Description = dut.Description;
                CreateTime = dut.CreateTime.ToString("yyyy-M-d HH:mm:ss");
            }

            /// <summary>
            ///     被测物ID，也是编号，全局不可重复
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("编号")]
            public string Id { get; }

            /// <summary>
            ///     被测物名称
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("名称")]
            public string Name { get; }

            /// <summary>
            ///     被测物的登记时间
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("登记时间")]
            public string CreateTime { get; }

            /// <summary>
            ///     被测物分类
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("分类")]
            public string Classify { get; }

            /// <summary>
            ///     计量单位
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("单位")]
            public string Unit { get; set; }

            /// <summary>
            ///     被测物的设计值
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("设计值")]
            public double ExpectValue { get; }

            /// <summary>
            ///     标定值
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("标定值")]
            public string MetrologyValues { get; }

            /// <summary>
            ///     详细描述
            /// </summary>
            [Category("被测量单元")]
            [DisplayName("详细描述")]
            public string Description { get; }
        }

        private class EngineeringVm
        {
            public EngineeringVm(Engineering engineering)
            {
                Number = engineering.Id;
                Name = engineering.Name;
                CreateTime = engineering.CreateTime;
                Description = engineering.Description;
                Path = engineering.Path;
                var ds = new List<DUT>();
                foreach (var pool in engineering.CommandPools)
                foreach (ScpiCommand command in pool)
                    ds.Add(command.DUT);
                Duts = ds.ToArray();
            }

            [Category("工程")] [DisplayName("编号")] public string Number { get; }

            [Category("工程")] [DisplayName("名称")] public string Name { get; }

            [Category("工程")] [DisplayName("创建时间")] public DateTime CreateTime { get; }

            [Category("工程")] [DisplayName("详细描述")] public string Description { get; }

            [Category("工程")] [DisplayName("数据路径")] public string Path { get; }

            [Category("工程")] [DisplayName("被测物")] public DUT[] Duts { get; }
        }
    }
}