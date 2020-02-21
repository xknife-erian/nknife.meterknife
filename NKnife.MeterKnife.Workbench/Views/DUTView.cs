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
using NKnife.MeterKnife.Workbench.Dialogs.DUTs;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Views
{
    public partial class DUTView : DockContent
    {
        private readonly IDialogProvider _dialogProvider;
        private readonly IWorkbenchViewModel _viewModel;

        public DUTView(IDialogProvider dialogProvider, IWorkbenchViewModel viewModel)
        {
            _dialogProvider = dialogProvider;
            _viewModel = viewModel;
            InitializeComponent();
            InitializeLanguage();
            RespondToControlClick();

            Shown += async (sender, args) =>
            {
                _DUTListView.Items.Clear();
                var dutArray = await _viewModel.GetAllDUTAsync();
                foreach (var dut in dutArray)
                {
                    var item = GetDUTListItem(dut);
                    _DUTListView.Items.Add(item);
                }
            };
        }

        private void InitializeLanguage()
        {
            this.Res(this);
            this.Res(_NewToolStripButton, _EditToolStripButton, _DeleteToolStripButton);
            _NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _NewToolStripButton.Image = MenuButtonResource.base_add_24px;
            _EditToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _EditToolStripButton.Image = MenuButtonResource.base_edit_24px;
            _DeleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _DeleteToolStripButton.Image = MenuButtonResource.base_delete_24px;
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
                    _viewModel.CreateDUTAsync(dut);
                    ListViewItem item = GetDUTListItem(dut);
                    _DUTListView.Items.Add(item);
                }
            };
        }

        private ListViewItem GetDUTListItem(DUT dut)
        {
            var item = new ListViewItem(dut.Name) {Tag = dut};
            item.SubItems.Add(dut.Classify);
            item.SubItems.Add(dut.ExpectValue.ToString("N"));
            if (dut.Unit != null)
                item.SubItems.Add(dut.Unit.ToString());
            else
                item.SubItems.Add("");
            if (dut.MetrologyValues != null && dut.MetrologyValues.Length > 0)
            {
                item.SubItems.Add(dut.MetrologyValues[0].DateTime.ToString("yyyy-MM-dd"));
                item.SubItems.Add(dut.MetrologyValues[0].Value.ToString("N"));
            }
            else
            {
                item.SubItems.Add("");
                item.SubItems.Add("");
            }

            item.SubItems.Add(dut.Description);
            item.SubItems.Add(dut.PhotosPath);
            item.SubItems.Add(dut.FilesPath);
            item.SubItems.Add(dut.Id);
            item.SubItems.Add(dut.CreateTime.ToString("yyyy-MM-dd HH:mm"));
            return item;
        }
    }
}
