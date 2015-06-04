using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments.Properties;

namespace MeterKnife.Instruments.Common
{
    public class ScpiParamPanel : BaseParamPanel
    {
        private static readonly ILog _logger = LogManager.GetLogger<ScpiParamPanel>();
        protected readonly List<ComboBox> _ComboBoxList = new List<ComboBox>();
        protected GpibCommandList _Commandlist;

        public ScpiParamPanel(XmlElement element)
        {
            ParseElement(element);
        }

        public override GpibCommandList GpibCommands
        {
            get
            {
                this.ThreadSafeInvoke(FillCommandList);
                return _Commandlist;
            }
        }

        protected virtual void FillCommandList()
        {
            _Commandlist = GetGpibCommandList();
            foreach (ComboBox comboBox in _ComboBoxList)
            {
                if (comboBox.SelectedItem == null)
                    continue;
                var cmd = (GpibCommand) (comboBox.SelectedItem);
                _Commandlist.AddLast(cmd);
            }
        }

        protected static GpibCommandList GetGpibCommandList()
        {
            var cmdlist = new GpibCommandList();
            cmdlist.AddLast(new GpibCommand {Command = "*CLS", Interval = 50});
            cmdlist.AddLast(new GpibCommand {Command = "*CLS", Interval = 50});
            cmdlist.AddLast(new GpibCommand {Command = "*RST", Interval = 200});
            cmdlist.AddLast(new GpibCommand {Command = "*CLS", Interval = 50});
            cmdlist.AddLast(new GpibCommand {Command = "INIT", Interval = 50});
            return cmdlist;
        }

        protected virtual void ParseElement(XmlElement element)
        {
            string isScpiStr = element.GetAttribute("format");
            bool isScpi = true;
            bool.TryParse(isScpiStr, out isScpi);
            XmlNodeList nodes = element.SelectNodes("/MeterParam/command[@isConfig='true']");
            if (nodes == null)
                return;

            int count = nodes.Count;
            _Panel.RowCount = count + 1;
            _Panel.RowStyles.Clear();
            //_Panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; //测试

            int index = 0;
            foreach (XmlElement confEle in nodes)
            {
                _Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                var rootCmd = new GpibCommand(isScpi);
                rootCmd.Content = confEle.GetAttribute("content");
                rootCmd.Command = confEle.GetAttribute("command");

                GetLabel(rootCmd.Content, index);

                if (!confEle.HasChildNodes)
                    continue;
                bool isAddButton = false;
                Panel cbxPanel;
                ComboBox cbx = GetComboBox(index, out cbxPanel);
                foreach (XmlElement confContentEle in confEle.ChildNodes)
                {
                    #region config element

                    GpibCommand cmd = ParseGpibCommand(isScpi, confContentEle, rootCmd.Command);
                    cbx.Items.Add(cmd);

                    if (!confContentEle.HasChildNodes)
                        continue;

                    #region 有配置子项

                    var subButton = GetSubButton();
                    cbx.SelectedIndexChanged += (s, e) =>
                    {
                        var selectedCmd = cbx.SelectedItem as GpibCommand;
                        if (selectedCmd != null && selectedCmd.Tag != null)
                        {
                            subButton.Tag = cmd.Tag;
                        }
                    };
                    if (!isAddButton)
                    {
                        cbxPanel.Controls.Add(subButton);
                        subButton.Click += (s, e) =>
                        {
                            if (subButton.Tag != null)
                            {
                                ShowSubCommandMenu((GpibCommand) subButton.Tag, subButton);
                            }
                        };
                        isAddButton = true;
                    }
                    foreach (XmlElement groupElement in confContentEle.ChildNodes)
                    {
                        if (groupElement.LocalName.ToLower() != "group")
                            continue;
                        if (!groupElement.HasChildNodes)
                            continue;
                        //将所有命令解析成链式后，置入Tag中，待显示时再进行链式生成菜单
                        GpibCommand groupCmd = ParseGpibCommand(isScpi, groupElement, rootCmd.Command);
                        foreach (XmlElement gpElement in groupElement.ChildNodes)
                        {
                            GpibCommand gpCmd = ParseGpibCommand(isScpi, gpElement, groupCmd.Command);
                            groupCmd.Next = gpCmd;
                        }
                        cmd.Tag = groupCmd;
                    }

                    #endregion

                    #endregion
                }
                index++;
            }
            _Panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private static Button GetSubButton()
        {
            return new Button
            {
                BackgroundImage = Resources.arrow_triangle_down,
                BackgroundImageLayout = ImageLayout.Center,
                FlatStyle = FlatStyle.Popup,
                Dock = DockStyle.Right,
                Width = 24,
                Height = 22
            };
        }

        private void ShowSubCommandMenu(GpibCommand command, Control control)
        {
            var menu = new ContextMenuStrip();
            var menuItem = new ToolStripMenuItem(command.Content);
            Next(command, menuItem);

            menu.Items.Add(menuItem);
            menu.Show(control, new Point(1, control.Height));
        }

        /// <summary>
        /// 递归生成菜单项
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="parentMenu">父菜单</param>
        private void Next(GpibCommand command, ToolStripMenuItem parentMenu)
        {
            if (command.Next != null)
            {
                var menu = new ToolStripMenuItem(command.Content);
                parentMenu.DropDownItems.Add(menu);
                Next(command.Next, menu);
            }
        }

        private static GpibCommand ParseGpibCommand(bool isScpi, XmlElement element, string rootCmd)
        {
            var cmd = new GpibCommand(isScpi);
            cmd.Content = element.GetAttribute("content");
            if (element.LocalName == "command")
                cmd.Command = string.Format("{0}:{1}", rootCmd, element.GetAttribute("command"));
            else if (element.LocalName == "param")
                cmd.Command = string.Format("{0} {1}", rootCmd, element.GetAttribute("command"));
            return cmd;
        }

        private void GetLabel(string content, int index)
        {
            var label = new Label();
            label.AutoSize = true;
            label.Text = content;
            label.Anchor = AnchorStyles.Right;
            _Panel.Controls.Add(label, 0, index);
        }

        private ComboBox GetComboBox(int index, out Panel cbxPanel)
        {
            cbxPanel = new Panel();
            cbxPanel.Dock = DockStyle.Fill;
            //cbxPanel.BackColor = Color.Red;
            var cbx = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 172,
                Location = new Point(1, 1),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            cbxPanel.Controls.Add(cbx);
            _ComboBoxList.Add(cbx);
            _Panel.Controls.Add(cbxPanel, 1, index);
            return cbx;
        }
    }
}