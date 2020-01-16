using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class MeterNode : BaseTreeNode
    {
        public MeterNode(IMeterKernel kernel) : base(kernel)
        {
            ImageKey = MeterTreeElement.Meter;
            SelectedImageKey = MeterTreeElement.Meter;
        }

        public BaseMeter Meter { get; set; }
    }
}