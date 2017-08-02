using System;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.Plots.Themes
{
    public partial class ThemeManagerDialog : SimpleForm
    {
        private PlotTheme _PlotTheme;

        public PlotTheme PlotTheme
        {
            get => _PlotTheme;
            set
            {
                if (value != null && _PlotTheme != value)
                {
                    _PlotTheme = value;
                    OnPlotThemeChanged();
                }
            }
        }

        public event EventHandler PlotThemeChanged;

        public ThemeManagerDialog()
        {
            InitializeComponent();
            PlotThemeChanged += (s, e) =>
            {
                _BottomAxisGridLineMajorColor.Color = PlotTheme.BottomAxisGridLineColors.Major;
                _BottomAxisGridLineMinorColor.Color = PlotTheme.BottomAxisGridLineColors.Minor;
                _LeftAxisGridLineMajorColor.Color = PlotTheme.LeftAxisGridLineColors.Major;
                _LeftAxisGridLineMinorColor.Color = PlotTheme.LeftAxisGridLineColors.Minor;
                _ViewBackground.Color = PlotTheme.ViewBackground;
                _AreaBackground.Color = PlotTheme.AreaBackground;
                foreach (var t in PlotTheme.SeriesStyles)
                {
                    _SeriesListComboBox.Items.Add(t);
                }
                if (_SeriesListComboBox.Items.Count <= 0)
                {
                    var item = new PlotTheme.SeriesStyle();
                    PlotTheme.SeriesStyles.Add(item);
                    _SeriesListComboBox.Items.Add(item);
                }
                _SeriesListComboBox.SelectedIndex = 0;
            };
            _SeriesListComboBox.SelectedIndexChanged += (s, e) =>
            {
                var seriesStyle = (PlotTheme.SeriesStyle) _SeriesListComboBox.SelectedItem;
                _SeriesColor.Color = seriesStyle.Color;
                _SeriesThickness.Value = (decimal) seriesStyle.Thickness;
            };
            _CloseButton.Click += (s, e) => { Close(); };
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Shown" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var hd = DI.Get<IHabitedDatas>();
            var themes = hd.PlotThemes;
            var usingTheme = hd.UsingTheme;
            foreach (var plotTheme in themes)
            {
                _ThemeListComboBox.Items.Add(plotTheme);
                _ThemeListComboBox.SelectedItem = plotTheme;
                if (plotTheme.Name == usingTheme)
                    PlotTheme = plotTheme;
            }
        }

        #endregion

        protected virtual void OnPlotThemeChanged()
        {
            PlotThemeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
