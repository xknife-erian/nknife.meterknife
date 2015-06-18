using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Workbench.Dialogs;
using NKnife.IoC;

namespace MeterKnife.Fairy
{
    internal class AddFairyMeterDialog : AddGpibMeterDialog
    {
        public AddFairyMeterDialog()
        {
            _AutoFindMeterCheckbox.Visible = false;
        }

        protected override void OnAcceptButtonClick(object s, EventArgs e)
        {
            var address = (int) _NumberBox.Value;
            if (GpibList.Contains(address))
            {
                MessageBox.Show(this, "请重新输入GPIB地址，该地址已有仪器占用。", "重复的GPIB地址", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Meter = DI.Get<BaseMeter>();
            if (string.IsNullOrEmpty(_MeterTypeComboBox.Text))
                Meter.Name = string.Format("仪器{0}", address);
            if (string.IsNullOrEmpty(_MeterBrandComboBox.Text))
                Meter.Brand = string.Format("UnKnowBrand");
            Meter.GpibAddress = address;
            Meter.Language = Language;
            DialogResult = DialogResult.OK;
        }
    }
}
