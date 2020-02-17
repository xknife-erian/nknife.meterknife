using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    public partial class EngineeringDetailDialog : Form
    {
        private readonly IDialogProvider _dialogProvider;
        private ListViewGroup _initializeGroup;
        private ListViewGroup _acquisitionGroup;
        private ListViewGroup _finishGroup;

        public EngineeringDetailDialog(IDialogProvider dialogProvider)
        {
            _dialogProvider = dialogProvider;
            InitializeComponent();
            InitializeCommandListView();
            RespondToEvent();
            RespondToButtonClick();
        }

        private void InitializeCommandListView()
        {
            _initializeGroup = new ListViewGroup(this.Res("初始化设置"));
            _acquisitionGroup = new ListViewGroup(this.Res("采集过程"));
            _finishGroup = new ListViewGroup(this.Res("结束维护"));
            _CommandsListView.Groups.AddRange(new[] {_initializeGroup, _acquisitionGroup, _finishGroup});
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
            };
            _CreateInitializeCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Initializtion); };
            _CreateAcquisitionCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Acquisition); };
            _CreateFinishCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Finished); };
            _EditCommandStripButton.Click += (sender, args) => { };
            _DeleteCommandStripButton.Click += (sender, args) => { };
            _UpCommandStripButton.Click += (sender, args) => { };
            _DownCommandStripButton.Click += (sender, args) => { };
        }

        private void OpenDialogAndGetCommand(PoolCategory pc)
        {
            var dialog = _dialogProvider.New<CareCommandEditorDialog>();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var cmd = dialog.ScpiCommand;
                var item = new ListViewItem();
                switch (pc)
                {
                    case PoolCategory.Initializtion:
                        item.Group = _initializeGroup;
                        break;
                    case PoolCategory.Finished:
                        item.Group = _finishGroup;
                        break;
                    case PoolCategory.Acquisition:
                    default:
                        item.Group = _acquisitionGroup;
                        break;
                }
                item.SubItems.Add(cmd.DUT.ToString());
                item.SubItems.Add(cmd.Slot.ToString());
                item.SubItems.Add(cmd.Scpi.ToString());
                item.SubItems.Add(cmd.Interval.ToString());
                item.SubItems.Add(cmd.Timeout.ToString());
                item.SubItems.Add(cmd.IsLoop.ToString());
                item.SubItems.Add(cmd.LoopCount.ToString());
                _CommandsListView.Items.Add(item);
            }
        }

        private void RespondToEvent()
        {
            
        }
    }
}
