using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Winforms.Dialogs;

namespace MeterKnife.Common.Winforms.Controls.Tree
{
    public abstract class InterfaceNode : BaseTreeNode
    {
        private static readonly ILog _logger = LogManager.GetLogger<InterfaceNode>();

        protected readonly ToolStripMenuItem _AddMeterMenu;
        protected readonly ContextMenuStrip _RightMenu;
        private AddMeterDialog _addMeterDialog;

        protected InterfaceNode(IMeterKernel meterKernel, AddMeterDialog addMeterDialog) : base(meterKernel)
        {
            _addMeterDialog = addMeterDialog;
            _RightMenu = new ContextMenuStrip();
            _AddMeterMenu = new ToolStripMenuItem("新建仪器");
            _AddMeterMenu.Click += AddMeterMenuOnClick;
            _RightMenu.Items.Add(_AddMeterMenu);
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

        public CommPort Port { get; set; }

        private void AddMeterMenuOnClick(object sender, EventArgs eventArgs)
        {
            Dictionary<CommPort, List<int>> dic = _meterKernel.GpibDictionary;
            List<int> gpibList;
            if (!dic.TryGetValue(Port, out gpibList))
            {
                gpibList = new List<int>();
                dic.Add(Port, gpibList);
            }
            _addMeterDialog.GpibList.AddRange(gpibList);
            _addMeterDialog.Port = Port;

            if (_addMeterDialog.ShowDialog() == DialogResult.OK)
            {
                var meterNode = new MeterNode();
                meterNode.Text = !string.IsNullOrEmpty(_addMeterDialog.Meter.Brand) 
                    ? string.Format("[{0}] {1},{2}", _addMeterDialog.GpibAddress, _addMeterDialog.Meter.Brand, _addMeterDialog.Meter.Name) 
                    : string.Format("[{0}] {1}", _addMeterDialog.GpibAddress, _addMeterDialog.Meter.Name);
                meterNode.Meter = _addMeterDialog.Meter;
                
                TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
                TreeView.ThreadSafeInvoke(Expand);
                gpibList.Add(_addMeterDialog.GpibAddress);
            }
        }

        protected abstract void BuildContextMenu();

        public event EventHandler<MouseEventArgs> NodeClicked;

        protected internal virtual void OnNodeClicked(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = NodeClicked;
            if (handler != null)
                handler(this, e);
        }
    }
}