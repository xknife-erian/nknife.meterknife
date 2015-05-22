using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using NKnife.Events;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class CareParameterDialog : SimpleForm
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareParameterDialog>();
        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private readonly int _Port;

        public CareParameterDialog(int port)
        {
            _Port = port;
            InitializeComponent();
            var handler = (ScpiProtocolHandler) _Comm.CareHandlers[_Port];
            handler.ProtocolRecevied += OnProtocolRecevied;
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            var talking = e.Item;
            if (talking != null && talking.MainCommand == 0xA0)
            {
                switch (talking.SubCommand)
                {
                    case 0xD2: //查询设备版本
                        _MainGroupBox.ThreadSafeInvoke(() =>
                        {
                            _MainGroupBox.Text = string.Format("Care.v{0}", talking.Scpi);
                        });
                        break;
                    case 0xD3: //查询DHCP是否激活（0x00:未激活;0x01:激活）
                        break;
                    case 0xD4: //查询Care的IP地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _IpAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD5: //查询Care的网关地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _GatwayAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD6: //查询Care的子网掩码；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _MaskAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD7: //查询Care的TCP Socket Server端口；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            var port = byte2Int(bs);
                            _TcpNumericUpDown.Value = port;
                        });
                        break;
                    case 0xDA: //查询温湿度传感器类型，18B20 or DHT22
                        this.ThreadSafeInvoke(() =>
                        {
                            switch (talking.Scpi.TrimEnd('\n'))
                            {
                                case "DS18B20":
                                    _18b20RadioButton.Checked = true;
                                    break;
                                case "DHT11":
                                    _Dht11RadioButton.Checked = true;
                                    break;
                                case "DHT22":
                                    _Dht22RadioButton.Checked = true;
                                    break;
                            }
                        });
                        break;
                    case 0xDB: //查询透明协议时仪器的GPIB地址；
                        break;
                    case 0xDC: //查询Care的MAC地址；
                        break;
                }
            }
        }

        public static int byte2Int(byte[] b)
        {
            int iOutcome = 0;
            for (int i = 0; i < b.Length; i++)
            {
                byte bLoop = b[i];
                iOutcome += (bLoop & 0xFF) << (8*i);
            }
            return iOutcome;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var task = new Task(GetCareParameter);
            task.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            var handler = (ScpiProtocolHandler) _Comm.CareHandlers[_Port];
            handler.ProtocolRecevied -= OnProtocolRecevied;
            base.OnClosed(e);
        }

        private void GetCareParameter()
        {
            for (int i = 0xD2; i <= 0xDC; i++)
            {
                if (i == 0xD8 || i == 0xD9)
                    continue;
                var data = CareTalking.CareGetter((byte) i);
                _Comm.Send(_Port, data);
                Thread.Sleep(50);
            }
        }
    }
}
