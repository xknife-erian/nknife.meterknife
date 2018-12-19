using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.ControlKnife.Colors;

namespace MeterKnife.Plots.Themes
{
    public sealed partial class ThemeColorItem : UserControl
    {
        private Color _color = Color.White;

        public Color Color
        {
            get => _color;
            set
            {
                if (value != _color)
                {
                    _color = value;
                    BackColor = _color;
                    OnColorChanged(new ColorChangedEventArgs(value));
                }
            }
        }

        public ThemeColorItem()
        {
            InitializeComponent();
            BackColor = _color;
            _Button.Click += (s, e) =>
            {
                var dialog = new ColorPickerDialog();
                if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    Color = dialog.SelectedColor;
                }
            };
        }

        public event EventHandler<ColorChangedEventArgs> ColorChanged;

        private void OnColorChanged(ColorChangedEventArgs e)
        {
            ColorChanged?.Invoke(this, e);
        }
    }

    public class ColorChangedEventArgs : EventArgs
    {
        public Color Color { get; set; }

        public ColorChangedEventArgs(Color color)
        {
            Color = color;
        }
    }
}
