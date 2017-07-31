/*
Copyright (C) 2015 Eremin V. Leonid (leremin@outlook.com)
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MeterKnife.Plots.Themes
{
    public class ColorComboBox : ComboBox
    {
        private int _MaxTextWidth;

        public ColorComboBox()
        {
            DrawItem += OnDrawItem;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                DrawMode = DrawMode.OwnerDrawVariable;
                DropDownStyle = ComboBoxStyle.DropDownList;
                LoadColors();
            }
        }

        public Color SelectedColor
        {
            get
            {
                var index = SelectedIndex;
                return index > 0 ? Color.FromName(Items[SelectedIndex].ToString()) : Color.Magenta;
            }
            set
            {
                var index = Items.IndexOf(value.Name);
                if (index > 0)
                    SelectedIndex = index;
            }
        }

        private void LoadColors()
        {
            var colorType = typeof(Color);
            var colors = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).ToList();
            colors = colors.Where(t => t.Name != Color.Transparent.Name && t.Name != Color.White.Name).ToList();

            Items.Clear();
            foreach (var t in colors)
                Items.Add(t.Name);

            using (var g = CreateGraphics())
            {
                _MaxTextWidth = (int) colors.Select(t => g.MeasureString(t.Name, Font).Width).Max();
            }
        }

        private void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var r = e.Bounds;

            var combo = sender as ComboBox;
            if (combo == null || e.Index < 0)
                return;

            e.DrawBackground();

            var text = combo.Items[e.Index].ToString();
            Brush colorBrush = new SolidBrush(Color.FromName(text));
            Brush blackBrush = new SolidBrush(Color.FromKnownColor(KnownColor.WindowText));

            var rect = new Rectangle
            {
                X = r.X + _MaxTextWidth,
                Y = (int) (r.Y + r.Height * 0.2),
                Width = (int) (r.Width - _MaxTextWidth - r.Width * 0.05),
                Height = (int) (r.Height - r.Height * 0.4)
            };

            g.DrawString(text, e.Font, blackBrush, r.X, r.Top);
            g.FillRectangle(colorBrush, rect);
            g.DrawRectangle(new Pen(blackBrush), rect);
        }
    }
}