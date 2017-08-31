﻿using System;
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
                    OnHeadLeftMouseClicked();
                    break;
                }
                case MouseButtons.Right:
                {
                    _ContextMenuStrip.Show(this, e.Location);
                    break;
                }
            }
        }

        private void HeadMouseLeave(object sender, EventArgs e)
        {
            _Panel.BackColor = SystemColors.ControlDark;
        }

        private void HeadMouseEnter(object sender, EventArgs e)
        {
            _Panel.BackColor = Color.Yellow;
        }

        public event EventHandler HeadLeftMouseClicked;

        protected virtual void OnHeadLeftMouseClicked()
        {
            HeadLeftMouseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}