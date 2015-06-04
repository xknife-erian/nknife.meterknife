using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using MeterKnife.Workbench.Controls.Tree;
using NKnife.Events;
using NKnife.IoC;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class AddGpibMeterDialog : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger<AddGpibMeterDialog>();

        protected readonly BaseCareCommunicationService _CommService = DI.Get<BaseCareCommunicationService>();
        private readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);

        public AddGpibMeterDialog()
        {
            GpibList = new List<int>();
            InitializeComponent();

            _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AutoFindMeterCheckbox.CheckedChanged += (s, e) => _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AcceptButton.Click += OnAcceptButtonClick;
            _CancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void OnAcceptButtonClick(object s, EventArgs e)
        {
            var address = (int) _NumberBox.Value;
            if (GpibList.Contains(address))
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
            var handler = new CareConfigHandler();
            if (_AutoFindMeterCheckbox.Checked)
            {
                Cursor = Cursors.WaitCursor;

                _CommService.Bind(Port, handler);
                handler.CareConfigging += OnCareConfigging;

                var careTalking = CareTalking.IDN(address);
                _logger.Debug(string.Format("Send:{0}", careTalking.Scpi));
                var data = careTalking.Generate();
                _logger.Trace(data.ToHexString());
                _CommService.Send(Port, data); //08 17 07 AA 00 2A 49 44 4E 3F
                _AutoResetEvent.WaitOne(5000);
            }
            else //当手动选择仪器类型时
            {
                Meter = DI.Get<BaseMeter>();
                Meter.Brand = _MeterBrandComboBox.Text;
                Meter.GpibAddress = address;
                Meter.Language = Language;
                Meter.Name = string.Format("{0}", _MeterTypeComboBox.Text);
            }
            if (Meter == null)
            {
                Meter = DI.Get<BaseMeter>();
                Meter.Name = "Unknown Meter";
                _CommService.Remove(Port, handler);
            }
            DialogResult = DialogResult.OK;
        }

        public List<int> GpibList { get; set; }
        public int Port { get; set; }
        public int GpibAddress { get { return (int) _NumberBox.Value; } }
        public BaseMeter Meter { get; set; }

        private void OnCareConfigging(object sender, EventArgs<CareTalking> e)
        {
            var idnName = e.Item.Scpi.Trim();

            var simpleName = MeterUtil.SimplifyName(idnName);
            var meterName = MeterUtil.Named(simpleName);
            _logger.Info(string.Format("准备创建仪器:{0}", meterName));
            try
            {
                Meter = DI.Get<BaseMeter>(meterName);
                Meter.Name = idnName;
                Meter.Brand = simpleName.First;
            }
            catch (Exception ex)
            {
                _logger.WarnFormat("未找到{0}的仪器", idnName, ex.Message);
                Meter = DI.Get<BaseMeter>();
                Meter.Name = string.IsNullOrEmpty(idnName) ? "Unknown Meter" : idnName;
            }
            Meter.GpibAddress = (int)_NumberBox.Value;
            Meter.Language = Language;

            var handler = (CareConfigHandler)sender;
            handler.CareConfigging -= OnCareConfigging;
            _CommService.Remove(Port, handler);

            _AutoResetEvent.Set();
        }

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
