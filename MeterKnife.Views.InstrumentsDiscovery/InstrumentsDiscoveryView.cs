using System;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.ViewModels;
using MeterKnife.Views.InstrumentsDiscovery.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.InstrumentsDiscovery
{
    public partial class InstrumentsDiscoveryView : DockContent
    {
        private readonly InstrumentsDiscoveryViewModel _ViewModel = new InstrumentsDiscoveryViewModel();

        public InstrumentsDiscoveryView()
        {
            InitializeComponent();
        }

        public void SetProvider(IExtenderProvider extenderProvider)
        {
            _ViewModel.SetProvider(extenderProvider);
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            foreach (var discover in _ViewModel.Discovers)
            {
                var panel = new InstrumentsListPanel();
                var head = new InstrumentsListHead();
                head.GatewayModel = discover.GatewayModel.ToString();
                panel.Controls.Add(head);
                panel.AddInstruments(discover.Instruments.ToArray());
                _LeftPanel.Controls.Add(panel);
            }
        }

        #endregion
    }
}
