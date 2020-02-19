using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Workbench.Dialogs.DUTs
{
    public partial class DUTDetailDialog : Form
    {
        public DUTDetailDialog()
        {
            InitializeComponent();
            InitializeLanguage();
        }

        public DUT DUT { get; set; }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, _AutoNumberButton);
            this.Res(tabPage1, tabPage2, tabPage3, tabPage4);
            this.Res(label1, label2, label3, label4, label5, label6);
            this.Res(columnHeader1, columnHeader2, columnHeader3, columnHeader4);
        }
    }
}
