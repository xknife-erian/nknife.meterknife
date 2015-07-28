using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.DemoApplication.Dialogs
{
    public partial class CustomScpiGroupDialog : SimpleForm
    {
        private readonly IMeterKernel _Kernel = DI.Get<IMeterKernel>();
        private bool _IsCollect = false;

        public CustomScpiGroupDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _IsCollect = !_IsCollect;
            _Kernel.UpdateCollectState(CarePort.Build(TunnelType.Serial, "COM3"), 23, _IsCollect);
        }
    }
}
