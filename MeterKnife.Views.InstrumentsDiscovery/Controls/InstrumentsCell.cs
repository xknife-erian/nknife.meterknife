using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Models;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsCell : UserControl
    {
        public InstrumentsCell()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            ControlSameEventManager();

            _MainPanel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                var pen = new Pen(Color.DarkGray);

                var leftTop = new Point(0, 0);
                var rightTop = new Point(_MainPanel.Width - 1, 0);
                var leftBottom = new Point(0, _MainPanel.Height - 1);
                var rightBottom = new Point(_MainPanel.Width - 1, _MainPanel.Height - 1);

                g.DrawLine(pen, leftTop, rightTop);
                g.DrawLine(pen, leftTop, leftBottom);
                g.DrawLine(pen, rightTop, rightBottom);
                g.DrawLine(pen, leftBottom, rightBottom);
            };
        }

        /// <summary>
        /// 当鼠标在控件上时，整个控件颜色发生变化
        /// </summary>
        private void ControlSameEventManager()
        {
            var list = new List<Control>();
            AddToControlList(_MainPanel, list);

            foreach (Control ctrl in list)
            {
                ctrl.MouseEnter += ControlMouseEnter;
                ctrl.MouseLeave += CoutrolMouseLeave;
                ctrl.MouseClick += ControlMouseClick;
            }
        }

        private void ControlMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                OnCellRightMouseClicked();
            }
        }

        private void CoutrolMouseLeave(object sender, EventArgs e)
        {
            _MainPanel.BackColor = SystemColors.ControlLight;
        }

        private void ControlMouseEnter(object sender, EventArgs e)
        {
            _MainPanel.BackColor = Color.LightGoldenrodYellow;
        }

        public event EventHandler CellRightMouseClicked;

        public void SetInstruments(Instrument instrument)
        {
            if (instrument != null)
            {
                Tag = instrument;
                if (instrument.Image != null)
                    Image = instrument.Image;
                Model = instrument.Model;
                Manufacturer = instrument.Manufacturer;
                Address = instrument.Address.ToString();
                Information = instrument.Information;
                DatasCount = instrument.DatasCount.ToString();
                UsingTime = instrument.LastUsingTime.ToString("yyyy/MM/dd");
            }
        }

        public Image Image
        {
            get => _PictureBox.BackgroundImage;
            set => _PictureBox.BackgroundImage = value;
        }

        public string Model
        {
            get => _ModelLabel.Text;
            set => _ModelLabel.Text = value;
        }

        public string Manufacturer
        {
            get => _ManufacturerLabel.Text;
            set => _ManufacturerLabel.Text = value;
        }

        public string Address
        {
            get => _AddressLabel.Text;
            set => _AddressLabel.Text = value;
        }

        public string Information
        {
            get => _InformationLabel.Text;
            set => _InformationLabel.Text = value;
        }

        public string DatasCount
        {
            get => _DatasCountLabel.Text;
            set => _DatasCountLabel.Text = value;
        }

        public string UsingTime
        {
            get => _UsingTimeLabel.Text;
            set => _UsingTimeLabel.Text = value;
        }

        private void AddToControlList(Control ctrl, List<Control> list)
        {
            list.Add(ctrl);
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control subControl in ctrl.Controls)
                    AddToControlList(subControl, list);
            }
        }

        protected virtual void OnCellRightMouseClicked()
        {
            CellRightMouseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}