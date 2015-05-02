using System;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Workbench.Dialogs;
using NKnife.Events;
using NKnife.IoC;

namespace MeterKnife.Workbench.Controls.Tree
{
    public abstract class InterfaceNode : BaseTreeNode
    {
        protected readonly MenuItem _AddMeterMenu;
        protected readonly BaseCareCommunicationService _CommService = DI.Get<BaseCareCommunicationService>();
        protected readonly ContextMenu _RightMenu;

        private int _CurrGpib;

        protected InterfaceNode()
        {
            _RightMenu = new ContextMenu();
            _AddMeterMenu = new MenuItem("新建仪器");
            _AddMeterMenu.Click += AddMeterMenuOnClick;
            _RightMenu.MenuItems.Add(_AddMeterMenu);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            BuildContextMenu();
            NodeClicked += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    _RightMenu.Show(TreeView, e.Location);
                }
            };
        }

        public int Port { get; set; }
        public CareOneProtocolHandler Handler { get; set; }

        private void AddMeterMenuOnClick(object sender, EventArgs eventArgs)
        {
            var dialog = new AddGpibMeterDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _CurrGpib = dialog.GpibAddress;
                var handler = (ScpiProtocolHandler) _CommService.CareHandlers[Port];
                handler.ProtocolRecevied += HandlerOnProtocolRecevied;

                var careSaying = new CareSaying();
                careSaying.MainCommand = 0xAA;
                careSaying.SubCommand = 0x00;
                careSaying.Content = "*IDN?";
                careSaying.GpibAddress = (byte) _CurrGpib;
                byte[] data = careSaying.Generate();

                _CommService.Send(Port, data);
            }
        }

        private void HandlerOnProtocolRecevied(object sender, EventArgs<string> e)
        {
            var meterNode = new MeterNode {Text = string.Format("{0}-{1}", _CurrGpib, e.Item)};
            TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
            ((ScpiProtocolHandler) _CommService.CareHandlers[Port]).ProtocolRecevied -= HandlerOnProtocolRecevied;
            TreeView.ThreadSafeInvoke(Expand);
        }

        protected abstract void BuildContextMenu();

        public event EventHandler<MouseEventArgs> NodeClicked;

        protected internal virtual void OnNodeClicked(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = NodeClicked;
            if (handler != null) handler(this, e);
        }
    }
}