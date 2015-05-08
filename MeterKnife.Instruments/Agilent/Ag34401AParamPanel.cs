using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Instruments.Agilent
{
    public class Ag34401AParamPanel : BaseParamPanel
    {
        public Ag34401AParamPanel(XmlElement element)
        {
            foreach (var node in element.ChildNodes)
            {
                if (!(node is XmlElement))
                    continue;
                var ele = (XmlElement) node;
                if (ele.HasAttribute("isConfig"))
                {
                    var count = ele.ChildNodes.Count;
                    _Panel.RowCount = count;
                    _Panel.RowStyles.Clear();
                    _Panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    float f = (float) 100/count;
                    int i = 0;
                    foreach (XmlElement configEle in ele.ChildNodes)
                    {
                        _Panel.RowStyles.Add(new RowStyle(SizeType.Percent, f));
                        _Panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        var label = new Label();
                        label.Text = configEle.GetAttribute("description");
                        label.Anchor = AnchorStyles.None;
                        _Panel.Controls.Add(label, 0, i);
                        if (configEle.ChildNodes.Count == 0)
                        {
                            var textbox = new TextBox();
                            _Panel.Controls.Add(textbox, 1, i);
                        }
                        else
                        {
                            var cbx = new ComboBox();
                            _Panel.Controls.Add(cbx, 1, i);
                        }
                        i++;
                    }
                }
            }
            Dock = DockStyle.Fill;
        }

        public override GpibCommandList GpibCommands { get; set; }
    }
}
