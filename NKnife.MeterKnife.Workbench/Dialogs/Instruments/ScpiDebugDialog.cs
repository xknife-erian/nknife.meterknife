using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Workbench.Dialogs.Instruments
{
    public partial class ScpiDebugDialog : Form
    {
        public ScpiDebugDialog(IWorkbenchViewModel viewModel)
        {
            InitializeComponent();
            Shown += async (sender, args) =>
            {
                IEnumerable<Slot> slots = await viewModel.GetAllSlotAsync();
                _SlotComboBox.Items.AddRange(slots.Cast<object>().ToArray());
                if (_SlotComboBox.Items.Count > 0)
                    _SlotComboBox.SelectedItem = _SlotComboBox.Items[0];
            };
        }
    }
}
