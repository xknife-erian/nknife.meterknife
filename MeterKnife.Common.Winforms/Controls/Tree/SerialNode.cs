using MeterKnife.Common.DataModels;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public class SerialNode : InterfaceNode
    {
        public SerialNode()
        {
            ImageKey = MeterTreeElement.Serial;
            SelectedImageKey = MeterTreeElement.Serial;
        }

        protected override void BuildContextMenu()
        {
        }
    }
}