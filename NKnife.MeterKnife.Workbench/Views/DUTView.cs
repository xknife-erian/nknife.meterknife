using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.DUTs;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class DUTView : DockContent
    {
        private readonly IDialogProvider _dialogProvider;

        public DUTView(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeLanguage();
            RespondToControlClick();
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            _NewToolStripButton.Res();
            _EditToolStripButton.Res();
            _DeleteToolStripButton.Res();
        }

        private void RespondToControlClick()
        {
            _NewToolStripButton.Click += (sender, args) =>
            {
                var dialog = _dialogProvider.New<DUTDetailDialog>();
                var rs = dialog.ShowDialog(this);
                if (rs == DialogResult.OK)
                {
                    var dut = dialog.DUT;
                }
            };
        }
    }
}
