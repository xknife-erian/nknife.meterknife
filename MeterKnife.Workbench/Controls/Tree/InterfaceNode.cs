using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Tunnels.CareOne;
using MeterKnife.Common.Util;
using MeterKnife.Workbench.Dialogs;
using NKnife.Events;
using NKnife.IoC;

namespace MeterKnife.Workbench.Controls.Tree
{
    public abstract class InterfaceNode : BaseTreeNode
    {
        private static readonly ILog _logger = LogManager.GetLogger<InterfaceNode>();

        protected readonly ToolStripMenuItem _AddMeterMenu;
        protected readonly BaseCareCommunicationService _CommService = DI.Get<BaseCareCommunicationService>();
        protected readonly ContextMenuStrip _RightMenu;
        private MeterInfo _MeterInfo;

        class MeterInfo
        {
            public int GpibAddress { get; set; }
            public string Brand { get; set; }
            public string Name { get; set; }
            public GpibLanguage Language { get; set; }
        }

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

        public int Port { get; set; }
        public CareOneProtocolHandler Handler { get; set; }

        private void AddMeterMenuOnClick(object sender, EventArgs eventArgs)
        {
            var dialog = new AddGpibMeterDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var dic = DI.Get<IMeterKernel>().GpibDictionary;
                List<int> gpibList;
                if (!dic.TryGetValue(Port, out gpibList))
                {
                    gpibList = new List<int>();
                    dic.Add(Port, gpibList);
                }
                if (gpibList.Contains(dialog.GpibAddress))
                {
                    MessageBox.Show(TreeView.FindForm(), "请重新输入GPIB地址，该地址已有仪器占用。", "重复的GPIB地址",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                _MeterInfo = new MeterInfo();
                _MeterInfo.Brand = dialog.Brand;
                _MeterInfo.Name = dialog.MeterName;
                _MeterInfo.GpibAddress = dialog.GpibAddress;
                _MeterInfo.Language = dialog.Language;
                if (dialog.AutoFindMeter)
                {
                    var handler = (CareConfigHandler) _CommService.CareHandlers[Port];
                    handler.CareConfigging += HandlerOnProtocolRecevied;

                    var careTalking = CareTalking.IDN(_MeterInfo.GpibAddress);
                    _logger.Debug(string.Format("Send:{0}", careTalking.Scpi));
                    var data = careTalking.Generate();
                    _CommService.Send(Port, data);//08 17 07 AA 00 2A 49 44 4E 3F
                }
                else //当手动选择仪器类型时
                {
                    var meterNode = new MeterNode
                    {
                        Text = string.Format("[{0}] {1},{2}", _MeterInfo.GpibAddress, dialog.Brand, dialog.MeterName),
                    };
                    var meter = DI.Get<BaseMeter>();
                    meter.Brand = dialog.Brand;
                    meter.GpibAddress = dialog.GpibAddress;
                    meter.Language = dialog.Language;
                    meter.Name = string.Format("{0},{1}", dialog.Brand, dialog.MeterName);
                    meterNode.Meter = meter;

                    TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
                    TreeView.ThreadSafeInvoke(Expand);
                }
                gpibList.Add(dialog.GpibAddress);
            }
        }

        private void HandlerOnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            var handler = ((CareConfigHandler)_CommService.CareHandlers[Port]);
            handler.CareConfigging -= HandlerOnProtocolRecevied;

            var idnName = e.Item.Scpi;
            var meterNode = new MeterNode();

            var simpleName = MeterUtil.SimplifyName(idnName);
            var meterName = MeterUtil.Named(simpleName);
            _logger.Info(string.Format("准备创建仪器:{0}", meterName));
            BaseMeter meter;
            try
            {
                meter = DI.Get<BaseMeter>(meterName);
                meter.Name = idnName;
                meter.Brand = simpleName.First;
            }
            catch (Exception ex)
            {
                _logger.WarnFormat("未找到{0}的仪器", idnName, ex.Message);
                meter = DI.Get<BaseMeter>();
                meter.Name = "Unknown Meter";
            }
            meter.GpibAddress = _MeterInfo.GpibAddress;
            meter.Language = _MeterInfo.Language;
            meterNode.Meter = meter;
            meterNode.Text = string.Format("[{0}] {1}", _MeterInfo.GpibAddress, meter.Name);
            TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
            TreeView.ThreadSafeInvoke(Expand);
        }

        protected abstract void BuildContextMenu();

        public event EventHandler<MouseEventArgs> NodeClicked;

        protected internal virtual void OnNodeClicked(MouseEventArgs e)
        {
            var handler = NodeClicked;
            if (handler != null) handler(this, e);
        }
    }
}