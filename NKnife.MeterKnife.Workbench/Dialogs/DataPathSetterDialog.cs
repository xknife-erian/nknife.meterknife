﻿using System;
using System.IO;
using System.Windows.Forms;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public partial class DataPathSetterDialog : SimpleForm
    {
        public DataPathSetterDialog()
        {
            InitializeComponent();
            InitializeLanguage();
            if (Directory.Exists(DataPath))
                _PathTextbox.Text = DataPath;
        }

        private void InitializeLanguage()
        {
            this.Res(this, groupBox1, label1, _CancelButton, _ConfirmButton, _PathSelectButton);
        }

        public string DataPath { get; private set; }

        private void _PathSelectButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _PathTextbox.Text = dialog.SelectedPath;
            }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_PathTextbox.Text))
            {
                MessageBox.Show(this, "请选择正确的数据存储路径.");
                return;
            }
            DataPath = _PathTextbox.Text;
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
