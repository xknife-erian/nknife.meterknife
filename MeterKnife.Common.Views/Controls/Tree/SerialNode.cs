using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class SerialNode : InterfaceNode
    {
        public SerialNode(IMeterKernel meterKernel, AddMeterDialog dialog) : base(meterKernel, dialog)
        {
            ImageKey = MeterTreeElement.Serial;
            SelectedImageKey = MeterTreeElement.Serial;
        }

        protected override void BuildContextMenu()
        {
        }
    }
}