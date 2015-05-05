using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Base;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class AddGpibMeterDialog : Form
    {
        public AddGpibMeterDialog()
        {
            InitializeComponent();
            _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AutoFindMeterCheckbox.CheckedChanged += (s, e) => _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AcceptButton.Click += (s, e) => DialogResult = DialogResult.OK;
            _CancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        public int GpibAddress{get { return (int)_NumberBox.Value; }}

        public bool AutoFindMeter { get { return _AutoFindMeterCheckbox.Checked; } }
        public string Brand { get { return _MeterBrandComboBox.Text; } }
        public string MeterName { get { return _MeterTypeComboBox.Text; } }

        public GpibLanguage Language
        {
            get
            {
                if(_StandardScpiRadiobox.Checked)
                    return GpibLanguage.SCPI;
                if (_AdvantestRadiobox.Checked)
                    return GpibLanguage.Advantest;
                if (_HP3478RadioBox.Checked)
                    return GpibLanguage.HP3478;
                if (_Fluke8840Radiobox.Checked)
                    return GpibLanguage.Fluke8840;
                return GpibLanguage.SCPI;
            }
        }
    }
}
