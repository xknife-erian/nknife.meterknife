using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Scpis;

namespace NKnife.MeterKnife.Scpis
{
    public partial class CustomerScpiSubjectPanel : UserControl
    {
        private readonly ListViewGroup _collectGroup = new ListViewGroup("采集指令集", HorizontalAlignment.Left);
        private readonly ListViewGroup _initGroup = new ListViewGroup("初始指令集", HorizontalAlignment.Left);

        private readonly string _scpiSubjectKey = Guid.NewGuid().ToString();
        private CareCommandSubject _currentScpiSubject;
        private CareCommandSubjectList _currentScpiSubjectCollection;

        private bool _isModified;

        public CustomerScpiSubjectPanel()
        {
            InitializeComponent();

            _ListView.ShowItemToolTips = true;
            _ListView.Groups.AddRange(new[] {_initGroup, _collectGroup});
            _ListView.LostFocus += (s, e) => _ListView.SelectedIndices.Clear();

            //TODO:kernel
            //var kernel = DI.Get<IMeterKernel>();
            //kernel.Collected += (s, e) =>
            //{
            //    if (e.GpibAddress == GpibAddress)
            //        SetToolStripState(e.IsCollected);
            //};
            _SaveButton.Enabled = false;
            _AddButton.Enabled = false;
            _DeleteButton.Enabled = false;
            _EditButton.Enabled = false;
            _UpButton.Enabled = false;
            _DownButton.Enabled = false;
            _initGroup.Header = "初始指令集";
            _initGroup.Name = "INIT";
            _collectGroup.Header = "采集指令集";
            _collectGroup.Name = "COLLECT";

            _ListView.SelectedIndexChanged += _ListView_SelectedIndexChanged;
            _ListView.ItemChecked += _ListView_ItemChecked;
        }

        public string ScpiSubjectKey
        {
            get { return _scpiSubjectKey; }
        }

        public bool IsModified
        {
            get { return _isModified; }
            private set
            {
                _isModified = value;
                _SaveButton.Enabled = value;
            }
        }

        public int GpibAddress { get; set; }

        private void _ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var scpiCommand = (CareCommand) e.Item.Tag;
//            if (scpiCommand.Selected != e.Item.Checked)
//                IsModified = true;
//            scpiCommand.Selected = e.Item.Checked;
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

        private void _OpenButton_Click(object sender, EventArgs e)
        {
            var dialog = ScpiMangerForm.AutofacContainer.Resolve<InstrumentScpiGroupTreeDialog>();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _currentScpiSubject = null;
                _currentScpiSubjectCollection = null;
                if (dialog.CurrentIsSubject)
                {
                    _currentScpiSubject = dialog.SelectedScpiSubject;
                }
                else
                {
                    _currentScpiSubjectCollection = dialog.SelectedScpiSubjectCollection;
                }
                _AddButton.Enabled = true;
                _StripLabel.Text = dialog.CurrentMeter;
                UpdateListView();
            }
            IsModified = false;
        }

        private void UpdateListView()
        {
            _ListView.BeginUpdate();
            _ListView.Items.Clear();
            if (_currentScpiSubject != null)
            {
                foreach (var command in _currentScpiSubject.Initializtion)
                {
                    AddListItem(PoolCategory.Initializtion, command);
                }
                foreach (var command in _currentScpiSubject.Collect)
                {
                    AddListItem(PoolCategory.Collect, command);
                }
            }
            _ListView.EndUpdate();
            _ListView.Refresh();
            _ListView.Focus();
        }

