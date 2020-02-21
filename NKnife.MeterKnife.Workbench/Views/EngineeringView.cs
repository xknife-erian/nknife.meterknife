using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Controls;
using NKnife.MeterKnife.Workbench.Dialogs;
using NKnife.MeterKnife.Workbench.Dialogs.Engineerings;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public sealed partial class EngineeringView : SingletonDockContent
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly IWorkbenchViewModel _viewModel;

        public EngineeringView(IWorkbenchViewModel viewModel, IDialogProvider dialogProvider)
        {
            _viewModel = viewModel;
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeLanguage();
            RespondToEvents();
            RespondToButtonClick();
            Shown += EngineeringsBindToTree;
            _CreateEngStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _CreateEngStripButton.Image = Res.eng_add;
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.Image = Res.eng_edit;
            _DeleteStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteStripButton.Image = Res.eng_delete;
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_CreateEngStripButton, _EditToolStripButton, _DeleteStripButton);
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
                    if (_TreeView.Nodes.Count > 0)
                        srcNode = (EngineeringCreateTimeTreeNode) _TreeView.Nodes[0];
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
            _DeleteStripButton.Click += (sender, args) =>
            {

            };
            _RefreshStripButton.Click += EngineeringsBindToTree;
        }

        private void RespondToEvents()
        {
            _TreeView.AfterSelect += (sender, args) =>
            {
                if (args.Node is EngineeringTreeNode node)
                {
                    var evm = new EngineeringVm(node.Engineering);
                    _EngPropertyGrid.SelectedObject = evm;
                }
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
        }

        public class EngineeringVm
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
                {
                    foreach (var command in pool)
                    {
                        ds.Add(command.DUT);
                    }
                }

                Duts = ds.ToArray();
            }

            [Category("工程"), DisplayName("编号")]
            public string Number { get; set; }

            [Category("工程"), DisplayName("名称")]
            public string Name { get; set; }

            [Category("工程"), DisplayName("创建时间")]
            public DateTime CreateTime { get; set; }

            [Category("工程"), DisplayName("详细描述")]
            public string Description { get; set; }

            [Category("工程"), DisplayName("数据路径")]
            public string Path { get; set; }

            [Category("工程"), DisplayName("被测物")]
            public DUT[] Duts { get; set; }

        }
    }
}

