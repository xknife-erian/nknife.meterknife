using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Resources;

namespace NKnife.MeterKnife.Workbench.Dialogs.DUTs
{
    public partial class DUTDetailDialog : Form
    {
        public DUTDetailDialog()
        {
            InitializeComponent();
            InitializeLanguage();
            RespondToButtonClick();
        }

        public DUT DUT { get; set; }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, _AutoNumberButton);
            this.Res(tabPage1, tabPage2, tabPage3, tabPage4);
            this.Res(label1, label2, label3, label4, label5, label6);
            this.Res(columnHeader1, columnHeader2, columnHeader3, columnHeader4);

            this.Res(_AddMetrologyValueToolStripButton, _EditMetrologyValueToolStripButton, _DeleteMetrologyValueToolStripButton);
            _AddMetrologyValueToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditMetrologyValueToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteMetrologyValueToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _AddMetrologyValueToolStripButton.Image = MenuButtonResource.base_add_24px;
            _EditMetrologyValueToolStripButton.Image = MenuButtonResource.base_edit_24px;
            _DeleteMetrologyValueToolStripButton.Image = MenuButtonResource.base_delete_24px;

            this.Res(_AddPhotoToolStripButton, _DeletePhotoToolStripButton);
            _AddPhotoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeletePhotoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _AddPhotoToolStripButton.Image = MenuButtonResource.base_add_24px;
            _DeletePhotoToolStripButton.Image = MenuButtonResource.base_delete_24px;

            this.Res(_AddFileToolStripButton, _DeleteFileToolStripButton);
            _AddFileToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteFileToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _AddFileToolStripButton.Image = MenuButtonResource.base_add_24px;
            _DeleteFileToolStripButton.Image = MenuButtonResource.base_delete_24px;
        }

        private void RespondToButtonClick()
        {
            _AutoNumberButton.Click += (sender, args) => { _NumberTextBox.Text = SequentialGuid.Create().ToString("D").ToUpper(); };
            _CancelButton.Click += (sender, args) => { DialogResult = DialogResult.Cancel; };
            _AcceptButton.Click += (sender, args) =>
            {
                if (DUT == null)
                    DUT = new DUT();
                DUT.Id = _NumberTextBox.Text;
                DUT.CreateTime = DateTime.Now;
                DUT.Name = _NameTextBox.Text;
                if (!_ExpectValueTextBox.IsEmptyText())
                    DUT.ExpectValue = double.Parse(_ExpectValueTextBox.Text);
                DUT.Classify = _ClassifyComboBox.Text;
                //DUT.Unit = _UnitComboBox.Text.ToString();
                DUT.Description = _DescriptionTextBox.Text;
                DUT.MetrologyValues = GetMetrologyValues();
                DUT.PhotosPath = _PhotoListView.Text;
                DUT.FilesPath = _FilesListView.Text;
                DialogResult = DialogResult.OK;
                Close();
            };
        }

        private MetrologyValue[] GetMetrologyValues()
        {
            return new MetrologyValue[0];
        }
    }
}
