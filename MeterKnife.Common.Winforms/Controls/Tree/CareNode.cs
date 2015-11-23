using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Workbench.Dialogs;

namespace MeterKnife.Workbench.Controls.Tree
{
    public class CareNode : SerialNode
    {
        protected ToolStripMenuItem _OfflineCollectSettingMenu;
        protected ToolStripMenuItem _CareSettingMenu;

        public CareNode()
        {
            ImageKey = MeterTreeElement.Care;
            SelectedImageKey = MeterTreeElement.Care;
        }

        protected override void BuildContextMenu()
        {
            _RightMenu.Items.Add(new ToolStripSeparator());
            _OfflineCollectSettingMenu = new ToolStripMenuItem("设置自动采集");
            _OfflineCollectSettingMenu.Click += (s, e) =>
            {
                var dialog = new OfflineCollectParameterDialog();
                dialog.ShowDialog(TreeView.FindForm());
            };
            _RightMenu.Items.Add(_OfflineCollectSettingMenu);
            _CareSettingMenu = new ToolStripMenuItem("Care属性");
            _CareSettingMenu.Click += (s, e) =>
            {
                var dialog = new CareParameterDialog(Port);
                dialog.ShowDialog(TreeView.FindForm());
            };
            _RightMenu.Items.Add(_CareSettingMenu);
        }
    }
}