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
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Instruments;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class InstrumentView : SingletonDockContent
    {
        private readonly IDialogProvider _dialogProvider;

        public InstrumentView(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeLanguage();
            RespondToControlClick();
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_NewToolStripButton, _EditToolStripButton, _DeleteToolStripButton);
        }

        private void RespondToControlClick()
        {
            _NewToolStripButton.Click += (sender, args) =>
            {
                var dialog = _dialogProvider.New<InstrumentDetailDialog>();
                var ds = dialog.ShowDialog();
                if (ds == DialogResult.OK)
                {

                }
            };
        }
    }
}
