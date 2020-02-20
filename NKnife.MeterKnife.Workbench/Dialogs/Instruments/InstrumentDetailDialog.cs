using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    public partial class InstrumentDetailDialog : Form
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly ICloudDataService _cloudDataService;

        public InstrumentDetailDialog(IDialogProvider dialogProvider, ICloudDataService cloudDataService)
        {
            _dialogProvider = dialogProvider;
            _cloudDataService = cloudDataService;
            InitializeComponent();
            InitializeLanguage();
            RespondToMenuButtonClick();
            RespondToControlEvent();

            RespontToScpiInputEvent();
            RespondToScpiTreeViewEvent();
        }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, _AutoNumberButton);
            this.Res(tabPage1, tabPage2, tabPage3, tabPage4);
            this.Res(label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12);
            this.Res(_AddScpiToolStripButton, _DeleteScpiToolStripButton);
            this.Res(_AddPhotoToolStripButton, _DeletePhotoToolStripButton);
            this.Res(_AddFileToolStripButton, _DeleteFileToolStripButton);
        }

        private void RespondToControlEvent()
        {
            _ManufacturerComboBox.LostFocus+= ControlOnLostFocus;
            _ModelTextBox.LostFocus += ControlOnLostFocus;
            _SubModelTextBox.LostFocus += ControlOnLostFocus;
        }

        private async void ControlOnLostFocus(object sender, EventArgs e)
        {
            if (_ManufacturerComboBox.IsEmptyText())
                return;
            if (_ModelTextBox.IsEmptyText())
                return;
            int count = await _cloudDataService.GetInstrumentScpiTemplate(_ManufacturerComboBox.Text, _ModelTextBox.Text, _SubModelTextBox.Text);
            if (count > 0)
            {
                _NetInfoLabel.ForeColor = SystemColors.ControlText;
                _NetInfoLabel.Text = this.ResF("云端有{0}个指令模板...", count);
                _GetInstrumentScpiTemplateButton.Enabled = true;
            }
        }

        private void RespondToMenuButtonClick()
        {
            _AddScpiToolStripButton.Click += (sender, args) =>
            {
                var node = new ScpiTreeNode(new Scpi() { Name = this.Res("新指令") });
                _ScpiTreeView.Nodes.Add(node);
                //node.BeginEdit();
                _ScpiTreeView.SelectedNode = node;
                node.EnsureVisible();
                _ScpiNameTextBox.Focus();
            };
        }
        private void RespontToScpiInputEvent()
        {
            _ScpiNameTextBox.LostFocus += (sender, args) =>
            {
                if (!VerifyScpiNameInput(_ScpiNameTextBox.Text, out string msg))
                {
                    MessageBox.Show(msg, "输入有误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _ScpiNameTextBox.Focus();
                    return;
                }

                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.Scpi.Name = _ScpiNameTextBox.Text;
                    node.Text = _ScpiNameTextBox.Text;
                }
            };
            _ScpiCommandTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.Scpi.Command = _ScpiCommandTextBox.Text.Trim();
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

        private bool VerifyScpiNameInput(string name, out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                msg = this.Res("SCPI指令必须录入名称，请输入。");
                return false;
            }

            foreach (TreeNode node in _ScpiTreeView.Nodes)
            {
                if (_ScpiTreeView.SelectedNode.Equals(node))
                    continue;
                if (node is ScpiTreeNode scpiNode)
                {
                    if (scpiNode.Scpi.Name == name)
                    {
                        msg = this.Res("SCPI的名称有重复，请重新输入。");
                        return false;
                    }
                }
            }

            return true;
        }

        private void RespondToScpiTreeViewEvent()
        {
            _ScpiTreeView.AfterLabelEdit += (sender, args) =>
            {
                if (args.Node is ScpiTreeNode node)
                {
                    _ScpiNameTextBox.Text = node.Scpi.Name;
                    _ScpiCommandTextBox.Text = node.Scpi.Command;
                    _ScpiDescriptionTextBox.Text = node.Scpi.Description;
                }
            };
            _ScpiTreeView.AfterSelect += (sender, args) =>
            {
                if (args.Node is ScpiTreeNode node)
                {
                    _ScpiNameTextBox.Text = node.Scpi.Name;
                    _ScpiCommandTextBox.Text = node.Scpi.Command;
                    _ScpiDescriptionTextBox.Text = node.Scpi.Description;
                }

                _DeleteScpiToolStripButton.Enabled = true;
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
                        return _scpi = new Scpi { Name = Text };
                    _scpi.Name = Text;
                    return _scpi;
                }
            }
        }
    }
}
