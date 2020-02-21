using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Commands
{
    public partial class CommandEditorDialog : Form
    {
        private readonly IWorkbenchViewModel _workbench;
        private bool _timeoutContrilAlreadySet = false;

        public CommandEditorDialog(IWorkbenchViewModel viewModel)
        {
            _workbench = viewModel;
            InitializeComponent();
            InitizlizeLanguage();
            InitializeInstrumentControls();
            InitializeCommandTabControl();
            InitializeTimeControlGroup();
            InitializeLoopControlGroup();

            RespondToControlEvent();
            Shown+= OnShown;
        }

        private void InitizlizeLanguage()
        {
            this.Res(new Control[]
            {
                label1, label2, label3, label4, label5, label6, label8, label9,
                //
                _ConfirmButton, _CancelButton,
                //
                _ScpiTabPage, _CareTabPage,
                //
                _InfiniteLoopCheckBox, _WorkToFinishCheckBox,
                //
                _TimeGroupBox, _LoopGroupBox
            });
        }

        public ScpiCommand ScpiCommand { get; set; }

        private void InitializeInstrumentControls()
        {
            _InstrumentsComboBox.SelectedIndexChanged += (sender, args) =>
            {
                if (_InstrumentsComboBox.SelectedItem is Instrument inst)
                {
                    _InstrumentSCPIComboBox.Items.Clear();
                    _InstrumentSCPIComboBox.Items.AddRange(inst.ScpiList.Cast<object>().ToArray());
                }
            };
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

            var instArray = await _workbench.GetAllInstrumentAsync();
            if (instArray != null)
            {
                _InstrumentsComboBox.Items.AddRange(instArray.Cast<object>().ToArray());
                if (_InstrumentsComboBox.Items.Count > 0)
                    _InstrumentsComboBox.SelectedIndex = 0;
            }
        }

        private void RespondToControlEvent()
        {
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var right = VerifyControlValue();
            if (right)
            {
                if (_ScpiRadioButton.Checked)
                {
                    ScpiCommand = new ScpiCommand();
                    ScpiCommand.Scpi = _ScpiDetailPanel.Scpi;
                }
                else if (_CareRadioButton.Checked)
                {
                    ScpiCommand = new CareCommand();
                }

                //ScpiCommand.GpibAddress = (short) _GpibNumericUpDown.Value;
                ScpiCommand.Interval = (int)_IntervalNumericUpDown.Value;
                ScpiCommand.Timeout = (int)_TimeoutNumericUpDown.Value;
                if (_LoopCountNmericUpDown.Value == 1)
                {
                    ScpiCommand.IsLoop = false;
                }
                else
                {
                    ScpiCommand.IsLoop = true;
                    ScpiCommand.LoopCount = (int) _LoopCountNmericUpDown.Value;
                    ScpiCommand.IsPrecedenceWork = _WorkToFinishCheckBox.Checked;
                }

                ScpiCommand.Slot = (Slot)_SlotComboBox.SelectedItem;
                ScpiCommand.DUT = (DUT)_DUTComboBox.SelectedItem;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private bool VerifyControlValue()
        {
            return true;
        }
    }
}