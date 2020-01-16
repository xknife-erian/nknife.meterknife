using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class LanNode : InterfaceNode
    {
        public LanNode(IMeterKernel meterKernel, AddMeterDialog dialog) : base(meterKernel, dialog)
        {
        }

        protected override void BuildContextMenu()
        {
        }
    }
}