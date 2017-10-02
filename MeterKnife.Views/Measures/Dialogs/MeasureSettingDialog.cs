using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Events;
using NKnife.IoC;

namespace MeterKnife.Views.Measures.Dialogs
{
    public sealed partial class MeasureSettingDialog : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeasureSettingDialog>();

        private readonly List<int> _GpibList = new List<int>();

        public MeasureSettingDialog()
        {
            InitializeComponent();
            _GateWayModelComboBox.SelectedIndex = 0;
            _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AutoFindMeterCheckbox.CheckedChanged += (s, e) => _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AcceptButton.Click += OnAcceptButtonClick;
            _CancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void OnAcceptButtonClick(object s, EventArgs e)
        {
            var address = (short)_NumberBox.Value;
            if (_GpibList.Contains(address))
            {
                MessageBox.Show(this, "请重新输入GPIB地址，该地址已有仪器占用。", "重复的GPIB地址", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!_AutoFindMeterCheckbox.Checked)
            {
                if (string.IsNullOrEmpty(_MeterBrandComboBox.Text))
                {
                    MessageBox.Show(this, "请重新输入或选择仪器品牌。", "空置的仪器品牌", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(_MeterTypeComboBox.Text))
                {
                    MessageBox.Show(this, "请重新输入或选择仪器型号。", "空置的仪器型号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }
        
    }
}
