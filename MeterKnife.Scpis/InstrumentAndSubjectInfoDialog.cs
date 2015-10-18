using System;
using System.Windows.Forms;
using NKnife.GUI.WinForm;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class InstrumentAndSubjectInfoDialog : SimpleForm
    {
        public InstrumentAndSubjectInfoDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var list = DI.Get<IScpiInfoGetter>().GetMeterInfoList();
        }

        public ScpiSubjectCollection ScpiSubjectCollection { get; set; }

        public string InstBrand
        {
            get { return _BrandComboBox.Text; }
        }

        public string InstName
        {
            get { return _NameComboBox.Text; }
        }

        public string InstDescription
        {
            get { return _DescriptionTextBox.Text; }
        }

        public string GroupName
        {
            get { return _GroupNameTextBox.Text; }
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
            if (string.IsNullOrEmpty(_GroupNameTextBox.Text))
            {
                MessageBox.Show(this, "请为当前的指令集命名", "指令集命名", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}