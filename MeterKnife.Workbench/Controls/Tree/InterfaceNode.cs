using System;
using System.Windows.Forms;
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
        protected readonly MenuItem _AddMeterMenu;
        protected readonly BaseCareCommunicationService _CommService = DI.Get<BaseCareCommunicationService>();
        protected readonly ContextMenu _RightMenu;
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
                if (dialog.AutoFindMeter)
                {
                    _MeterInfo = new MeterInfo();
                    _MeterInfo.Brand = dialog.Brand;
                    _MeterInfo.Name = dialog.MeterName;
                    _MeterInfo.GpibAddress = dialog.GpibAddress;
                    _MeterInfo.Language = dialog.Language;

                    var handler = (ScpiProtocolHandler) _CommService.CareHandlers[Port];
                    handler.ProtocolRecevied += HandlerOnProtocolRecevied;

                    var careSaying = CareSaying.IDN(_MeterInfo.GpibAddress);
                    var data = careSaying.Generate();

                    _CommService.Send(Port, data);
                }
                else//当手动选择仪器类型时
                {
                    var meterNode = new MeterNode
                    {
                        Text = string.Format("{0}-{1},{2}", _MeterInfo.GpibAddress, dialog.Brand, dialog.Name),
                    };
                    var meter = DI.Get<BaseMeter>(dialog.Name);
                    meter.Brand = dialog.Brand;
                    meter.GpibAddress = dialog.GpibAddress;
                    meter.Language = dialog.Language;
                    meter.Name = string.Format("{0},{1}", dialog.Brand, dialog.Name);

                    TreeView.ThreadSafeInvoke(() => Nodes.Add(meterNode));
                    TreeView.ThreadSafeInvoke(Expand);
                }
            }
        }

        private void HandlerOnProtocolRecevied(object sender, EventArgs<CareSaying> e)
        {
            ((ScpiProtocolHandler) _CommService.CareHandlers[Port]).ProtocolRecevied -= HandlerOnProtocolRecevied;

            var meterName = e.Item.Content;
            var meterNode = new MeterNode
            {
                Text = string.Format("{0}-{1}", _MeterInfo.GpibAddress, meterName)
            };

            var name = MeterUtil.SimplifyName(meterName);
            var meter = DI.Get<BaseMeter>(name.Second);
            meter.GpibAddress = _MeterInfo.GpibAddress;
            meter.Name = meterName;
            meter.Brand = name.First;
            meter.Language = _MeterInfo.Language;
            meterNode.Meter = meter;

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