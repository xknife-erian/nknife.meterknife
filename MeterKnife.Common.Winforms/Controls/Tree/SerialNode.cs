using MeterKnife.Common.DataModels;

namespace MeterKnife.Workbench.Controls.Tree
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