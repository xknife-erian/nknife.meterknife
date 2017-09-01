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
            ViewModelChange();
        }

        public void SetProvider(IExtenderProvider extenderProvider)
        {
            _ViewModel.SetProvider(extenderProvider);
        }

        private void ViewModelChange()
        {
            foreach (var pair in _ViewModel.InstrumentMap)
            {
                var model = pair.Key;
                var list = pair.Value;
                list.CollectionChanged += (s, e) =>
                {
                    var panel = _PanelMap[model];
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
                        case NotifyCollectionChangedAction.Move:
                        {
                            foreach (Instrument inst in e.OldItems)
                                panel.RemoveInstrument(inst);
                            break;
                        }
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                    panel.Count = list.Count;
                };
            }
        }

        private void TrigPanelEvent(InstrumentsListPanel panel)
        {
            panel.GatewayModelUpdate += (s, e) =>
            {
                _ViewModel.GatewayModelUpdate((GatewayModel)((ToolStripMenuItem)s).Tag);
            };
            panel.GatewayModelDelete += (s, e) =>
            {
                _ViewModel.GatewayModelDelete((GatewayModel)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentDelete += (s, e) =>
            {
                _ViewModel.DeleteInstrument(panel.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentCommandManager += (s, e) =>
            {
                _ViewModel.InstrumentCommandManager(panel.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentConnectionTest += (s, e) =>
            {
                _ViewModel.InstrumentConnectionTest(panel.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
            panel.InstrumentDatasManager += (s, e) =>
            {
                _ViewModel.InstrumentDatasManager(panel.GatewayModel, (Instrument)((ToolStripMenuItem)s).Tag);
            };
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            foreach (var pair in _ViewModel.InstrumentMap)
            {
                var model = pair.Key;
                var instruments = pair.Value;

                var panel = new InstrumentsListPanel
                {
                    GatewayModel = model,
                    Dock = DockStyle.Top
                };
                panel.AddInstruments(instruments.ToArray());
                panel.Count = instruments.Count;
                _LeftContentPanel.Controls.Add(panel);
                _PanelMap.Add(model, panel);
                TrigPanelEvent(panel);

                var menuitem = new ToolStripMenuItem();
                menuitem.Text = $"{model}";
                menuitem.Click += (s, r) => _ViewModel.CreateInstrument(model);
                _AddDropDownButton.DropDownItems.Add(menuitem);
            }
        }

        #endregion
    }
}