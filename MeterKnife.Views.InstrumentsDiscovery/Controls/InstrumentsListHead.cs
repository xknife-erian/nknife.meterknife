using System;
using System.Windows.Forms;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsListHead : UserControl
    {
        public InstrumentsListHead()
        {
            InitializeComponent();
            _GatewayModelLabel.Click += HeadClick;
            _Panel.Click += HeadClick;
            _CountLabel.Click += HeadClick;
            _PictureBox.Click += HeadClick;
        }

        public string GatewayModel
        {
            get => _GatewayModelLabel.Text;
            set => _GatewayModelLabel.Text = value;
        }

        private void HeadClick(object sender, EventArgs e)
        {
            OnHeadClicked();
        }

        public event EventHandler HeadClicked;

        protected virtual void OnHeadClicked()
        {
            HeadClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}