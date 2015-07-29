﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    //TODO:上下移,保存,导入,导出未完成
    public partial class CustomerScpiSubjectPanel : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<CustomerScpiSubjectPanel>();

        private readonly ListViewGroup _CollectGroup = new ListViewGroup("采集指令集", HorizontalAlignment.Left);
        private readonly ListViewGroup _InitGroup = new ListViewGroup("初始指令集", HorizontalAlignment.Left);

        private bool _IsModified;

        public CustomerScpiSubjectPanel()
        {
            InitializeComponent();
            _ListView.ShowItemToolTips = true;
            _ListView.Groups.AddRange(new[] {_InitGroup, _CollectGroup});
            _ListView.LostFocus += (s, e) => _ListView.SelectedIndices.Clear();

            var kernel = DI.Get<IMeterKernel>();
            kernel.Collected += (s, e) =>
            {
                if (e.GpibAddress == GpibAddress)
                    SetToolStripState(e.IsCollected);
            };
            _SaveButton.Enabled = false;
            _DeleteButton.Enabled = false;
            _EditButton.Enabled = false;
            _UpButton.Enabled = false;
            _DownButton.Enabled = false;
            _InitGroup.Header = "初始指令集";
            _InitGroup.Name = "INIT";
            _CollectGroup.Header = "采集指令集";
            _CollectGroup.Name = "COLLECT";

            _ListView.SelectedIndexChanged += OnListViewOnSelectedIndexChanged;
        }

        private void OnListViewOnSelectedIndexChanged(object s, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = _ListView.SelectedIndices;
            bool b = indices.Count > 0;
            _DeleteButton.Enabled = b;
            _EditButton.Enabled = b;

            if (b)
            {
                var item = _ListView.SelectedItems[0];
                if (item.Group == null)
                    return;
                List<ListViewItem> group = null;
                switch (item.Group.Name)
                {
                    case "INIT":
                        group = GetInitGroup();
                        break;
                    case "COLLECT":
                        group = GetCollectGroup();
                        break;
                }
                if (group != null)
                {
                    _UpButton.Enabled = group.IndexOf(item) > 0;
                    _DownButton.Enabled = group.IndexOf(item) < group.Count - 1;
                }
            }
        }

        private List<ListViewItem> GetInitGroup()
        {
            var list = _ListView.Items.Cast<ListViewItem>().Where(vi => vi.Group.Name == "INIT").ToList();
            foreach (var listViewItem in list)
                _logger.Fatal(listViewItem.SubItems[1]);
            return list;
        }

        private List<ListViewItem> GetCollectGroup()
        {
            var list = _ListView.Items.Cast<ListViewItem>().Where(vi => vi.Group.Name == "COLLECT").ToList();
            return list;
        }

        public bool IsModified
        {
            get { return _IsModified; }
            private set
            {
                _IsModified = value;
                _SaveButton.Enabled = value;
            }
        }

        public int GpibAddress { get; set; }

        public CommandQueue.CareItem[] GetCollectCommands()
        {
            return GetCommands("COLLECT");
        }

        public CommandQueue.CareItem[] GetInitCommands()
        {
            return GetCommands("INIT");
        }

        protected virtual CommandQueue.CareItem[] GetCommands(string groupName)
        {
            var commands = new List<CommandQueue.CareItem>();
            this.ThreadSafeInvoke(() =>
            {
                foreach (ListViewItem item in _ListView.Items)
                {
                    if (item.Checked && item.Group.Name == groupName)
                    {
                        var cmd = (ScpiCommand) (item.Tag);
                        var ci = new CommandQueue.CareItem
                        {
                            IsCare = false, 
                            GpibAddress = (short) GpibAddress, 
                            ScpiCommand = cmd
                        };
                        commands.Add(ci);
                    }
                }
            });
            return commands.ToArray();
        }

        protected void SetToolStripState(bool state)
        {
            _ListView.Enabled = !state;
            _ToolStrip.Enabled = !state;
        }

        protected void AddListItem(ScpiCommandGroupCategory category, ScpiCommand command)
        {
            var listitem = new ListViewItem {Checked = true};
            switch (category)
            {
                case ScpiCommandGroupCategory.Init:
                    listitem.Group = _InitGroup;
                    break;
                case ScpiCommandGroupCategory.Collect:
                    listitem.Group = _CollectGroup;
                    break;
            }
            var subitem = new ListViewItem.ListViewSubItem {Text = command.Command};
            listitem.SubItems.Add(subitem);
            subitem = new ListViewItem.ListViewSubItem { Text = command.Interval.ToString() };
            listitem.SubItems.Add(subitem);
            listitem.Tag = command;
            listitem.ToolTipText = command.ToString();
            _ListView.Items.Add(listitem);
            IsModified = true;
        }

        private void _OpenButton_Click(object sender, EventArgs e)
        {
            var dialog = new MeterScpiGroupTreeDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _ListView.Items.Clear();
                _StripLabel.Text = string.Format("{0} - {1}", dialog.CurrentMeter, dialog.CurrentDescription);
                ScpiSubject subject = dialog.ScpiSubject;
                foreach (ScpiCommand command in subject.Preload)
                {
                    AddListItem(ScpiCommandGroupCategory.Init, command);
                }
                foreach (ScpiCommand command in subject.Collect)
                {
                    AddListItem(ScpiCommandGroupCategory.Collect, command);
                }
            }
            IsModified = false;
        }

        private void _SaveButton_Click(object sender, EventArgs e)
        {
        }

        private void _AddInitButton_Click(object sender, EventArgs e)
        {
            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = ScpiCommandGroupCategory.Init;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
                var command = new ScpiCommand
                {
                    Command = dialog.Command,
                    Interval = dialog.Interval,
                    IsHex = dialog.IsHex,
                    IsReturn = false
                };
                AddListItem(ScpiCommandGroupCategory.Init, command);
            }
        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {
            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = ScpiCommandGroupCategory.Collect;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
                var command = new ScpiCommand
                {
                    Command = dialog.Command, 
                    Interval = dialog.Interval, 
                    IsHex = dialog.IsHex,
                    IsReturn = true
                };
                AddListItem(ScpiCommandGroupCategory.Collect, command);
            }
        }

        private void _DeleteButton_Click(object sender, EventArgs e)
        {
            int i = _ListView.SelectedIndices[0];
            string cmd = _ListView.Items[i].SubItems[1].Text;
            var ds = MessageBox.Show(this, string.Format("确认删除指令[{0}]么？", cmd), "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ds == DialogResult.Yes)
            {
                _ListView.Items.RemoveAt(i);
            }
            IsModified = true;
        }

        private void _EditButton_Click(object sender, EventArgs e)
        {
            int i = _ListView.SelectedIndices[0];
            var item = _ListView.Items[i];
            var command = (ScpiCommand) item.Tag;
            var dialog = new ScpiCommandEditorDialog
            {
                Command = command.Command, 
                Interval = (int) command.Interval, 
                IsHex = command.IsHex
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                item.SubItems[1].Text = dialog.Command;
                command.Command = dialog.Command;
                item.SubItems[2].Text = dialog.Interval.ToString();
                command.Interval = dialog.Interval;
                command.IsHex = dialog.IsHex;
                item.ToolTipText = command.ToString();
                IsModified = true;
            }
            item.Selected = true;
            _ListView.Focus();
        }

        private void _DownButton_Click(object sender, EventArgs e)
        {
            int i = _ListView.SelectedIndices[0];
            var item = _ListView.Items[i];
            var group = item.Group;

            _ListView.BeginUpdate();
            _ListView.Items.RemoveAt(i);
            _ListView.Items.Insert(i + 1, item);
            item.Group = group;
            _ListView.EndUpdate();
            _ListView.Refresh();
            _ListView.Focus();
            item.Selected = true;
            IsModified = true;
        }

        private void _UpButton_Click(object sender, EventArgs e)
        {
            IsModified = true;
        }

        private void _ImportButton_Click(object sender, EventArgs e)
        {

        }

        private void _ExportButton_Click(object sender, EventArgs e)
        {

        }

    }
}