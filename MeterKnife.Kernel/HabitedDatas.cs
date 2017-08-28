using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using MeterKnife.Plots.Themes;
using Newtonsoft.Json;
using NKnife.Utility;
using NKnife.Wrapper;

namespace MeterKnife.Kernel
{
    /// <summary>
    /// 用户使用习惯
    /// </summary>
    public class HabitedDatas : UserApplicationData, IHabitedDatas
    {
        private readonly List<PlotTheme> _DefaultPlotThemes;

        public HabitedDatas()
        {
            var pt = new PlotTheme();
            pt.SeriesStyles.Add(new PlotTheme.SeriesStyle());
            _DefaultPlotThemes = new List<PlotTheme>(new[] { pt });
        }

        /// <summary>
        /// 用户自定义的主题
        /// </summary>
        public List<PlotTheme> PlotThemes
        {
            get
            {
                var content = GetValue(nameof(PlotThemes), JsonConvert.SerializeObject(_DefaultPlotThemes));
                var value = JsonConvert.DeserializeObject<List<PlotTheme>>(content);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(PlotThemes), JsonConvert.SerializeObject(_DefaultPlotThemes));
                else
                    SetValue(nameof(PlotThemes), JsonConvert.SerializeObject(value));
            }
        }

        /// <summary>
        /// 用户当前正在使用的主题
        /// </summary>
        public string UsingTheme
        {
            get => GetValue(nameof(UsingTheme), "默认主题");
            set => SetValue(nameof(UsingTheme), value);
        }

        public List<IGatewayDiscover> Discovers
        {
            get
            {
                var content = GetValue(nameof(Discovers), new List<IGatewayDiscover>(0));
                var value = JsonConvert.DeserializeObject<List<IGatewayDiscover>>(content);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(Discovers), JsonConvert.SerializeObject(new List<IGatewayDiscover>(0)));
                else
                    SetValue(nameof(Discovers), JsonConvert.SerializeObject(value));
            }
        }
    }
}
