using System.Net;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Winforms.Controls.Tree
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
