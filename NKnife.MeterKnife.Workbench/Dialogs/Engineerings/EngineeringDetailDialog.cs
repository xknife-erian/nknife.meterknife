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

namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    public partial class EngineeringDetailDialog : Form
    {
        private readonly IDialogProvider _dialogProvider;

        public EngineeringDetailDialog(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            InitializeComponent();
            RespondToEvent();
            RespondToButtonClick();
        }

        private void RespondToButtonClick()
        {
            _AutomaticNumberGenerationButton.Click += (sender, args) =>
            {
                var number = SequentialGuid.Create().ToString("D").ToUpper();
                _EngNumberTextBox.Text = number;
            };
            _GenerateNameOnDUTButton.Click += (sender, args) =>
            {
                var dialog = _dialogProvider.New<CareCommandEditorDialog>();
                dialog.ShowDialog(this);
            };
            _CreateCommandStripButton.Click += (sender, args) =>
            {
                var dialog = _dialogProvider.New<CareCommandEditorDialog>();
                dialog.ShowDialog(this);
            };
            _EditCommandStripButton.Click += (sender, args) => { };
            _DeleteCommandStripButton.Click += (sender, args) => { };
            _UpCommandStripButton.Click += (sender, args) => { };
            _DownCommandStripButton.Click += (sender, args) => { };
        }

        private void RespondToEvent()
        {
            
        }
    }
}
