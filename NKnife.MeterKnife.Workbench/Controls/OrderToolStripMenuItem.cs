using System.Collections.Generic;
using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Controls
{
    public class OrderToolStripMenuItem : ToolStripMenuItem
    {
        private bool _isFirstDropDown = true;

        public OrderToolStripMenuItem()
            : this("")
        {
        }

        public OrderToolStripMenuItem(string menuText)
            : base(menuText)
        {
            DropDownOpening += (s, e) =>
            {
                if (_isFirstDropDown)
                {
                    _isFirstDropDown = false;
                    var tmpItems = new List<ToolStripItem>();
                    foreach (ToolStripItem dropDownItem in DropDownItems)
                        tmpItems.Add(dropDownItem);
                    DropDownItems.Clear();

                    tmpItems.Sort(new MenuItemComparer());
                    foreach (var item in tmpItems)
                    {
                        var order = ((OrderToolStripMenuItem) item).Order;
                        if (tmpItems.Count > 1 && order != 0 && order % 10 <= 0)
                            DropDownItems.Add(new ToolStripSeparator());
                        DropDownItems.Add(item);
                    }
                }
            };
        }

        public float Order { get; set; } = 0;

        public class MenuItemComparer : IComparer<ToolStripItem>
        {
            public int Compare(ToolStripItem m, ToolStripItem n)
            {
                var x = m as OrderToolStripMenuItem;
                var y = n as OrderToolStripMenuItem;
                if (x != null && y != null)
                {
                    var xx = (int) (x.Order * 10000);
                    var yy = (int) (y.Order * 10000);
                    return xx - yy;
                }
                return 0;
            }
        }
    }
}