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
            Shown += async (sender, args) =>
            {

                IEnumerable<Instrument> instArray = await viewModel.GetAllInstrumentAsync();
                foreach (var inst in instArray)
                {
                    var item = GetInstrumentListItem(inst);
                    _InstrumentListView.Items.Add(item);
                }
            };
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
        }

        private void RespondToControlClick()
        {
            _NewToolStripButton.Click += async (sender, args) =>
            {
                var dialog = _dialogProvider.New<InstrumentDetailDialog>();
                var ds = dialog.ShowDialog();
                if (ds == DialogResult.OK)
                {
                    var inst = dialog.Instrument;
                    var item = GetInstrumentListItem(inst);
                    _InstrumentListView.Items.Add(item);
                    await _viewModel.CreateInstrumentAsync(inst);
                }
            };
        }

        private ListViewItem GetInstrumentListItem(Instrument inst)
        {
            var item = new ListViewItem();
            item.Tag = inst;
            item.Text = $"{inst.Manufacturer} {inst.Model1}";
            item.ImageKey = nameof(LargeImageResource.instrument);
            return item;
        }
    }
}
