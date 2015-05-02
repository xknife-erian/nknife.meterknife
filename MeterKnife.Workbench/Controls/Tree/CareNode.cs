using System.Windows.Forms;
using MeterKnife.Common.DataModels;

namespace MeterKnife.Workbench.Controls.Tree
{
    public class CareNode : SerialNode
    {
        protected MenuItem _OfflineCollectSettingMenu;

        public CareNode()
        {
            ImageKey = MeterTreeElement.Care;
            SelectedImageKey = MeterTreeElement.Care;
        }

        protected override void BuildContextMenu()
        {
            _OfflineCollectSettingMenu = new MenuItem("设置自动采集");
            _RightMenu.MenuItems.Add(_OfflineCollectSettingMenu);
        }
    }
}