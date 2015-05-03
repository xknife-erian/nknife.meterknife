using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Workbench.Controls.Tree
{
    public class MeterNode : BaseTreeNode
    {
        public MeterNode()
        {
            ImageKey = MeterTreeElement.Meter;
            SelectedImageKey = MeterTreeElement.Meter;
        }

        public IMeter Meter { get; set; }
    }
}