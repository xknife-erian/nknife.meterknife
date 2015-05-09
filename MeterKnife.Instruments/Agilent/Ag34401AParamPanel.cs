using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Instruments.Agilent
{
    public sealed class Ag34401AParamPanel : BaseParamPanel
    {
        public Ag34401AParamPanel(XmlElement element)
        {
            Dock = DockStyle.Fill;
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

        private GpibCommandList _Commandlist;
        private void FillCommandList()
        {
            _Commandlist = GetGpibCommandList();
            foreach (var comboBox in _ComboBox)
            {
                if (comboBox.SelectedItem == null)
                    continue;
                var cmd = (GpibCommand)(comboBox.SelectedItem);
                _Commandlist.AddLast(cmd);
            }
        }

        private static GpibCommandList GetGpibCommandList()
        {
            var cmdlist = new GpibCommandList();
            cmdlist.AddLast(new GpibCommand { Command = "*CLS", Interval = 50 });
            cmdlist.AddLast(new GpibCommand { Command = "*CLS", Interval = 50 });
            cmdlist.AddLast(new GpibCommand { Command = "*RST", Interval = 200 });
            cmdlist.AddLast(new GpibCommand { Command = "*CLS", Interval = 50 });
            cmdlist.AddLast(new GpibCommand { Command = "INIT", Interval = 50 });
            return cmdlist;
        }

        private readonly List<ComboBox> _ComboBox = new List<ComboBox>(); 

        private void ParseElement(XmlElement element)
        {
            string isScpiStr = element.GetAttribute("format");
            bool isScpi = true;
            bool.TryParse(isScpiStr, out isScpi);
            XmlNodeList nodes = element.SelectNodes("/MeterParam/command[@isConfig='true']");
            if (nodes != null)
            {
                int count = nodes.Count;
                _Panel.RowCount = count + 1;
                _Panel.RowStyles.Clear();
                //_Panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                float f = (float) 100/count;
                int i = 0;
                foreach (XmlElement configEle in nodes)
                {
                    var rootCmd = new GpibCommand(isScpi);
                    rootCmd.Content = configEle.GetAttribute("content");
                    rootCmd.Command = configEle.GetAttribute("command");

                    _Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    var label = new Label();
                    label.AutoSize = true;
                    label.Text = rootCmd.Content;
                    label.Anchor = AnchorStyles.Right;
                    _Panel.Controls.Add(label, 0, i);
                    if (configEle.HasChildNodes)
                    {
                        var cbx = new ComboBox
                        {
                            Width = 140,
                            Anchor = AnchorStyles.Left
                        };
                        foreach (XmlElement vNode in configEle.ChildNodes)
                        {
                            var cmd = new GpibCommand(isScpi);
                            cmd.Content = vNode.GetAttribute("content");
                            cmd.Command = string.Format("{0}:{1}", rootCmd.Command, vNode.GetAttribute("command"));

                            cbx.Items.Add(cmd);
                        }
                        _ComboBox.Add(cbx);
                        _Panel.Controls.Add(cbx, 1, i);
                    }
                    i++;
                }
                _Panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }
    }
}