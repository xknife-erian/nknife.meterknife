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
                panel.GatewayModel = discover.GatewayModel.ToString();
                panel.Dock = DockStyle.Top;
                panel.AddInstruments(discover.Instruments.ToArray());
                _LeftContentPanel.Controls.Add(panel);

                var menuitem = new ToolStripMenuItem();
                menuitem.Text = $"{discover.GatewayModel}";
                _AddDropDownButton.DropDownItems.Add(menuitem);
                menuitem.Click += (s, r) => discover.AddInstrument();
                discover.InstrumentAdded += (s, i) =>
                {
                    var inst = i.Instrument;
                    panel.AddInstruments(inst);
                    panel.Count++;
                };
            }
        }

        #endregion
    }
}
