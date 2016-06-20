using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class MeterNode : BaseTreeNode
    {
        public MeterNode()
        {
            ImageKey = MeterTreeElement.Meter;
            SelectedImageKey = MeterTreeElement.Meter;
        }

        public BaseMeter Meter { get; set; }
    }
}