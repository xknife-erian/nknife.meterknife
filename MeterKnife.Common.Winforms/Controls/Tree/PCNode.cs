using System.Net;
using MerterKnife.Common.Winforms.Controls.Tree;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Workbench.Controls.Tree
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
