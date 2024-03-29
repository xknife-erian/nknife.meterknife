﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    public partial class InstrumentDetailDialog : Form
    {
        private readonly ICloudDataService _cloudDataService;
        private readonly IDialogProvider _dialogProvider;
        private Instrument _instrument;

        public InstrumentDetailDialog(ICloudDataService cloudDataService, IDialogProvider dialogProvider)
        {
            _cloudDataService = cloudDataService;
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeLanguage();
            RespondToMenuButtonClick();
            RespondToControlEvent();

            RespontToScpiInputEvent();
            RespondToScpiTreeViewEvent();
        }

        public Instrument Instrument
        {
            get
            {
                if (_instrument == null)
                    _instrument = new Instrument();
                _instrument.CreateTime = DateTime.Now; 
                _instrument.Id = _NumberTextBox.Text;
                _instrument.Manufacturer = _ManufacturerComboBox.Text;
                _instrument.UseClassification = _UseClassificationComboBox.Text;
                _instrument.Model1 = _ModelTextBox.Text;
                _instrument.Model2 = _SubModelTextBox.Text;
                _instrument.Description = _DescriptionTextBox.Text;
                _instrument.GPIBAddress = (short) _GPIBNumericUpDown.Value;
                _instrument.IPAddress = _IpAddressControl.GetAddressBytes();
                _instrument.PurchasingDate = _PurchasingDatePicker.Value;
                _instrument.ScpiList = GetScpiList();
                //下述路径，当按确定键后会被创建。
                _instrument.PhotosPath = _PhotosPathToolStripStatusLabel.Text;
                _instrument.FilesPath = _FilesPathToolStripStatusLabel.Text;
                _instrument.Name = $"{_ManufacturerComboBox.Text}/{_ModelTextBox.Text}/{_SubModelTextBox.Text}".TrimEnd('/');
                return _instrument;
            }
            set
            {
                _instrument = value;
                _NumberTextBox.Text = value.Id;
                _ManufacturerComboBox.Text = value.Manufacturer;
                _UseClassificationComboBox.Text = value.UseClassification;
                _ModelTextBox.Text = _instrument.Model1;
                _SubModelTextBox.Text = _instrument.Model2;
                _DescriptionTextBox.Text = _instrument.Description;
                _GPIBNumericUpDown.Value = _instrument.GPIBAddress;
                _IpAddressControl.SetAddressBytes(_instrument.IPAddress);
                _PhotosPathToolStripStatusLabel.Text = value.PhotosPath;
                _FilesPathToolStripStatusLabel.Text = value.FilesPath;
                _PurchasingDatePicker.Value = value.PurchasingDate;
                InitializeScpiList(value.ScpiList);
            }
        }

        private void InitializeScpiList(List<SCPI> scpiList)
        {
            if (scpiList == null)
                return;
            foreach (var scpi in scpiList)
            {
                var node = new ScpiTreeNode(scpi);
                _ScpiTreeView.Nodes.Add(node);
            }
        }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, _AutoNumberButton, _GetInstrumentScpiTemplateButton);
            this.Res(tabPage1, tabPage2, tabPage3, tabPage4);
            this.Res(label1, label2, label3, label4, label5, label6, label7, label8, label9, label10);
            this.Res(_AddScpiToolStripButton, _DeleteScpiToolStripButton);
            this.Res(_AddPhotoToolStripButton, _DeletePhotoToolStripButton);
            this.Res(_AddFileToolStripButton, _DeleteFileToolStripButton);
            this.Res(_ScpiInfoGroupBox, _ScpiGroupBox, _ScpiTestButton);

            _AddScpiToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteScpiToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _AddPhotoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeletePhotoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _AddFileToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteFileToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;

            _AddScpiToolStripButton.Image = MenuButtonResource.base_add_24px;
            _DeleteScpiToolStripButton.Image = MenuButtonResource.base_delete_24px;
            _AddPhotoToolStripButton.Image = MenuButtonResource.base_add_24px;
            _DeletePhotoToolStripButton.Image = MenuButtonResource.base_delete_24px;
            _AddFileToolStripButton.Image = MenuButtonResource.base_add_24px;
            _DeleteFileToolStripButton.Image = MenuButtonResource.base_delete_24px;

            _PurchasingDatePicker.Value = DateTime.Now;
        }

        private void RespondToControlEvent()
        {
            _ManufacturerComboBox.LostFocus += ControlOnLostFocus;
            _ModelTextBox.LostFocus += ControlOnLostFocus;
            _SubModelTextBox.LostFocus += ControlOnLostFocus;
            _ScpiTestButton.Click += delegate
            {
                var testDialog = _dialogProvider.New<ScpiDebugDialog>();
                testDialog.Height = this.Height;
                var x = this.Location.X + this.Width;
                var y = this.Location.Y;
                testDialog.Location = new Point(x,y);
                testDialog.Show();
            };
            _AutoNumberButton.Click += (sender, args) =>
            {
                var rand = UtilRandom.GetString(1, UtilRandom.RandomCharType.Uppercased);
                _NumberTextBox.Text = $"T{DateTime.Now:yyMMddHHmmss}{rand}";
            };
            _CancelButton.Click += (sender, args) => { DialogResult = DialogResult.Cancel; };
            _AcceptButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }

        private async void ControlOnLostFocus(object sender, EventArgs e)
        {
            if (_ManufacturerComboBox.IsEmptyText())
                return;
            if (_ModelTextBox.IsEmptyText())
                return;
            var count = await _cloudDataService.GetInstrumentScpiTemplate(_ManufacturerComboBox.Text, _ModelTextBox.Text, _SubModelTextBox.Text);
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
                var preset = this.Res("新指令");
                var newName = preset;
                var i = 1;
                while (HasSameName(newName))
                {
                    newName = $"{preset}{i}";
                    i++;
                }

                var node = new ScpiTreeNode(new SCPI {Name = newName});
                _ScpiTreeView.Nodes.Add(node);
                _ScpiTreeView.SelectedNode = node;
                _ScpiNameTextBox.Focus();
                node.EnsureVisible();
            };
        }

        private bool HasSameName(string name)
        {
            foreach (TreeNode treeNode in _ScpiTreeView.Nodes)
                if (treeNode.Text.Equals(name))
                    return true;
            return false;
        }

        private void RespontToScpiInputEvent()
        {
            _ScpiNameTextBox.LostFocus += (sender, args) =>
            {
                if (!VerifyScpiNameInput(_ScpiNameTextBox.Text, out var msg))
                {
                    MessageBox.Show(msg, "输入有误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _ScpiNameTextBox.Focus();
                    return;
                }

                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node)
                {
                    node.SCPI.Name = _ScpiNameTextBox.Text;
                    node.Text = _ScpiNameTextBox.Text;
                }
            };
            _ScpiCommandTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node) node.SCPI.Command = _ScpiCommandTextBox.Text.Trim();
            };
            _ScpiDescriptionTextBox.LostFocus += (sender, args) =>
            {
                if (_ScpiTreeView.SelectedNode is ScpiTreeNode node) node.SCPI.Description = _ScpiDescriptionTextBox.Text;
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
                    if (scpiNode.SCPI.Name == name)
                    {
                        msg = this.Res("SCPI的名称有重复，请重新输入。");
                        return false;
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
                    _ScpiNameTextBox.Text = node.SCPI.Name;
                    _ScpiCommandTextBox.Text = node.SCPI.Command;
                    _ScpiDescriptionTextBox.Text = node.SCPI.Description;
                }
            };
            _ScpiTreeView.AfterSelect += (sender, args) =>
            {
                if (args.Node is ScpiTreeNode node)
                {
                    _ScpiNameTextBox.Text = node.SCPI.Name;
                    _ScpiCommandTextBox.Text = node.SCPI.Command;
                    _ScpiDescriptionTextBox.Text = node.SCPI.Description;
                }

                _DeleteScpiToolStripButton.Enabled = true;
            };
        }

        private List<SCPI> GetScpiList()
        {
            return (from ScpiTreeNode node in _ScpiTreeView.Nodes select node.SCPI).ToList();
        }

        private class ScpiTreeNode : TreeNode
        {
            private SCPI _scpi;

            public ScpiTreeNode(SCPI scpi)
            {
                _scpi = scpi;
                Text = scpi.Name;
            }

            public SCPI SCPI
            {
                get
                {
                    if (_scpi == null)
                        return _scpi = new SCPI {Name = Text};
                    _scpi.Name = Text;
                    return _scpi;
                }
            }
        }
    }
}