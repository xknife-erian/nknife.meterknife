using System;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;

namespace MeterKnife.Common.Winforms.Dialogs
{
    public class AddMeterLiteDialog : AddMeterDialog
    {
        private static readonly ILog _logger = LogManager.GetLogger<AddMeterDialog>();

        private readonly BaseCareCommunicationService _commService;

        public AddMeterLiteDialog(BaseCareCommunicationService commService)
            : base(commService)
        {
            _AutoFindMeterCheckbox.Visible = false;
        }

        /*
        protected override void OnAcceptButtonClick(object s, EventArgs e)
        {
            var address = (int) _NumberBox.Value;
            if (GpibList.Contains(address))
            {
                MessageBox.Show(this, "请重新输入GPIB地址，该地址已有仪器占用。", "重复的GPIB地址", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Meter = DI.Get<BaseMeter>("DigitalMultimeter".ToLower());
            Meter.Brand = string.IsNullOrEmpty(_MeterBrandComboBox.Text) ? "Unknown brand" : _MeterBrandComboBox.Text;

            Meter.Name = string.IsNullOrEmpty(_MeterTypeComboBox.Text)
                ? string.Format("仪器{0}", address)
                : string.Format("{0}", _MeterTypeComboBox.Text);
            Meter.GpibAddress = address;
            DialogResult = DialogResult.OK;
        }
        */
    }
}
