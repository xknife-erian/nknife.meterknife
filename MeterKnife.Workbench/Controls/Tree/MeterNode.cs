using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Workbench.Controls.Tree
{
    public class MeterNode : BaseTreeNode
    {
        public MeterNode()
        {
            ImageKey = MeterTreeElement.Meter;
            SelectedImageKey = MeterTreeElement.Meter;
        }
    }
}