using System;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.Plots.Themes
{
    public partial class ThemeManagerDialog : SimpleForm
    {
        public PlotTheme PlotTheme { get; set; }

        public ThemeManagerDialog()
        {
            InitializeComponent();
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Shown" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var hd = DI.Get<IHabitedDatas>();
            var themes = hd.PlotThemes;
            foreach (var plotTheme in themes)
            {
                _ThemeListComboBox.Items.Add(plotTheme.Name);
            }
            var usingTheme = hd.UsingTheme;
            _ThemeListComboBox.SelectedText = usingTheme;
            foreach (var plotTheme in themes)
            {
                if (plotTheme.Name == usingTheme)
                {
                    PlotTheme = plotTheme;
                    break;
                }
            }
        }

        #endregion
    }
}
