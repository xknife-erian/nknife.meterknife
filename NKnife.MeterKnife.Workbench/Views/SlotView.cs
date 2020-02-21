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
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Workbench.Dialogs;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Base;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    /// <summary>
    /// 接驳器管理界面
    /// </summary>
    public sealed partial class SlotView : SingletonDockContent
    {
        private readonly IWorkbenchViewModel _viewModel;

        public SlotView(IWorkbenchViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            InitializeLanguage();
            InitializeImageList();
            RespondToClickEvent();
            Shown += async (sender, args) =>
            {

                IEnumerable<Slot> slots = await viewModel.GetAllSlotAsync();
                foreach (var slot in slots)
                {
                    var slotListItem = GetSlotListItem(slot);
                    _SlotListView.Items.Add(slotListItem);
                }
            };
            _NewToolStripDropDownButton.Image = Res.slot_add;
            _NewToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.Image = Res.slot_edit;
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteToolStripButton.Image = Res.slot_delete;
            _DeleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_NewToolStripDropDownButton, _NewSerialPortToolStripMenuItem, _EditToolStripButton, _DeleteToolStripButton);
        }

        private void InitializeImageList()
        {
            if(_SlotListView.LargeImageList==null)
                _SlotListView.LargeImageList = new ImageList();
            _SlotListView.LargeImageList.ImageSize = new Size(64,64);
            _SlotListView.LargeImageList.Images.Add(nameof(SlotType.MeterCare), LargeImageResource.slot_meter_care);
            _SlotListView.LargeImageList.Images.Add(nameof(SlotType.Serial), LargeImageResource.slot_serial);
        }

        private void RespondToClickEvent()
        {
            _NewCareToolStripMenuItem.Click += async (sender, args) =>
            {
                var dialog = new SerialPortSelectorDialog();
                dialog.Text = this.Res("选择MeterCare所在的串口");
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var result = dialog.SerialPort;
                    var slot = await _viewModel.CreateMeterCareSlotAsync(result);
                    var item = GetSlotListItem(slot);
                    _SlotListView.Items.Add(item);
                }
            };
        }

        private static ListViewItem GetSlotListItem(Slot slot)
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
            return slotListItem;
        }
    }
}
