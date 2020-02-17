using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    public partial class CareCommandEditorDialog : Form
    {
        private readonly IWorkbenchViewModel _workbench;
        private PoolCategory _category;
        private ScpiCommand _scpiCommand;
        private bool _timeoutContrilAlreadySet = false;


        public CareCommandEditorDialog(IWorkbenchViewModel viewModel)
        {
            _workbench = viewModel;
            InitializeComponent();
            InitizlizeLanguage();
            InitializeSlotGroup();
            InitializeDUTGroup();
            InitializeCommandTabControl();
            InitializeTimeControlGroup();
            InitializeLoopControlGroup();

            RespondToControlEvent();
            Shown+= OnShown;
        }

        private void InitizlizeLanguage()
        {
            _Label1.Text = this.Res(_Label1.Text);
            _Label2.Text = this.Res(_Label2.Text);
            _Label3.Text = this.Res(_Label3.Text);
            _Label4.Text = this.Res(_Label4.Text);
            _Label5.Text = this.Res(_Label5.Text);
            _Label6.Text = this.Res(_Label6.Text);
            _NewDUTButton.Text = this.Res(_NewDUTButton.Text);

        }

        public ScpiCommand ScpiCommand
        {
            get => _scpiCommand;
            set => _scpiCommand = value;
        }

        private void InitializeSlotGroup()
        {
        }

        private void InitializeDUTGroup()
        {
            throw new NotImplementedException();
        }

        private void InitializeCommandTabControl()
        {
            _CommandTabControl.SuspendLayout();
            _CommandTabControl.TabPages.Clear();
            _CommandTabControl.TabPages.Add(_ScpiTabPage);
            _CommandTabControl.ResumeLayout(false);
            _CommandTabControl.PerformLayout();
            _ScpiRadioButton.CheckedChanged += (sender, args) =>
            {
                _CommandTabControl.SuspendLayout();
                _CommandTabControl.TabPages.Clear();
                _CommandTabControl.TabPages.Add(_ScpiTabPage);
                _CommandTabControl.ResumeLayout(false);
                _CommandTabControl.PerformLayout();
            };
            _CareRadioButton.CheckedChanged += (sender, args) =>
            {
                _CommandTabControl.SuspendLayout();
                _CommandTabControl.TabPages.Clear();
                _CommandTabControl.TabPages.Add(_CareTabPage);
                _CommandTabControl.ResumeLayout(false);
                _CommandTabControl.PerformLayout();
            };
            _HexEnableCheckBox.CheckedChanged += (sender, args) =>
            {
                if (_HexEnableCheckBox.Checked)
                {
                    var t = _CommandTextBox.Text;
                    _CommandTextBox.Text = Encoding.ASCII.GetBytes(t).ToHexString();
                }
                else
                {
                    var t = _CommandTextBox.Text;
                    var bs = UtilByte.ConvertToBytes(t);
                    _CommandTextBox.Text = Encoding.ASCII.GetString(bs);
                }
                _CommandTextBox.Focus();
            };
        }

        private void InitializeTimeControlGroup()
        {
            _TimeoutNumericUpDown.Maximum = _IntervalNumericUpDown.Maximum * 2 + 1;
            _IntervalNumericUpDown.LostFocus += (sender, args) =>
            {
                if (!_timeoutContrilAlreadySet)
                    _TimeoutNumericUpDown.Value = _IntervalNumericUpDown.Value * 2;
            };
            _TimeoutNumericUpDown.LostFocus += (sender, args) =>
            {
                _timeoutContrilAlreadySet = true;
                if (_TimeoutNumericUpDown.Value < _IntervalNumericUpDown.Value)
                {
                    MessageBox.Show(this.Res("“等待超时”的时长不能小于“等待定时”的时长。"), this.Res("时长设置"),
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _TimeoutNumericUpDown.Focus();
                }
            };
        }

        private void InitializeLoopControlGroup()
        {
        }

        private async void OnShown(object sender, EventArgs e)
        {
            var slotArray = await _workbench.GetAllSlotAsync();
            if (slotArray != null)
            {
                _SlotComboBox.Items.AddRange(slotArray.Cast<object>().ToArray());
                if (_SlotComboBox.Items.Count > 0)
                    _SlotComboBox.SelectedIndex = 0;
            }

            var dutArray = await _workbench.GetAllDUTAsync();
            if (dutArray != null)
            {
                _DUTComboBox.Items.AddRange(dutArray.Cast<object>().ToArray());
                if (_DUTComboBox.Items.Count > 0)
                    _DUTComboBox.SelectedIndex = 0;
            }
        }

        private void RespondToControlEvent()
        {
        }

        public PoolCategory Category
        {
            get => _category;
            set
            {
                _category = value;
                switch (value)
                {
                    case PoolCategory.Collect:
                        _IntervalNumericUpDown.Value = 400;
                        break;
                    case PoolCategory.Initializtion:
                        _IntervalNumericUpDown.Value = 50;
                        break;
                }
            }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            var right = VerifyControlValue();
            if (right)
            {
                if (_ScpiRadioButton.Checked)
                {
                    _scpiCommand = new ScpiCommand();
                    var scpi = new Scpi();
                    scpi.Name = _ScpiNameTextBox.Text;
                    scpi.IsHex = _HexEnableCheckBox.Checked;
                    scpi.Command = _CommandTextBox.Text;
                    scpi.Description = _ScpiDescriptionTextBox.Text;
                    _scpiCommand.Scpi = scpi;
                }
                else if (_CareRadioButton.Checked)
                {
                    _scpiCommand = new CareCommand();
                }

                _scpiCommand.GpibAddress = (short) _GpibNumericUpDown.Value;
                _scpiCommand.Interval = (int)_IntervalNumericUpDown.Value;
                _scpiCommand.Timeout = (int)_TimeoutNumericUpDown.Value;
                if (_LoopCountNmericUpDown.Value == 1)
                {
                    _scpiCommand.IsLoop = false;
                }
                else
                {
                    _scpiCommand.IsLoop = true;
                    _scpiCommand.LoopCount = (int) _LoopCountNmericUpDown.Value;
                    _scpiCommand.IsPrecedenceWork = _WorkToFinishCheckBox.Checked;
                }

                _scpiCommand.Slot = new Slot();
                _scpiCommand.DUT = (DUT)_DUTComboBox.SelectedItem;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private bool VerifyControlValue()
        {
            throw new NotImplementedException();
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}