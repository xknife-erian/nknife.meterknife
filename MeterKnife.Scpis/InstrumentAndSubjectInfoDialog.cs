using System;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using NKnife.GUI.WinForm;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class InstrumentAndSubjectInfoDialog : SimpleForm
    {
        private readonly BrandCollection _BrandCollection;

        public InstrumentAndSubjectInfoDialog()
        {
            InitializeComponent();
            var list = DI.Get<IScpiInfoGetter>().GetMeterInfoList();
            _BrandCollection = new BrandCollection(list);

            foreach (var brand in _BrandCollection.Brands)
                _BrandComboBox.Items.Add(brand);

            _BrandComboBox.SelectedIndex = 0;
            UpdateNameAndDescription();

            _BrandComboBox.SelectedIndexChanged += _BrandComboBox_SelectedIndexChanged;
            _NameComboBox.SelectedIndexChanged += _NameComboBox_SelectedIndexChanged;
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

        public void Initialize(string brand, string name, string description)
        {
            var i = _BrandComboBox.Items.IndexOf(brand);
            if (i >= 0)
                _BrandComboBox.SelectedIndex = i;
            i = _NameComboBox.Items.IndexOf(name);
            if (i >= 0)
                _NameComboBox.Text = name;
        }

        private void _BrandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNameAndDescription();
        }

        private void UpdateNameAndDescription()
        {
            _NameComboBox.Items.Clear();
            var names = _BrandCollection.ByBrand(_BrandComboBox.SelectedItem.ToString());
            if (names != null && names.Count > 0)
            {
                foreach (var name in names)
                    _NameComboBox.Items.Add(name.Item2);
                _NameComboBox.SelectedIndex = 0;
                _DescriptionTextBox.Text = names[0].Item3;
            }
            _NameComboBox.Update();
        }

        private void _NameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var t = _BrandCollection.ByBrandAndName(_BrandComboBox.Text, _NameComboBox.Text);
            if (t != null)
                _DescriptionTextBox.Text = t.Item3;
            else
                _DescriptionTextBox.Clear();
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
                MessageBox.Show(this, "请为当前的功能主题命名", "功能主题命名", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}