using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class CustomerScpiSubjectPanel : UserControl
    {
        private readonly ListViewGroup _CollectGroup = new ListViewGroup("采集指令集", HorizontalAlignment.Left);
        private readonly ListViewGroup _InitGroup = new ListViewGroup("初始指令集", HorizontalAlignment.Left);

        private readonly string _ScpiSubjectKey = Guid.NewGuid().ToString();
        private ScpiSubject _CurrentScpiSubject;

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

            _ListView.SelectedIndexChanged += _ListView_SelectedIndexChanged;
            _ListView.ItemChecked += _ListView_ItemChecked;
        }

        public string ScpiSubjectKey
        {
            get { return _ScpiSubjectKey; }
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

        private void _ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var scpiCommand = (ScpiCommand) e.Item.Tag;
            if (scpiCommand.Selected != e.Item.Checked)
                IsModified = true;
            scpiCommand.Selected = e.Item.Checked;
        }

        private void _ListView_SelectedIndexChanged(object s, EventArgs e)
        {
            var indices = _ListView.SelectedIndices;
            var b = indices.Count > 0;
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
                    //根据ListViewItem在Group中的位置更新上移和下移按钮的状态
                    _UpButton.Enabled = group.IndexOf(item) > 0;
                    _DownButton.Enabled = group.IndexOf(item) < group.Count - 1;
                }
            }
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
                case ScpiCommandGroupCategory.Initializtion:
                    listitem.Group = _InitGroup;
                    if (!_CurrentScpiSubject.Initializtion.Contains(command))
                        _CurrentScpiSubject.Initializtion.Add(command);
                    break;
                case ScpiCommandGroupCategory.Collect:
                    listitem.Group = _CollectGroup;
                    if (!_CurrentScpiSubject.Collect.Contains(command))
                        _CurrentScpiSubject.Collect.Add(command);
                    break;
            }
            var subitem = new ListViewItem.ListViewSubItem {Text = command.Command};
            listitem.SubItems.Add(subitem);
            subitem = new ListViewItem.ListViewSubItem {Text = command.Interval.ToString()};
            listitem.Checked = command.Selected;
            listitem.SubItems.Add(subitem);
            listitem.Tag = command;
            listitem.ToolTipText = command.ToString();
            _ListView.Items.Add(listitem);
            IsModified = true;
        }

        private void _OpenButton_Click(object sender, EventArgs e)
        {
            var dialog = new InstrumentScpiGroupTreeDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _StripLabel.Text = string.Format("{0} - {1}", dialog.CurrentMeter, dialog.CurrentDescription);
                _CurrentScpiSubject = dialog.ScpiSubject;
                UpdateListView();
            }
            IsModified = false;
        }

        private void UpdateListView()
        {
            _ListView.BeginUpdate();
            _ListView.Items.Clear();
            foreach (var command in _CurrentScpiSubject.Initializtion)
            {
                AddListItem(ScpiCommandGroupCategory.Initializtion, command);
            }
            foreach (var command in _CurrentScpiSubject.Collect)
            {
                AddListItem(ScpiCommandGroupCategory.Collect, command);
            }
            _ListView.EndUpdate();
            _ListView.Refresh();
            _ListView.Focus();
        }

        private void _SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_CurrentScpiSubject.Description))
            {
                var dialog = new ScpiGroupInfomationDialog();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _CurrentScpiSubject.Description = dialog.GroupText;
                }
                else
                {
                    MessageBox.Show(this, "指令集集合的名称不能为空，未执行保存操作。", "未保存", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (_CurrentScpiSubject.OwnerCollection.Save())
            {
                var brand = _CurrentScpiSubject.OwnerCollection.Brand;
                var name = _CurrentScpiSubject.OwnerCollection.Name;
                var content = string.Format("{0}{1}: “{2}”SCPI指令集保存成功", brand, name, _CurrentScpiSubject.Description);
                MessageBox.Show(this, content, "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _AddInitButton_Click(object sender, EventArgs e)
        {
            if (CheckCurrentScpiSubject())
                return;
            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = ScpiCommandGroupCategory.Initializtion;
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
                AddListItem(ScpiCommandGroupCategory.Initializtion, command);
            }
        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {
            if (CheckCurrentScpiSubject())
                return;

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

        private bool CheckCurrentScpiSubject()
        {
            if (_CurrentScpiSubject == null)
            {
                var collection = new ScpiSubjectCollection();
                var newInstDialog = new NewInstrumentInfoDialog();
                newInstDialog.ScpiSubjectCollection = collection;
                if (newInstDialog.ShowDialog(this) == DialogResult.OK)
                {
                    collection.Brand = newInstDialog.InstBrand;
                    collection.Name = newInstDialog.InstName;
                    collection.Description = newInstDialog.InstDescription;
                    var fileName = string.Format("{0}{1}.xml", collection.Brand, collection.Name);
                    collection.BuildScpiFile(Path.Combine(ScpiUtil.ScpisPath, fileName));
                    _CurrentScpiSubject = new ScpiSubject();
                    _CurrentScpiSubject.OwnerCollection = collection;
                    collection.Add(_CurrentScpiSubject);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private void _DeleteButton_Click(object sender, EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var i = _ListView.SelectedIndices[0];
            var cmd = _ListView.Items[i].SubItems[1].Text;
            var content = string.Format("确认删除指令[{0}]么？", cmd);
            var ds = MessageBox.Show(this, content, "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ds == DialogResult.Yes)
            {
                var group = item.Group;
                var n = group.Items.IndexOf(item);
                _ListView.Items.RemoveAt(i);
                switch (group.Name)
                {
                    case "INIT":
                        _CurrentScpiSubject.Initializtion.RemoveAt(n);
                        break;
                    case "COLLECT":
                        _CurrentScpiSubject.Collect.RemoveAt(n);
                        break;
                }
                IsModified = true;
            }
            else
            {
                _ListView.Items[i].Selected = true;
            }
        }

        private void _EditButton_Click(object sender, EventArgs e)
        {
            var i = _ListView.SelectedIndices[0];
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
                command.Command = dialog.Command;
                command.Interval = dialog.Interval;
                command.IsHex = dialog.IsHex;

                var group = item.Group;
                switch (group.Name)
                {
                    case "INIT":
                        command.IsReturn = false;
                        break;
                    case "COLLECT":
                        command.IsReturn = true;
                        break;
                }

                item.SubItems[1].Text = dialog.Command;
                item.SubItems[2].Text = dialog.Interval.ToString();
                item.ToolTipText = command.ToString();
                IsModified = true;
            }
            item.Selected = true;
            _ListView.Focus();
        }

        private void _DownButton_Click(object sender, EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var group = item.Group;
            var n = group.Items.IndexOf(item);
            switch (group.Name)
            {
                case "INIT":
                    _CurrentScpiSubject.Initializtion.DownItem(n);
                    break;
                case "COLLECT":
                    _CurrentScpiSubject.Collect.DownItem(n);
                    break;
            }
            UpdateListView();
            group.Items[n + 1].Selected = true;
            IsModified = true;
        }

        private void _UpButton_Click(object sender, EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var group = item.Group;
            var n = group.Items.IndexOf(item);
            switch (group.Name)
            {
                case "INIT":
                    _CurrentScpiSubject.Initializtion.UpItem(n);
                    break;
                case "COLLECT":
                    _CurrentScpiSubject.Collect.UpItem(n);
                    break;
            }
            UpdateListView();
            group.Items[n - 1].Selected = true;
            IsModified = true;
        }

        private void _ImportButton_Click(object sender, EventArgs e)
        {
            //TODO:导入,导出未完成
        }

        private void _ExportButton_Click(object sender, EventArgs e)
        {
            //TODO:导入,导出未完成
        }

        #region 获取指令

        private List<ListViewItem> GetInitGroup()
        {
            var list = _ListView.Items.Cast<ListViewItem>().Where(vi => vi.Group.Name == "INIT").ToList();
            return list;
        }

        private List<ListViewItem> GetCollectGroup()
        {
            var list = _ListView.Items.Cast<ListViewItem>().Where(vi => vi.Group.Name == "COLLECT").ToList();
            return list;
        }

        protected virtual ScpiCommandQueue.Item[] GetCommands(string groupName)
        {
            var commands = new List<ScpiCommandQueue.Item>();
            this.ThreadSafeInvoke(() =>
            {
                foreach (ListViewItem item in _ListView.Items)
                {
                    if (item.Checked && item.Group.Name == groupName)
                    {
                        var cmd = (ScpiCommand) (item.Tag);
                        var ci = new ScpiCommandQueue.Item
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

        public KeyValuePair<string, ScpiCommandQueue.Item[]> GetCollectCommands()
        {
            return new KeyValuePair<string, ScpiCommandQueue.Item[]>(_ScpiSubjectKey, GetCommands("COLLECT"));
        }

        public KeyValuePair<string, ScpiCommandQueue.Item[]> GetInitCommands()
        {
            return new KeyValuePair<string, ScpiCommandQueue.Item[]>(_ScpiSubjectKey, GetCommands("INIT"));
        }

        #endregion
    }
}