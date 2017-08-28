using System;
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

        public void SetInstruments(Instrument instrument)
        {
            Image = instrument.Image;
            Model = instrument.Model;
            Manufacturer = instrument.Manufacturer;
            ConnectString = instrument.ConnectString;
            Information = instrument.Information;
            DatasCount = instrument.DatasCount.ToString();
            UsingTime = instrument.LastUsingTime.ToString("yy/MM/dd");
        }

        public Image Image
        {
            get => _PictureBox.Image;
            set => _PictureBox.Image = value;
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

        public string ConnectString
        {
            get => _ConnStringLabel.Text;
            set => _ConnStringLabel.Text = value;
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

    }
}