using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Views.InstrumentsDiscovery.Properties;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsListHead : UserControl
    {
        private bool _IsDown = true;

        public InstrumentsListHead()
        {
            InitializeComponent();
            _GatewayModelLabel.Click += HeadClick;
            _Panel.Click += HeadClick;
            _CountLabel.Click += HeadClick;
            _PictureBox.Click += HeadClick;

            _GatewayModelLabel.MouseHover += HeadMouseHover;
            _Panel.MouseHover += HeadMouseHover;
            _CountLabel.MouseHover += HeadMouseHover;
            _PictureBox.MouseHover += HeadMouseHover;

            _GatewayModelLabel.MouseLeave += HeadMouseLeave;
            _Panel.MouseLeave += HeadMouseLeave;
            _CountLabel.MouseLeave += HeadMouseLeave;
            _PictureBox.MouseLeave += HeadMouseLeave;
        }

        private void HeadMouseLeave(object sender, EventArgs e)
        {
            _Panel.BackColor = SystemColors.ControlDark;
        }

        private void HeadMouseHover(object sender, EventArgs e)
        {
            _Panel.BackColor = Color.Plum;
        }

        public string GatewayModel
        {
            get => _GatewayModelLabel.Text;
            set => _GatewayModelLabel.Text = value;
        }

        public int Count
        {
            get => int.Parse(_CountLabel.Text);
            set => _CountLabel.Text = value.ToString();
        }

        private void HeadClick(object sender, EventArgs e)
        {
            _IsDown = !_IsDown;
            _PictureBox.BackgroundImage = !_IsDown ? Resources.down : Resources.up;
            OnHeadClicked();
        }

        public event EventHandler HeadClicked;

        protected virtual void OnHeadClicked()
        {
            HeadClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}