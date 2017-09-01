using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Views.InstrumentsDiscovery.Properties;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsListHead : UserControl
    {
        private bool _IsDown = true;

        public InstrumentsListHead()
        {
            InitializeComponent();
            ControlSameEvent();
        }

        public string GatewayModel
        {
            get => _GatewayModelLabel.Text;
            set => _GatewayModelLabel.Text = value;
        }

        public int Count
        {
            get
            {
                var count = 0;
                int.TryParse(_CountLabel.Text, out count);
                return count;
            }
            set => _CountLabel.Text = value.ToString();
        }

        private void ControlSameEvent()
        {
            var array = new Control[_Panel.Controls.Count + 1];
            _Panel.Controls.CopyTo(array, 0);
            array[array.Length - 1] = _Panel;

            foreach (var control in array)
            {
                control.MouseClick += HeadMouseClick;
                control.MouseLeave += HeadMouseLeave;
                control.MouseEnter += HeadMouseEnter;
            }
        }

        private void HeadMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    _IsDown = !_IsDown;
                    //当点击时更替左侧的图形，给出上缩下拉的两种工作模式
                    _PictureBox.BackgroundImage = !_IsDown ? Resources.down : Resources.up;
                    break;
                }
            }
            var p = ((Control)sender).PointToScreen(e.Location);
            GatewayModel model;
            Enum.TryParse(GatewayModel, out model);
            var me = new HeadClickEventArgs(model, e.Button, e.Clicks, p.X, p.Y, e.Delta);
            OnHeadMouseClicked(me);
        }

        private void HeadMouseLeave(object sender, EventArgs e)
        {
            _Panel.BackColor = SystemColors.ControlDark;
        }

        private void HeadMouseEnter(object sender, EventArgs e)
        {
            _Panel.BackColor = Color.GreenYellow;
        }

        public event EventHandler<HeadClickEventArgs> HeadMouseClicked;

        protected virtual void OnHeadMouseClicked(HeadClickEventArgs e)
        {
            HeadMouseClicked?.Invoke(this, e);
        }
    }

    public class HeadClickEventArgs : MouseEventArgs
    {
        public GatewayModel GatewayModel { get; set; }

        /// <summary>初始化 <see cref="T:System.Windows.Forms.MouseEventArgs" /> 类的新实例。</summary>
        /// <param name="gatewayModel">测量途径</param>
        /// <param name="button">
        /// <see cref="T:System.Windows.Forms.MouseButtons" /> 值之一，它指示曾按下的是哪个鼠标按钮。</param>
        /// <param name="clicks">鼠标按钮曾被按下的次数。</param>
        /// <param name="x">鼠标单击的 x 坐标（以像素为单位）。</param>
        /// <param name="y">鼠标单击的 y 坐标（以像素为单位）。</param>
        /// <param name="delta">鼠标轮已转动的制动器数的有符号计数。</param>
        public HeadClickEventArgs(GatewayModel gatewayModel, MouseButtons button, int clicks, int x, int y, int delta) : base(button, clicks, x, y, delta)
        {
            GatewayModel = gatewayModel;
        }
    }
}