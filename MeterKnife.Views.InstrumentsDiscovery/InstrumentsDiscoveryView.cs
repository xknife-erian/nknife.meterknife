using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Models;
using MeterKnife.ViewModels;
using MeterKnife.Views.InstrumentsDiscovery.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.InstrumentsDiscovery
{
    public partial class InstrumentsDiscoveryView : DockContent
    {
        private readonly Dictionary<GatewayModel, InstrumentsListPanel> _PanelMap = new Dictionary<GatewayModel, InstrumentsListPanel>();
        private readonly InstrumentsDiscoveryViewModel _ViewModel = new InstrumentsDiscoveryViewModel();

        public InstrumentsDiscoveryView()
        {
            InitializeComponent();
            ViewModelPropertiesChangedManager();
            foreach (var discover in _ViewModel.DiscoverMap.Values)
            {
                OnInstrumentsCollectionChanged(discover);
            }
        }

        public void SetProvider(IExtenderProvider extenderProvider)
        {
            _ViewModel.SetProvider(extenderProvider);
        }

        private void ViewModelPropertiesChangedManager()
        {
            _ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(_ViewModel.SelectedInstrument):
                        MessageBox.Show($"{_ViewModel.SelectedInstrument.Id}");
                        break;
                }
            };
        }

        /// <summary>
        /// 在OnShown事件执行后初始化仪器管理容器
        /// </summary>
        private void InitializeInstrumentsContainer()
        {
            foreach (var pair in _ViewModel.DiscoverMap)
            {
                var model = pair.Key;
                var discover = pair.Value;

                var menuitem = new ToolStripMenuItem();
                menuitem.Text = $"{model}";
                menuitem.Click += (s, r) => _ViewModel.CreateInstrument(model);
                _AddDropDownButton.DropDownItems.Add(menuitem);

                var panel = new InstrumentsListPanel(discover);
                panel.Dock = DockStyle.Top;
                panel.AddInstruments(discover.Instruments.ToArray());
                panel.Count = discover.Instruments.Count;
                OnPanelEventTriggered(panel);

                _LeftContentPanel.Controls.Add(panel);
                _PanelMap.Add(model, panel);
            }
        }

        private void OnInstrumentSelected(object sender, CellClickEventArgs e)
        {
            _ViewModel.SelectedInstrument = e.Instrument;
        }

        private void OnInstrumentsCollectionChanged(IGatewayDiscover discover)
        {
            discover.Instruments.CollectionChanged += (s, e) =>
            {
                var panel = _PanelMap[discover.GatewayModel];
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    {
                        var insts = new Instrument[e.NewItems.Count];
                        for (var i = 0; i < e.NewItems.Count; i++)
                            insts[i] = (Instrument) e.NewItems[i];
                        panel.AddInstruments(insts);
                        break;
                    }
                    case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (Instrument inst in e.OldItems)
                            panel.RemoveInstrument(inst);
                        break;
                    }
                    case NotifyCollectionChangedAction.Move:
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Reset:
                        break;
                }
                panel.Count = discover.Instruments.Count;
            };
        }

        private void OnPanelEventTriggered(InstrumentsListPanel panel)
        {
            panel.GatewayModelRefreshInstrumentsState += (s, e) =>
            {
                _ViewModel.RefreshInstrumentStateByGateway((GatewayModel)((ToolStripMenuItem)s).Tag);
            };
            panel.GatewayModelDelete += (s, e) =>
            {
                _ViewModel.GatewayModelDelete((GatewayModel)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentDelete += (s, e) =>
            {
                var discover = (IGatewayDiscover) panel.Tag;
                _ViewModel.DeleteInstrument(discover.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentCommandManager += (s, e) =>
            {
                var discover = (IGatewayDiscover)panel.Tag;
                _ViewModel.InstrumentCommandManager(discover.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentConnectionTest += (s, e) =>
            {
                var discover = (IGatewayDiscover)panel.Tag;
                _ViewModel.InstrumentConnectionTest(discover.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentDatasManager += (s, e) =>
            {
                var discover = (IGatewayDiscover)panel.Tag;
                _ViewModel.InstrumentDatasManager(discover.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentSelected += OnInstrumentSelected;
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            InitializeInstrumentsContainer();
        }

        #endregion
    }
}