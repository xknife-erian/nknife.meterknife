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
using NKnife.MeterKnife.Workbench.Dialogs.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    public partial class InstrumentDetailDialog : Form
    {
        private readonly IDialogProvider _dialogProvider;

        public InstrumentDetailDialog(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeLanguage();
            RespondToMenuButtonClick();
        }

        private void InitializeLanguage()
        {
            this.Res(this, _AcceptButton, _CancelButton, _AutoNumberButton);
            this.Res(tabPage1, tabPage2, tabPage3, tabPage4);
            this.Res(label1, label2, label3, label4, label5, label6, label7, label8);
            this.Res(_AddCommandToolStripButton, _EditCommandToolStripButton, _DeleteCommandToolStripButton);
            this.Res(_AddPhotoToolStripButton, _EditPhotoToolStripButton, _DeletePhotoToolStripButton);
            this.Res(_AddFileToolStripButton, _EditFileToolStripButton, _DeleteFileToolStripButton);
        }

        private void RespondToMenuButtonClick()
        {
            _AddCommandToolStripButton.Click += (sender, args) =>
            {
                var dialog = _dialogProvider.New<ScpiDetailDialog>();
                var ds = dialog.ShowDialog(this);
                if (ds == DialogResult.OK)
                {
                }
            };
            _EditCommandToolStripButton.Click += (sender, args) => { };
            _DeleteCommandToolStripButton.Click += (sender, args) => { };
        }
    }
}
