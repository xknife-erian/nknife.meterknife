﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.ViewModels;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Controls;
using NKnife.MeterKnife.Workbench.Dialogs;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public sealed partial class EngineeringView : SingletonDockContent
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly EngineeringViewModel _viewModel;

        public EngineeringView(EngineeringViewModel viewModel, IDialogProvider dialogProvider)
        {
            _viewModel = viewModel;
            _dialogProvider = dialogProvider;
            InitializeComponent();
            Text = this.Res("工程管理");
            RespondToEvents();
            Shown += EngineeringsBindToTree;
        }

        private void RespondToEvents()
        {
            _TreeView.AfterSelect += (s, e) =>
            {
                if (e.Node.Tag is Engineering engineering)
                {
                    var evm = new EngineeringVm(engineering);
                    _EngPropertyGrid.SelectedObject = evm;
                }
            };
            _CreateEngStripButton.Click += (s, e) =>
            {
                var dialog = _dialogProvider.New<EngineeringDetailDialog>();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                }
            };
        }

        private async void EngineeringsBindToTree(object sender, EventArgs e)
        {
            var engList = await _viewModel.GetEngineeringAndDateMap();
            foreach (var pair in engList)
            {
                var dateNode = new EngineeringCreateTimeTreeNode(pair.Key);
                dateNode.Tag = pair.Key;
                _TreeView.Nodes.Add(dateNode);
                foreach (var engineering in pair.Value)
                {
                    var engNode = new EngineeringTreeNode(engineering.Name);
                    engNode.Tag = engineering;
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
        }

        public class EngineeringVm
        {
            public EngineeringVm(Engineering engineering)
            {
                Number = engineering.Number;
                Name = engineering.Name;
                CreateTime = engineering.CreateTime;
                Description = engineering.Description;
                Path = engineering.Path;
                Duts = new DUT[engineering.Commands.Count];
                for (int i = 0; i < engineering.Commands.Count; i++)
                {
                    Duts[i] = engineering.Commands[i].DUT;
                }
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

