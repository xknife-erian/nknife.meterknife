using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Controls.Tree
{
    public class SerialNode : BaseTreeNode
    {
        public SerialNode()
        {
            ImageKey = MeterTreeElement.Serial;
            SelectedImageKey = MeterTreeElement.Serial;
        }

        public int Port { get; set; }
    }
}