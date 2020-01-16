using System.Net;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class PCNode : BaseTreeNode
    {
        public PCNode(IMeterKernel kernel):base(kernel)
        {
            ImageKey = MeterTreeElement.PC;
            SelectedImageKey = MeterTreeElement.PC;
            Text = Dns.GetHostName();
        }
    }
}
