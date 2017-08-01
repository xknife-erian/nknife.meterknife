using System.Collections.Generic;
using MeterKnife.Models;

namespace MeterKnife.Interfaces
{
    public interface IHabitedDatas
    {
        List<PlotTheme> PlotThemes { get; set; }
        string UsingTheme { get; set; }
    }
}