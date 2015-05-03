using System;
using System.Collections.Specialized;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Instruments;
using MeterKnife.Workbench.Controls.Tree;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Wrapper;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Workbench.Views
{
    public partial class InterfaceTreeView : DockContent
    {
        private static readonly ILog _logger = LogManager.GetLogger<InterfaceTreeView>();

        public InterfaceTreeView()
        {
            InitializeComponent();
            _MeterTree.SelectedMeter+= MeterTreeOnSelectedMeter;
            //防止卡死界面，交由另一个线程去刷新树节点
            var thread = new Thread(UpdateTreeNode);
            thread.Start();
        }

        private void MeterTreeOnSelectedMeter(object sender, EventArgs<IMeter> e)
        {
            var collectView = DI.Get<CollectDataView>();
            collectView.Meter = e.Item;
            collectView.Text = e.Item.Name;
            collectView.Show(this.PanelPane.DockPanel, DockState.Document);
        }

        private void UpdateTreeNode()
        {
            int i = 1;
            var careComm = DI.Get<BaseCareCommunicationService>();
            while (!careComm.IsInitialized) //等待通讯服务初始化完成
            {
                if (i > 100)
                {
                    _logger.Error("Care通讯服务异常，始终未能初始化完成");
                    return;
                }
                Thread.Sleep(200);
                _logger.Trace(string.Format("Care通讯服务未完成，树刷新等待{0}次", i));
                i++;
            }
            Thread.Sleep(200);

            StringCollection comlist = PcInterfaces.GetSerialList();
            foreach (string serial in comlist)
            {
                string com = serial.ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                int port = 0;
                if (int.TryParse(com, out port) && port > 0)
                {
                    SerialNode node = !careComm.CareHandlers.ContainsKey(port)
                        ? new SerialNode {Text = serial}
                        : new CareNode {Text = string.Format("Care [{0}]", serial)};
                    node.Port = port;
                    if (careComm.CareHandlers.ContainsKey(port))
                        node.Handler = careComm.CareHandlers[port];
                    _MeterTree.ThreadSafeInvoke(() => _MeterTree.RootNode.Nodes.Add(node));
                }
            }
            _MeterTree.ThreadSafeInvoke(() => _MeterTree.ExpandAll());
        }
    }
}