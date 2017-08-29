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
            foreach (var pair in _ViewModel.Discovers)
            {
                var model = pair.Key;
                var list = pair.Value;
                list.CollectionChanged += (s, e) =>
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        {
                            var insts = new Instrument[e.NewItems.Count];
                            for (var i = 0; i < e.NewItems.Count; i++)
                                insts[i] = (Instrument) e.NewItems[i];
                            var panel = _PanelMap[model];
                            panel.AddInstruments(insts);
                            break;
                        }
                        case NotifyCollectionChangedAction.Move:
                        case NotifyCollectionChangedAction.Remove:
                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                };
            }
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            foreach (var pair in _ViewModel.Discovers)
            {
                var model = pair.Key;
                var instruments = pair.Value;

                var panel = new InstrumentsListPanel();
                panel.GatewayModel = model.ToString();
                panel.Dock = DockStyle.Top;
                panel.AddInstruments(instruments.ToArray());
                panel.Count = instruments.Count;
                _LeftContentPanel.Controls.Add(panel);
                _PanelMap.Add(model, panel);

                var menuitem = new ToolStripMenuItem();
                menuitem.Text = $"{model}";
                menuitem.Click += (s, r) => _ViewModel.AddInstrument(model);
                _AddDropDownButton.DropDownItems.Add(menuitem);
            }
        }

        #endregion
    }
}