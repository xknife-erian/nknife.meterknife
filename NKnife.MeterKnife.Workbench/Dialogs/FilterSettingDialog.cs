using System;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public partial class FilterSettingDialog : SimpleForm
    {
        public FiguredDataFilter FiguredDataFilter { get; set; }

        public FilterSettingDialog()
        {
            InitializeComponent();
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            FiguredDataFilter = new FiguredDataFilter();
            FiguredDataFilter.InStatistical = _InStatisticalCheckBox.Checked;
            FiguredDataFilter.IsSave = _IsSaveCheckBox.Checked;
            FiguredDataFilter.Multiple = (uint) _NumericUpDown.Value;
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public void SetFilter(FiguredDataFilter filter)
        {
            _NumericUpDown.Value = filter.Multiple;
            _InStatisticalCheckBox.Checked = filter.InStatistical;
            _IsSaveCheckBox.Checked = filter.IsSave;
        }
    }
}
