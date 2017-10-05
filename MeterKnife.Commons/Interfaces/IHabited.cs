using System.Collections.Generic;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;

namespace MeterKnife.Interfaces
{
    /// <summary>
    /// 一个描述用户习惯的数据
    /// </summary>
    public interface IHabited
    {
        /// <summary>
        /// 用户自定义的主题
        /// </summary>
        List<PlotTheme> PlotThemes { get; set; }

        /// <summary>
        /// 用户当前正在使用的主题
        /// </summary>
        string UsingTheme { get; set; }

        /// <summary>
        /// 用户已添加的测试途径
        /// </summary>
        Dictionary<GatewayModel, List<Instrument>> Gateways { get; set; }

        List<PlotSeriesStyleSolution> SeriesStyleSolutions { get; set; }
    }
}