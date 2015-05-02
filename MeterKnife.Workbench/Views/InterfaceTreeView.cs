using System;
using System.Collections.Specialized;
using System.Threading;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls.Tree;
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
            var thread = new Thread(UpdateCareNode);
            thread.Start();
        }

        private void UpdateCareNode()
        {
            int i = 1;
            var careComm = DI.Get<BaseCareCommunicationService>();
            while (!careComm.IsInitialized)
            {
                if (i > 100)
                {
                    _logger.Error("Care通讯服务异常，始终未能初始化完成");
                    return;
                }
                Thread.Sleep(100);
                _logger.Debug(string.Format("Care通讯服务未完成，等待{0}次", i));
                i++;
            }
            Thread.Sleep(200);

            StringCollection comlist = PcInterfaces.GetSerialList();
            foreach (string serial in comlist)
            {
                string com = serial.ToUpper().TrimStart(new[] {'C', 'O', 'M'});
                int port = 0;
                if (int.TryParse(com, out port))
                {
                    SerialNode node;
                    if (!careComm.CareHandlers.ContainsKey(port))
                    {
                        node = new SerialNode();
                        node.Text = serial;
                    }
                    else
                    {
                        node = new CareNode();
                        node.Text = string.Format("Care[{0}]", serial);
                    }
                    node.Port = port;
                    _MeterTree.ThreadSafeInvoke(() => _MeterTree.RootNode.Nodes.Add(node));
                }
            }
            _MeterTree.ThreadSafeInvoke(()=>_MeterTree.ExpandAll());
        }
    }
}