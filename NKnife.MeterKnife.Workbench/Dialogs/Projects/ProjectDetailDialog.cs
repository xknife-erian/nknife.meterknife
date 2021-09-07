using System;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Workbench.Base;
using NKnife.MeterKnife.Workbench.Dialogs.Commands;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Projects
{
    public partial class ProjectDetailDialog : Form
    {
        private readonly IWorkbenchViewModel _viewModel;
        private readonly IDialogProvider _dialogProvider;
        private ListViewGroup _initializeGroup;
        private ListViewGroup _acquisitionGroup;
        private ListViewGroup _finishGroup;
        private Project _project;

        public ProjectDetailDialog(IDialogProvider dialogProvider, IWorkbenchViewModel viewModel)
        {
            _dialogProvider = dialogProvider;
            _viewModel = viewModel;
            InitializeComponent();
            InitializeCommandListView();
            RespondToButtonClick();
        }

        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                _EngNumberTextBox.Text = value.Id;
                _EngNameTextBox.Text = value.Name;
                _EngDescriptionTextBox.Text = value.Description;
                foreach (var pool in value.CommandPools)
                {
                    foreach (ScpiCommand command in pool)
                    {
                        var item = BuildViewItemByCommand(pool.Category, command);
                        _CommandsListView.Items.Add(item);
                    }
                }
            }
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
                var rand = UtilRandom.GetString(1, UtilRandom.RandomCharType.Uppercased);
                _EngNumberTextBox.Text = $"E{DateTime.Now:yyMMddHHmmss}{rand}";
            };
            _GenerateNameOnDUTButton.Click += (sender, args) => { };
            _CreateInitializeCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Initializtion); };
            _CreateAcquisitionCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Acquisition); };
            _CreateFinishCmdStripButtonMenuItem.Click += (sender, args) => { OpenDialogAndGetCommand(PoolCategory.Finished); };
            _EditCommandStripButton.Click += (sender, args) => { };
            _DeleteCommandStripButton.Click += (sender, args) => { };
            _UpCommandStripButton.Click += (sender, args) => { };
            _DownCommandStripButton.Click += (sender, args) => { };
            _CancelButton.Click += (sender, args) => { DialogResult = DialogResult.Cancel; };
            _AcceptButton.Click += OnAcceptButtonOnClick;
        }

        private void OnAcceptButtonOnClick(object sender, EventArgs args)
        {
            if (!VerifyControlValue(out Control control, out string message))
            {
                MessageBox.Show(message, this.Res("填写有误"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return;
            }

            Project = new Project {Id = _EngNumberTextBox.Text, Name = _EngNameTextBox.Text, Description = _EngDescriptionTextBox.Text, CreateTime = DateTime.Now};

            var pool = new ScpiCommandPool();
            pool.Category = PoolCategory.Initializtion;
            foreach (ListViewItem item in _initializeGroup.Items)
            {
                var cmd = item.Tag as ScpiCommand;
                pool.Add(cmd);
            }
            if (pool.Count > 0) 
                Project.CommandPools.Add(pool);

            pool = new ScpiCommandPool();
            pool.Category = PoolCategory.Acquisition;
            foreach (ListViewItem item in _acquisitionGroup.Items)
            {
                var cmd = item.Tag as ScpiCommand;
                pool.Add(cmd);
            }
            if (pool.Count > 0) 
                Project.CommandPools.Add(pool);

            pool = new ScpiCommandPool();
            pool.Category = PoolCategory.Finished;
            foreach (ListViewItem item in _finishGroup.Items)
            {
                var cmd = item.Tag as ScpiCommand;
                pool.Add(cmd);
            }
            if (pool.Count > 0)
                Project.CommandPools.Add(pool);

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool VerifyControlValue(out Control control, out string msg)
        {
            msg = string.Empty;
            control = _EngNumberTextBox;
            if (string.IsNullOrEmpty(_EngNumberTextBox.Text))
            {
                msg = this.Res("工程编号不能为空。");
                return false;
            }

            if (_viewModel.ExistProject(_EngNumberTextBox.Text))
            {
                msg = this.Res("工程编号已存在，请重新编号。");
                return false;
            }

            return true;
        }

        private void OpenDialogAndGetCommand(PoolCategory pc)
        {
            var dialog = _dialogProvider.New<CommandEditorDialog>();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var cmd = dialog.ScpiCommand;
                var item = BuildViewItemByCommand(pc, cmd);
                _CommandsListView.Items.Add(item);
            }
        }

        private ListViewItem BuildViewItemByCommand(PoolCategory pc, ScpiCommand cmd)
        {
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
                case PoolCategory.Other:
                default:
                    item.Group = _acquisitionGroup;
                    break;
            }

            item.SubItems.Add(cmd.DUT?.ToString());
            item.SubItems.Add(cmd.Slot.ToString());
            item.SubItems.Add(cmd.Scpi?.ToString());
            item.SubItems.Add(cmd.Interval.ToString());
            item.SubItems.Add(cmd.Timeout.ToString());
            item.SubItems.Add(cmd.IsLoop.ToString());
            item.SubItems.Add(cmd.LoopCount.ToString());
            item.Tag = cmd;
            return item;
        }
    }
}
