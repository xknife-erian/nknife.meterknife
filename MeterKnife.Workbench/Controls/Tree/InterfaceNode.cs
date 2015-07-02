using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Workbench.Dialogs;
using NKnife.IoC;

namespace MeterKnife.Workbench.Controls.Tree
{
    public abstract class InterfaceNode : BaseTreeNode
    {
        private static readonly ILog _logger = LogManager.GetLogger<InterfaceNode>();

        protected readonly ToolStripMenuItem _AddMeterMenu;
        protected readonly ContextMenuStrip _RightMenu;

        protected InterfaceNode()
        {
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

        public CarePort Port { get; set; }

        private void AddMeterMenuOnClick(object sender, EventArgs eventArgs)
        {
            Dictionary<CarePort, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            List<int> gpibList;
            if (!dic.TryGetValue(Port, out gpibList))
            {
                gpibList = new List<int>();
                dic.Add(Port, gpibList);
            }
            var dialog = new AddMeterDialog();
            dialog.GpibList.AddRange(gpibList);
            dialog.Port = Port;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var meterNode = new MeterNode();
                meterNode.Text = !string.IsNullOrEmpty(dialog.Meter.Brand) 
                    ? string.Format("[{0}] {1},{2}", dialog.GpibAddress, dialog.Meter.Brand, dialog.Meter.Name) 
                    : string.Format("[{0}] {1}", dialog.GpibAddress, dialog.Meter.Name);
                meterNode.Meter = dialog.Meter;
                
                TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
                TreeView.ThreadSafeInvoke(Expand);
                gpibList.Add(dialog.GpibAddress);
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