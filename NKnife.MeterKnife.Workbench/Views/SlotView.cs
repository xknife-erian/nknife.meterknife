using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Workbench.Dialogs;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class SlotView : SingletonDockContent
    {
        private readonly IWorkbenchViewModel _viewModel;

        public SlotView(IWorkbenchViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            InitializeImageList();
            RespondToClickEvent();
            Shown += async (sender, args) =>
            {

                IEnumerable<Slot> slots = await viewModel.GetAllSlotAsync();
                foreach (var slot in slots)
                {
                    var slotListItem = new ListViewItem();
                    slotListItem.Tag = slot;
                    switch (slot.SlotType)
                    {
                        case SlotType.MeterCare:
                        default:
                        {
                            var config = slot.GetSerialPortInfo();
                            slotListItem.Text = $"MeterCare\r\nCOM{config.Item1}";
                            slotListItem.ImageKey = $"{nameof(SlotType.MeterCare)}";
                            break;
                        }
                    }

                    _SlotListView.Items.Add(slotListItem);
                }
            };
        }

        private void InitializeImageList()
        {
            if(_SlotListView.LargeImageList==null)
                _SlotListView.LargeImageList = new ImageList();
            _SlotListView.LargeImageList.ImageSize = new Size(72,72);
            _SlotListView.LargeImageList.Images.Add(nameof(SlotType.MeterCare), Resources.SlotType_MeterCare);
            _SlotListView.LargeImageList.Images.Add(nameof(SlotType.Serial), Resources.SlotType_Serial);
        }

        private void RespondToClickEvent()
        {
            _NewCareToolStripMenuItem.Click += (sender, args) =>
            {
                var dialog = new SerialPortSelectorDialog();
                dialog.Text = this.Res("选择MeterCare所在的串口");
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var result = dialog.SerialPort;
                    _viewModel.CreatMeterCareSlotAsync(result);
                }
            };
        }
    }
}
