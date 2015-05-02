using System;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.Tunnels.CareOne;

namespace MeterKnife.Common.Controls.Tree
{
    public abstract class InterfaceNode : BaseTreeNode
    {
        protected readonly ContextMenu _RightMenu;
        protected readonly MenuItem _AddMeterMenu;

        public int Port { get; set; }

        protected InterfaceNode()
        {
            _RightMenu = new ContextMenu();
            _AddMeterMenu = new MenuItem("新建仪器");
            _RightMenu.MenuItems.Add(_AddMeterMenu);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            BuildContextMenu();
            NodeClicked += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    _RightMenu.Show(this.TreeView, e.Location);
                }
            };
        }

        protected abstract void BuildContextMenu();

        public CareOneProtocolHandler Handler { get; set; }

        public event EventHandler<MouseEventArgs> NodeClicked;

        protected internal virtual void OnNodeClicked(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = NodeClicked;
            if (handler != null) handler(this, e);
        }
    }
}