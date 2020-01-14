using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Winforms.Controls.Tree;
using MeterKnife.Instruments;
using NKnife.Configuring.UserData;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Utility;
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
            var thread = new Task(UpdateTreeNode);
            thread.Start();
        }

        private void MeterTreeOnSelectedMeter(object sender, InterfaceNodeClickedEventArgs e)
        {
            var dic = DI.Get<IMeterKernel>().MeterContents;
            DockContent dockContent;
            if (!dic.TryGetValue(e.Meter, out dockContent))
            {
                var meterView = DI.Get<DigitMultiMeterView>();
                meterView.Port = e.Port;
                meterView.SetMeter(e.Port, e.Meter);
                meterView.Text = e.Meter.AbbrName;
                dic.Add(e.Meter, meterView);
                dockContent = meterView;
            }
            dockContent.Show(PanelPane.DockPanel, DockState.Document);
            _logger.InfoFormat("由仪器树面板创建仪器{0}实时窗体", e.Meter.AbbrName);
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
                    CommPort carePort = CommPort.Build(TunnelType.Serial, port.ToString());
                    SerialNode node = !careComm.Cares.Contains(carePort) 
                        ? new SerialNode {Text = serial} 
                        : new CareNode {Text = string.Format("Care [{0}]", serial)};
                    node.Port = carePort;
                    _MeterTree.ThreadSafeInvoke(() => _MeterTree.RootNode.Nodes.Add(node));
                }
            }
            _MeterTree.ThreadSafeInvoke(() => _MeterTree.ExpandAll());
        }
    }
}