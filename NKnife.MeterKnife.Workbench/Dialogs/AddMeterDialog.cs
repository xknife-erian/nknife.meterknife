using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public partial class AddMeterDialog : Form
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);


        public AddMeterDialog()//BaseCareCommunicationService commService)
        {
            GpibList = new List<int>();
            InitializeComponent();

            _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            _AutoFindMeterCheckbox.CheckedChanged += (s, e) => _MeterTypeGroupBox.Enabled = !_AutoFindMeterCheckbox.Checked;
            //_AcceptButton.Click += OnAcceptButtonClick;
            _CancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        public List<int> GpibList { get; set; }
        public int GpibAddress => (int) _NumberBox.Value;

        /*
        protected virtual void OnAcceptButtonClick(object s, EventArgs e)
        {
            var address = (short)_NumberBox.Value;
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

                var idn = CommandUtil.IDN(address);
                _logger.Debug(string.Format("Send:{0}", idn.GenerateCareProtocol(0)));
                var careItem = new ScpiCommandQueue.Item
                {
                    ScpiCommandPanel = idn,
                    GpibAddress = address
                };
                _CommService.SendCommands(Port, careItem); //08 17 07 AA 00 2A 49 44 4E 3F
                _AutoResetEvent.WaitOne(5000);
            }
            else //当手动选择仪器类型时
            {
                Meter = DI.Get<BaseMeter>("DigitalMultimeter".ToLower());
                Meter.Brand = _MeterBrandComboBox.Text;
                Meter.GpibAddress = address;
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
                Meter.Brand = simpleName.Item1;
            }
            catch (Exception ex)
            {
                _logger.WarnFormat("未找到{0}的仪器", idnName, ex.Message);
                Meter = DI.Get<BaseMeter>();
                Meter.Name = string.IsNullOrEmpty(idnName) ? "Unknown Meter" : idnName;
            }
            Meter.GpibAddress = (int)_NumberBox.Value;

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
        */
    }
}