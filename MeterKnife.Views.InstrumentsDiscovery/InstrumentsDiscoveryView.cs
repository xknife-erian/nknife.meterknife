using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views.InstrumentsDiscovery
{
    public partial class InstrumentsDiscoveryView : DockContent
    {
        public InstrumentsDiscoveryView()
        {
            InitializeComponent();
        }

        public void SetProvider(IExtenderProvider extenderProvider)
        {
        }
    }
}
