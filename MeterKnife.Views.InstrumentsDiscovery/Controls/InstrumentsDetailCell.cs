using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls
{
    public partial class InstrumentsDetailCell : UserControl
    {
        public InstrumentsDetailCell()
        {
            InitializeComponent();
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

        public string ConnString
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