using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using MeterKnife.Workbench.Dialogs;
using NKnife.Events;
using NKnife.IoC;

namespace MeterKnife.Lite
{
    public class AddMeterLiteDialog : AddMeterDialog
    {
        private static readonly ILog _logger = LogManager.GetLogger<AddMeterDialog>();

        public AddMeterLiteDialog()
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
            Meter.Brand = string.IsNullOrEmpty(_MeterBrandComboBox.Text) ? "Unknown brand" : _MeterBrandComboBox.Text;

            Meter.Name = string.IsNullOrEmpty(_MeterTypeComboBox.Text)
                ? string.Format("仪器{0}", address)
                : string.Format("{0}", _MeterTypeComboBox.Text);
            Meter.GpibAddress = address;
            Meter.Language = Language;
            DialogResult = DialogResult.OK;
        }

    }
}