        private void _SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentScpiSubject.Name) && _currentScpiSubjectCollection == null)
            {
                var collection = new CareCommandSubjectList();
                var dialog = ScpiMangerForm.AutofacContainer.Resolve<InstrumentAndSubjectInfoDialog>();
                dialog.ScpiSubjectCollection = collection;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    collection.Brand = dialog.InstBrand;
                    collection.Name = dialog.InstName;
                    collection.Description = dialog.InstDescription;
                    var fileName = $"{collection.Brand}{collection.Name}.xml";
                    collection.BuildScpiFile(Path.Combine(ScpiUtil.ScpisPath, fileName));
                    _currentScpiSubject.OwnerList = collection;
                    _currentScpiSubject.Name = dialog.GroupName;
                    collection.Add(_currentScpiSubject);
                }
                else
                {
                    MessageBox.Show(this, "指令集集合的名称不能为空，未执行保存操作。", "未保存", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (_currentScpiSubjectCollection != null)
            {
                var dialog = ScpiMangerForm.AutofacContainer.Resolve<InstrumentAndSubjectInfoDialog>();
                dialog.ScpiSubjectCollection = _currentScpiSubjectCollection;
                dialog.Initialize(_currentScpiSubjectCollection.Brand, _currentScpiSubjectCollection.Name, _currentScpiSubjectCollection.Description);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _currentScpiSubjectCollection.Add(_currentScpiSubject);
                    _currentScpiSubject.OwnerList = _currentScpiSubjectCollection;
                    _currentScpiSubject.Name = dialog.GroupName;
                }
                else
                {
                    MessageBox.Show(this, "指令集集合的名称不能为空，未执行保存操作。", "未保存", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (_currentScpiSubject.OwnerList != null && _currentScpiSubject.OwnerList.Save())
            {
                var brand = _currentScpiSubject.OwnerList.Brand;
                var name = _currentScpiSubject.OwnerList.Name;
                var content = string.Format("{0}{1}: “{2}”SCPI指令集保存成功", brand, name, _currentScpiSubject.Name);
                MessageBox.Show(this, content, "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _AddInitButton_Click(object sender, EventArgs e)
        {
            if (_currentScpiSubject == null)
                _currentScpiSubject = new CareCommandSubject();

            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = PoolCategory.Initializtion;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
                var command = new CareCommand()
                {
                    // Command = dialog.Command,
                    // Interval = dialog.Interval,
                    // IsHex = dialog.IsHex,
                    // IsReturn = false
                };
                AddListItem(PoolCategory.Initializtion, command);
            }
        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {
            if (_currentScpiSubject == null)
                _currentScpiSubject = new CareCommandSubject();

            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = PoolCategory.Collect;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
                var command = new CareCommand
                {
                    // Command = dialog.Command,
                    // Interval = dialog.Interval,
                    // IsHex = dialog.IsHex,
                    // IsReturn = true
                };
                AddListItem(PoolCategory.Collect, command);
            }
        }

        protected void AddListItem(PoolCategory category, CareCommand command)
        {
            var listitem = new ListViewItem {Checked = true};
            switch (category)
            {
                case PoolCategory.Initializtion:
                    listitem.Group = _initGroup;
                    if (!_currentScpiSubject.Initializtion.Contains(command))
                        _currentScpiSubject.Initializtion.Add(command);
                    break;
                case PoolCategory.Collect:
                    listitem.Group = _collectGroup;
                    if (!_currentScpiSubject.Collect.Contains(command))
                        _currentScpiSubject.Collect.Add(command);
                    break;
            }
            var subitem = new ListViewItem.ListViewSubItem {Text = command.Scpi.Command};
            listitem.SubItems.Add(subitem);
            subitem = new ListViewItem.ListViewSubItem {Text = command.Interval.ToString()};
//            listitem.Checked = command.Selected;
            listitem.SubItems.Add(subitem);
            listitem.Tag = command;
            listitem.ToolTipText = command.ToString();
            _ListView.Items.Add(listitem);
            IsModified = true;
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
                        _currentScpiSubject.Initializtion.RemoveAt(n);
                        break;
                    case "COLLECT":
                        _currentScpiSubject.Collect.RemoveAt(n);
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
            var command = (CareCommand) item.Tag;
            var dialog = new ScpiCommandEditorDialog
            {
                // Command = command.Command,
                // Interval = (int) command.Interval,
                // IsHex = command.IsHex,
                // IsReturn = command.IsReturn
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // command.Command = dialog.Command;
                // command.Interval = dialog.Interval;
                // command.IsHex = dialog.IsHex;
                //
                // var group = item.Group;
                // switch (group.Name)
                // {
                //     case "INIT":
                //         command.IsReturn = false;
                //         break;
                //     case "COLLECT":
                //         command.IsReturn = true;
                //         break;
                // }

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
                // case "INIT":
                //     _currentScpiSubject.Initializtion.DownItem(n);
                //     break;
                // case "COLLECT":
                //     _currentScpiSubject.Collect.DownItem(n);
                //     break;
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
                // case "INIT":
                //     _currentScpiSubject.Initializtion.UpItem(n);
                //     break;
                // case "COLLECT":
                //     _currentScpiSubject.Collect.UpItem(n);
                //     break;
            }
            UpdateListView();
            group.Items[n - 1].Selected = true;
            IsModified = true;
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

        // protected virtual ScpiCommandQueue.Item[] GetCommands(string groupName)
        // {
        //     var commands = new List<ScpiCommandQueue.Item>();
        //     this.ThreadSafeInvoke(() =>
        //     {
        //         foreach (ListViewItem item in _ListView.Items)
        //         {
        //             if (item.Checked && item.Group.Name == groupName)
        //             {
        //                 var cmd = (ScpiCommand) (item.Tag);
        //                 var ci = new ScpiCommandQueue.Item
        //                 {
        //                     IsCare = false,
        //                     GpibAddress = (short) GpibAddress,
        //                     ScpiCommand = cmd
        //                 };
        //                 commands.Add(ci);
        //             }
        //         }
        //     });
        //     return commands.ToArray();
        // }
        //
        // public KeyValuePair<string, ScpiCommandQueue.Item[]> GetCollectCommands()
        // {
        //     return new KeyValuePair<string, ScpiCommandQueue.Item[]>(_scpiSubjectKey, GetCommands("COLLECT"));
        // }
        //
        // public KeyValuePair<string, ScpiCommandQueue.Item[]> GetInitCommands()
        // {
        //     return new KeyValuePair<string, ScpiCommandQueue.Item[]>(_scpiSubjectKey, GetCommands("INIT"));
        // }

        #endregion
    }
}