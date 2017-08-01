using MeterKnife.Models;
using NKnife.ControlKnife;

namespace MeterKnife.Plots.Themes
{
    public partial class ThemeManagerDialog : SimpleForm
    {
        public PlotTheme PlotTheme { get; set; }

        public ThemeManagerDialog(PlotTheme plotTheme)
        {
            PlotTheme = plotTheme;
            InitializeComponent();
        }
    }
}
