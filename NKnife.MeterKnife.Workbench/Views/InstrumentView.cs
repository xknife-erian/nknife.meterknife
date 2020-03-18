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
using NKnife.MeterKnife.Resources;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Instruments;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.Win.Quick.Controls;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class InstrumentView : SingletonDockContent
    {
        private readonly IWorkbenchViewModel _viewModel;
        private readonly IDialogProvider _dialogProvider;
        private readonly ImageList _instPhotos = new ImageList();

        public InstrumentView(IDialogProvider dialogProvider, IWorkbenchViewModel viewModel)
        {
            _dialogProvider = dialogProvider;
            _viewModel = viewModel;
            InitializeComponent();
            InitializeLanguage();
            InitializeImageList();
            RespondToControlClick();
            UpdateControlState();
            Shown += async (sender, args) => { await FillListView(); };
        }

        private async Task FillListView()
        {
            SuspendLayout(); 
            _InstrumentListView.Items.Clear();
            IEnumerable<Instrument> instArray = await _viewModel.GetAllInstrumentAsync();
            foreach (var inst in instArray)
            {
                var item = BuildInstrumentListItem(inst);
                _InstrumentListView.Items.Add(item);
            }
            ResumeLayout(false);
            PerformLayout();
        }

        private void UpdateControlState()
        {
            if (_InstrumentListView.SelectedItems.Count > 0)
            {
                _NewToolStripButton.Enabled = true;
                _EditToolStripButton.Enabled = true;
                _DeleteToolStripButton.Enabled = true;
            }
            else
            {
                _NewToolStripButton.Enabled = true;
                _EditToolStripButton.Enabled = false;
                _DeleteToolStripButton.Enabled = false;
            }
        }

        private void InitializeImageList()
        {
            _InstrumentListView.LargeImageList = _instPhotos;
            _InstrumentListView.LargeImageList.ImageSize = new Size(64, 64);
            _instPhotos.Images.Add(nameof(LargeImageResource.instrument), LargeImageResource.instrument);
            _instPhotos.Images.Add(nameof(LargeImageResource.inst_agilent_34401), LargeImageResource.inst_agilent_34401);
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_NewToolStripButton, _EditToolStripButton, _DeleteToolStripButton);
            _NewToolStripButton.Image = MenuButtonResource.base_add_24px;
            _NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.Image = MenuButtonResource.base_edit_24px;
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteToolStripButton.Image = MenuButtonResource.base_delete_24px;
            _DeleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void RespondToControlClick()
        {
            _InstrumentListView.ItemSelectionChanged += delegate { UpdateControlState(); };
            _NewToolStripButton.Click += async (sender, args) =>
            {
                var dialog = _dialogProvider.New<InstrumentDetailDialog>();
                var ds = dialog.ShowDialog();
                if (ds == DialogResult.OK)
                {
                    var inst = dialog.Instrument;
                    var item = BuildInstrumentListItem(inst);
                    _InstrumentListView.Items.Add(item);
                    await _viewModel.CreateInstrumentAsync(inst);
                }
            };
            _EditToolStripButton.Click += async delegate(object sender, EventArgs args)
            {
                if (_InstrumentListView.SelectedItems.Count <= 0)
                    return;
                var inst = (Instrument) _InstrumentListView.SelectedItems[0].Tag;
                var dialog = _dialogProvider.New<InstrumentDetailDialog>();
                dialog.Instrument = inst;
                var ds = dialog.ShowDialog();
                if (ds == DialogResult.OK)
                {
                    UpdateInstrumentListItem(_InstrumentListView.SelectedItems[0], dialog.Instrument);
                    await _viewModel.UpdateInstrumentAsync(inst);
                }
            };
            _DeleteToolStripButton.Click += async delegate(object sender, EventArgs args)
            {
                if (_InstrumentListView.SelectedItems.Count <= 0)
                    return;
                var inst = (Instrument) _InstrumentListView.SelectedItems[0].Tag;
                var info = $"{inst.Name}\r\n{inst.Manufacturer}/{inst.Model1}/{inst.Model2}".TrimEnd('/');
                var ds = MessageBox.Show($"{this.Res("是否删除？")}\r\n{this.Res("仪器：")}{info}", $"{this.Res("删除")}",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (ds == DialogResult.Yes)
                {
                    await _viewModel.DeleteInstrumentAsync(inst);
                    await FillListView();
                }
            };
        }

        private ListViewItem BuildInstrumentListItem(Instrument inst)
        {
            var item = new ListViewItem();
            item.Tag = inst;
            item.Text = $"{inst.Manufacturer} {inst.Model1}";
            item.ImageKey = nameof(LargeImageResource.instrument);
            return item;
        }

        private ListViewItem UpdateInstrumentListItem(ListViewItem item, Instrument inst)
        {
            item.Tag = inst;
            item.Text = $"{inst.Manufacturer} {inst.Model1}";
            item.ImageKey = nameof(LargeImageResource.instrument);
            return item;
        }
    }
}
