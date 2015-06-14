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
            //临时
            _LanRadioButton.Enabled = false;
        }

        private void FillSerialComonbox()
        {
            var list = SerialPort.GetPortNames();
            foreach (var s in list)
            {
                _SerialComboBox.Items.Add(s);
            }
            if (!UtilityCollection.IsNullOrEmpty(list))
            {
                _SerialComboBox.SelectedItem = list[list.Length - 1];
            }
        }

        public bool IsSerial { get { return _SerialRadioButton.Checked; } }

        public int Serial
        {
            get
            {
                var v = _SerialComboBox.Text.TrimStart(new char[] {'C', 'O', 'M'});
                return int.Parse(v);
            }
        }

        public IPAddress IpAddress
        {
            get
            {
                string value = _IpAddressControl.Text == "..." 
                    ? "0.0.0.0" 
                    : _IpAddressControl.Text;
                return IPAddress.Parse(value);
            }
        }

        public int Port
        {
            get { return int.Parse(_PortNumericUpDown.Text); }
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            if (IsSerial)
            {
                if (string.IsNullOrEmpty(_SerialComboBox.Text))
                {
                    MessageBox.Show(this, "必须选择一个串口");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_IpAddressControl.Text) || _IpAddressControl.Text == "...")
                {
                    MessageBox.Show(this, "必须设置一个IP地址");
                    return;
                }
                if (string.IsNullOrEmpty(_PortNumericUpDown.Text))
                {
                    MessageBox.Show(this, "必须设置一个TCPIP通讯端口");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
