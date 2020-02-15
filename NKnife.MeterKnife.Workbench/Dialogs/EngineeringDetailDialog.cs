using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public partial class EngineeringDetailDialog : Form
    {
        public EngineeringDetailDialog()
        {
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
            _GenerateNameOnDUTButton.Click += (sender, args) => { };
        }

        private void RespondToEvent()
        {
            
        }
    }
}
