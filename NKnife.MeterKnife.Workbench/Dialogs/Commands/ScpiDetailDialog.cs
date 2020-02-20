using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;

namespace NKnife.MeterKnife.Workbench.Dialogs.Commands
{
    public partial class ScpiDetailDialog : Form
    {
        private List<Scpi> _scpis;

        public ScpiDetailDialog()
        {
            InitializeComponent();
            InitializeLanguage();
            RespondToMenuClick();
            RespondToTreeViewEvent();
            RespontToInputEvent();
        }

        public List<Scpi> Scpis
        {
            get
            {
                if(_scpis==null)
                    _scpis = new List<Scpi>();
                _scpis.Clear();
                foreach (TreeNode node in _ScpiTreeView.Nodes)
                {
                    var scpiNode = node as ScpiTreeNode;
                    _scpis.Add(scpiNode?.Scpi);
                }
                return _scpis;
            }
            set
            {
                _scpis = value;
                foreach (var scpi in _scpis)
                {
                    var node = new ScpiTreeNode(scpi);
                    _ScpiTreeView.Nodes.Add(node);
                }
            }
        }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, label1, label2, label3, label4);
            this.Res(_AddToolStripButton, _DeleteToolStripButton);
        }

        private void RespondToTreeViewEvent()
        {
            _ScpiTreeView.AfterLabelEdit += (sender, args) =>
            {
                if (args.Node is ScpiTreeNode node)
                {
                    _ScpiNameTextBox.Text = node.Scpi.Name;
                    _CommandTextBox.Text = node.Scpi.Command;
                    _ScpiDescriptionTextBox.Text = node.Scpi.Description;
                }
            };
            _ScpiTreeView.AfterSelect += (sender, args) =>
            {
                if (args.Node is ScpiTreeNode node)
                {
                    _ScpiNameTextBox.Text = node.Scpi.Name;
                    _CommandTextBox.Text = node.Scpi.Command;
                    _ScpiDescriptionTextBox.Text = node.Scpi.Description;
                }

                _DeleteToolStripButton.Enabled = true;
            };
        }

        private void RespontToInputEvent()
        {
            _ScpiNameTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.Scpi.Name = _ScpiNameTextBox.Text;
                    node.Text = _ScpiNameTextBox.Text;
                }
            }; 
            _CommandTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.Scpi.Command = _CommandTextBox.Text.Trim();
                }
            }; 
            _ScpiDescriptionTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.Scpi.Description = _ScpiDescriptionTextBox.Text;
                }
            };
        }

        private void RespondToMenuClick()
        {
            _AddToolStripButton.Click += (sender, args) =>
            {
                var node = new ScpiTreeNode(new Scpi() {Name = this.Res("新指令")});
                _ScpiTreeView.Nodes.Add(node);
                //node.BeginEdit();
                _ScpiTreeView.SelectedNode = node;
                node.EnsureVisible();
                _ScpiNameTextBox.Focus();
            };
        }

        class ScpiTreeNode : TreeNode
        {
            private Scpi _scpi;

            public ScpiTreeNode(Scpi scpi)
            {
                _scpi = scpi;
                Text = scpi.Name;
            }

            public Scpi Scpi
            {
                get
                {
                    if (_scpi == null) 
                        return _scpi = new Scpi {Name = Text};
                    _scpi.Name = Text;
                    return _scpi;
                }
            }
        }
    }
}
