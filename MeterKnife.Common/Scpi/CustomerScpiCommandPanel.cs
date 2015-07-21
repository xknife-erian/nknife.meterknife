using System;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public partial class CustomerScpiCommandPanel : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<CustomerScpiCommandPanel>();

        private readonly ListViewGroup _InitGroup = new ListViewGroup("初始指令集", HorizontalAlignment.Left);
        private readonly ListViewGroup _CollectGroup = new ListViewGroup("采集指令集", HorizontalAlignment.Left);

        private bool _IsModified = false;

        public bool IsModified
        {
            get
            {
                return _IsModified;
            }
            private set
            {
                _IsModified = value;
                _SaveButton.Enabled = value;
            }
        }

        public int GpibAddress { get; set; }

        public CustomerScpiCommandPanel()
        {
            GpibAddress = 23; //测试使用
            InitializeComponent();
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
            this.ThreadSafeInvoke(() =>
            {
                foreach (ListViewItem item in _ListView.Items)
                {
                    if (item.Checked && item.Group.Name == groupName)
                    {
                        ScpiCommand sc = null;
                        try
                        {
                            sc = new ScpiCommand();
                            sc.Command = item.SubItems[1].Text;
                            sc.Interval = int.Parse(item.SubItems[2].Text);
                        }
                        catch (Exception e)
                        {
                            _logger.Warn("解析ListItem的内容为Command时异常." + e.Message, e);
                            continue;
                        }
                        commands.AddLast(sc);
                    }
                }
            });
            return commands;
        }

        protected void SetToolStripState(bool state)
        {
            _ListView.Enabled = !state;
            _ToolStrip.Enabled = !state;
        }

        protected void AddListItem(ScpiCommandGroupCategory category, string command, long range)
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
            var subitem = new ListViewItem.ListViewSubItem {Text = command};
            listitem.SubItems.Add(subitem);
            subitem = new ListViewItem.ListViewSubItem {Text = range.ToString()};
            listitem.SubItems.Add(subitem);
            _ListView.Items.Add(listitem);
            IsModified = true;
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
                    AddListItem(ScpiCommandGroupCategory.Init, command.Command, command.Interval);
                }
                foreach (var command in subject.Collect)
                {
                    AddListItem(ScpiCommandGroupCategory.Collect, command.Command, command.Interval);
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
                AddListItem(ScpiCommandGroupCategory.Init, dialog.Command, dialog.Range);
            }
        }

        private void _AddCollectButton_Click(object sender, EventArgs e)
        {
            var dialog = new ScpiCommandEditorDialog();
            dialog.Category = ScpiCommandGroupCategory.Collect;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                IsModified = true;
                AddListItem(ScpiCommandGroupCategory.Collect, dialog.Command, dialog.Range);
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
