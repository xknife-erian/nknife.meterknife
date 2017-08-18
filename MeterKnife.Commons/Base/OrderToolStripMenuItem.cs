using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MeterKnife.Base
{
    public class OrderToolStripMenuItem : ToolStripMenuItem
    {
        private bool _IsFirstDropDown = true;

        public OrderToolStripMenuItem() 
            : this("")
        {
        }

        public OrderToolStripMenuItem(string menuText)
            : base(menuText)
        {
            DropDownOpening += (s, e) =>
            {
                if (_IsFirstDropDown)
                {
                    _IsFirstDropDown = false;
                    var tmpItems = new List<ToolStripItem>();
                    foreach (ToolStripItem dropDownItem in DropDownItems)
                        tmpItems.Add(dropDownItem);
                    DropDownItems.Clear();

                    tmpItems.Sort(new MenuItemComparer());
                    foreach (var toolStripItem in tmpItems)
                    {
                        var order = ((OrderToolStripMenuItem)toolStripItem).Order;
                        if (order != 0 && order % 10 == 0)
                            DropDownItems.Add(new ToolStripSeparator());
                        DropDownItems.Add(toolStripItem);
                    }
                }
            };
        }

        public float Order { get; set; } = 0;

        public class MenuItemComparer : IComparer<ToolStripItem>
        {
            public int Compare(ToolStripItem m, ToolStripItem n)
            {
                if (m != null && n != null)
                {
                    var x = (OrderToolStripMenuItem) m;
                    var y = (OrderToolStripMenuItem) n;
                    var xx = (int) (x.Order * 10000);
                    var yy = (int) (y.Order * 10000);
                    return xx - yy;
                }
                return 0;
            }
        }
    }
}