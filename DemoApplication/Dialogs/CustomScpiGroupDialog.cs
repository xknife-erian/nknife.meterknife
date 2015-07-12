using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.DemoApplication.Dialogs
{
    public partial class CustomScpiGroupDialog : SimpleForm
    {
        private readonly IMeterKernel _Kernel;
        private bool _IsCollect = false;

        public CustomScpiGroupDialog()
        {
            InitializeComponent();
            _Kernel = DI.Get<IMeterKernel>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _IsCollect = !_IsCollect;
            _Kernel.CollectBeginning(23, _IsCollect);
        }
    }
}
