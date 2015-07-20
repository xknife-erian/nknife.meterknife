using System;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public partial class CustomerScpiCommandPanel : UserControl
    {
        readonly ListViewGroup _InitGroup = new ListViewGroup("初始指令集", HorizontalAlignment.Left);
        readonly ListViewGroup _CollectGroup = new ListViewGroup("采集指令集", HorizontalAlignment.Left);

        public bool IsModified { get; private set; }

        public int GpibAddress { get; set; }

        public CustomerScpiCommandPanel()
        {
            GpibAddress = 23; //测试使用
            InitializeComponent();
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

            _ListView.Groups.AddRange(new[] {_InitGroup,_CollectGroup});
            _ListView.LostFocus += (s, e) => _ListView.SelectedIndices.Clear();
            _ListView.SelectedIndexChanged += (s, e) =>
            {
                var b = _ListView.SelectedIndices.Count > 0;
                _DeleteButton.Enabled = b;
                _EditButton.Enabled = b;
                _UpButton.Enabled = b && _ListView.SelectedIndices[0] > 0;
                _DownButton.Enabled = b && _ListView.SelectedIndices[0] < _ListView.Items.Count - 1;
                //TODO:分组后上下真是麻烦
            };
        }

        public ScpiGroup GetCollectCommands()
        {
            return GetCommands("COLLECT");
        }

        public ScpiGroup GetInitCommands()
        {
            return GetCommands("INIT");
        }

        protected virtual ScpiGroup GetCommands(string groupName)
        {
            var commands = new ScpiGroup();
            foreach (ListViewItem item in _ListView.Items)
            {
                if (item.Checked && item.Group.Name == groupName)
                {
                    commands.AddLast(new ScpiCommand()
                    {
                        Command = item.SubItems[0].Text,
                        Interval = int.Parse(item.SubItems[1].Text)
                    });
                }
            }
            return commands;
        }

        protected void SetToolStripState(bool state)
        {
            _ListView.Enabled = !state;
            _ToolStrip.Enabled = !state;
        }

        private void _LoadButton_Click(object sender, EventArgs e)
        {
            var dialog = new MeterScpiGroupTreeDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _ListView.Items.Clear();
                _StripLabel.Text = string.Format("{0} - {1}", dialog.CurrentMeter, dialog.CurrentDescription);
                var subject = dialog.ScpiSubject;
                foreach (var command in subject.Preload)
                {
                    var listitem = new ListViewItem();
                    listitem.Checked = true;
                    listitem.Group = _InitGroup;

                    var subitem = new ListViewItem.ListViewSubItem();
                    subitem.Text = command.Command;
                    listitem.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    subitem.Text = command.Interval.ToString();
                    listitem.SubItems.Add(subitem);

                    _ListView.Items.Add(listitem);
                }
                foreach (var command in subject.Collect)
                {
                    var listitem = new ListViewItem();
                    listitem.Checked = true;
                    listitem.Group = _CollectGroup;

                    var subitem = new ListViewItem.ListViewSubItem();
                    subitem.Text = command.Command;
                    listitem.SubItems.Add(subitem);

                    subitem = new ListViewItem.ListViewSubItem();
                    subitem.Text = command.Interval.ToString();
                    listitem.SubItems.Add(subitem);

                    _ListView.Items.Add(listitem);
                }
            }
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
            }
        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {
            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = ScpiCommandGroupCategory.Collect;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
            }
        }

        private void _DeleteButton_Click(object sender, EventArgs e)
        {
            IsModified = true;
        }

        private void _EditButton_Click(object sender, EventArgs e)
        {
            var dialog = new ScpiCommandEditorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
            }
        }

        private void _DownButton_Click(object sender, EventArgs e)
        {
            IsModified = true;
        }

        private void _UpButton_Click(object sender, EventArgs e)
        {
            IsModified = true;
        }
    }
}
