using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class AddGpibMeterDialog : Form
    {
        public AddGpibMeterDialog()
        {
            InitializeComponent();
            _AcceptButton.Click += (s, e) => DialogResult = DialogResult.OK;
            _CancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        public int GpibAddress
        {
            get { return (int)_NumberBox.Value; }
        }
    }
}
