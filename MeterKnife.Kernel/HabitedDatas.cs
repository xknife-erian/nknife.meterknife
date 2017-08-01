using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;
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
        public List<PlotTheme> PlotThemes
        {
            get
            {
                var content = GetValue(nameof(PlotThemes), new List<PlotTheme>(new[] {new PlotTheme()}));
                var ts = JsonConvert.DeserializeObject<List<PlotTheme>>(content);
                return ts;
            }
            set
            {
                if (value == null || value.Count <= 0)
                {
                    var ts = new List<PlotTheme>(new[] {new PlotTheme()});
                    SetValue(nameof(PlotThemes), JsonConvert.SerializeObject(ts));
                }
                else
                {
                    SetValue(nameof(PlotThemes), JsonConvert.SerializeObject(value));
                }
            }
        }

        public string UsingTheme
        {
            get => GetValue(nameof(UsingTheme), "默认主题");
            set => SetValue(nameof(UsingTheme), value);
        }
    }
}
