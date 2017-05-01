using System;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Scpis
{
    public partial class InstrumentInfoDialog : SimpleForm
    {
        public InstrumentInfoDialog()
        {
            InitializeComponent();
        }

        public ScpiSubjectCollection ScpiSubjectCollection { get; set; }

        public string InstBrand
        {
            get { return _BrandComboBox.Text; }
            set { _BrandComboBox.Text = value; }
        }

        public string InstName
        {
            get { return _NameComboBox.Text; }
            set { _NameComboBox.Text = value; }
        }

        public string InstDescription
        {
            get { return _DescriptionTextBox.Text; }
            set { _DescriptionTextBox.Text = value; }
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_BrandComboBox.Text))
            {
                MessageBox.Show(this, "请输入仪器的品牌", "填写品牌", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(_NameComboBox.Text))
            {
                MessageBox.Show(this, "请输入仪器的型号", "填写型号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}