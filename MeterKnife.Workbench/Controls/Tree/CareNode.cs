using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Workbench.Dialogs;

namespace MeterKnife.Workbench.Controls.Tree
{
    public class CareNode : SerialNode
    {
        protected MenuItem _OfflineCollectSettingMenu;
        protected MenuItem _CareSettingMenu;

        public CareNode()
        {
            ImageKey = MeterTreeElement.Care;
            SelectedImageKey = MeterTreeElement.Care;
        }

        protected override void BuildContextMenu()
        {
            _OfflineCollectSettingMenu = new MenuItem("设置自动采集");
            _OfflineCollectSettingMenu.Click += (s, e) =>
            {
                var dialog = new OfflineCollectParameterDialog();
                dialog.ShowDialog(TreeView.FindForm());
            };
            _RightMenu.MenuItems.Add(_OfflineCollectSettingMenu);
            _CareSettingMenu = new MenuItem("设置Care参数");
            _CareSettingMenu.Click += (s, e) =>
            {
                var dialog = new CareParameterDialog();
                dialog.ShowDialog(TreeView.FindForm());
            };
            _RightMenu.MenuItems.Add(_CareSettingMenu);
        }
    }
}