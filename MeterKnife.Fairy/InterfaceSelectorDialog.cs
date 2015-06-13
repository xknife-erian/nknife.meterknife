using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;
using NKnife.Utility;

namespace MeterKnife.Fairy
{
    public partial class InterfaceSelectorDialog : SimpleForm
    {
        public InterfaceSelectorDialog()
        {
            InitializeComponent();
            FillSerialComonbox();
            _SerialRadioButton.CheckedChanged += (s, e) =>
            {
                _SerialComboBox.Enabled = _SerialRadioButton.Checked;
                _IpAddressControl.Enabled = !_SerialRadioButton.Checked;
                _PortNumericUpDown.Enabled = !_SerialRadioButton.Checked;
            };
            _LanRadioButton.CheckedChanged += (s, e) =>
            {
                _SerialComboBox.Enabled = !_LanRadioButton.Checked;
                _IpAddressControl.Enabled = _LanRadioButton.Checked;
                _PortNumericUpDown.Enabled = _LanRadioButton.Checked;
            };
            _SerialRadioButton.Checked = true;
        }

        private void FillSerialComonbox()
        {
            var list = SerialPort.GetPortNames();
            foreach (var s in list)
            {
                _SerialComboBox.Items.Add(s);
            }
        }

        public bool IsSerial { get { return _SerialRadioButton.Checked; } }

        public int Serial
        {
            get { return int.Parse(_SerialComboBox.Text); }
        }

        public IPAddress IpAddress
        {
            get { return IPAddress.Parse(_IpAddressControl.Text); }
        }

        public int Port
        {
            get { return int.Parse(_PortNumericUpDown.Text); }
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
