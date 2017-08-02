﻿using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Utils.ColorControls;

namespace MeterKnife.Plots.Themes
{
    public sealed partial class ThemeColorItem : UserControl
    {
        private Color _Color = Color.White;

        public Color Color
        {
            get => _Color;
            set
            {
                if (value != _Color)
                {
                    _Color = value;
                    BackColor = _Color;
                    OnColorChanged(new ColorChangedEventArgs(value));
                }
            }
        }

        public ThemeColorItem()
        {
            InitializeComponent();
            BackColor = _Color;
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