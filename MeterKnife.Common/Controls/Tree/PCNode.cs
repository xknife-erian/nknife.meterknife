using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Controls.Tree
{
    public class PCNode : BaseTreeNode
    {
        public PCNode()
        {
            ImageKey = MeterTreeElement.PC;
            SelectedImageKey = MeterTreeElement.PC;
            Text = Dns.GetHostName();
        }
    }
}
