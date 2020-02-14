using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.ViewModels;
using NKnife.Win.Quick.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public sealed partial class EngineeringView : SingletonDockContent
    {
        private readonly EngineeringViewModel _viewModel;

        public EngineeringView(EngineeringViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            Text = this.Res("工程管理");
            Shown += EngineeringsBindToTree;
        }

        private async void EngineeringsBindToTree(object sender, EventArgs e)
        {
            var engList = await _viewModel.GetEngineeringAndDateMap();
            foreach (var pair in engList)
            {
                var dateNode = new TreeNode(pair.Key);
                dateNode.Tag = pair.Key;
                _treeView.Nodes.Add(dateNode);
                foreach (var engineering in pair.Value)
                {
                    var engNode = new TreeNode(engineering.Name);
                    engNode.Tag = engineering;
                    dateNode.Nodes.Add(engNode);
                }
            }
            _treeView.ExpandAll();
        }
    }
}
