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
using NKnife.Events;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class CareParameterDialog : SimpleForm
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareParameterDialog>();
        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private int _Port;

        public CareParameterDialog(int port)
        {
            _Port = port;
            InitializeComponent();
            var handler = (ScpiProtocolHandler)_Comm.CareHandlers[_Port];
            handler.ProtocolRecevied += OnProtocolRecevied;
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareSaying> e)
        {
            _logger.Warn(e.Item);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var thread = new Thread(GetCareParameter);
            thread.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            var handler = (ScpiProtocolHandler)_Comm.CareHandlers[_Port];
            handler.ProtocolRecevied -= OnProtocolRecevied;
            base.OnClosed(e);
        }

        private void GetCareParameter()
        {
            for (int i = 0xD2; i <= 0xDC; i++)
            {
                var data = CareSaying.CareGetter((byte)i);
                _Comm.Send(_Port, data);
                Thread.Sleep(50);
            }
        }
    }
}
