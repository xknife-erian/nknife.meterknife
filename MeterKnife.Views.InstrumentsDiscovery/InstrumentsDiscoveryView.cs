using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.ViewModels;
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
    }
}
