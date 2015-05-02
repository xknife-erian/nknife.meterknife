using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using SerialKnife.Generic.Filters;

namespace MeterKnife.Common.Controls.Tree
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