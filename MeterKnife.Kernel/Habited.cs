using System.Collections.Generic;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using Newtonsoft.Json;
using NKnife.Wrapper;

namespace MeterKnife.Kernel
{
    /// <summary>
    ///     用户使用习惯
    /// </summary>
    public class Habited : HabitedDatas, IHabited
    {
        /// <summary>
        /// 定义JSON序列化时需要将类型名置入，以方便反序列化时可以定位接口的实现
        /// </summary>
        private static readonly JsonSerializerSettings _hasTypeNameJsonSettings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        private readonly Dictionary<GatewayModel, List<Instrument>> _DefaultDiscovers;
        private readonly List<PlotTheme> _DefaultPlotThemes;
        private readonly List<PlotSeriesStyleSolution> _DefaultSeriesStyleSolutions;

        public Habited()
        {
            var pt = new PlotTheme();
            _DefaultPlotThemes = new List<PlotTheme>(new[] {pt});
            _DefaultSeriesStyleSolutions = new List<PlotSeriesStyleSolution>(0);

            _DefaultDiscovers = new Dictionary<GatewayModel, List<Instrument>>();
            _DefaultDiscovers.Add(GatewayModel.Aglient82357A, new List<Instrument>());
            _DefaultDiscovers.Add(GatewayModel.CareOne, new List<Instrument>());
        }

        /// <summary>
        ///     用户自定义的主题
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
        ///     用户当前正在使用的主题
        /// </summary>
        public string UsingTheme
        {
            get => GetValue(nameof(UsingTheme), "默认主题");
            set => SetValue(nameof(UsingTheme), value);
        }

        public Dictionary<GatewayModel, List<Instrument>> Gateways
        {
            get
            {
                var content = GetValue(nameof(Gateways), JsonConvert.SerializeObject(_DefaultDiscovers));
                var value = JsonConvert.DeserializeObject<Dictionary<GatewayModel, List<Instrument>>>(content);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(Gateways), JsonConvert.SerializeObject(_DefaultDiscovers));
                else
                    SetValue(nameof(Gateways), JsonConvert.SerializeObject(value));
            }
        }

        /// <summary>
        ///     数据折线样式列表
        /// </summary>
        public List<PlotSeriesStyleSolution> SeriesStyleSolutionList
        {
            get
            {
                var content = GetValue(nameof(SeriesStyleSolutionList),
                    JsonConvert.SerializeObject(_DefaultSeriesStyleSolutions, Formatting.None, _hasTypeNameJsonSettings));
                var value = JsonConvert.DeserializeObject<List<PlotSeriesStyleSolution>>(content, _hasTypeNameJsonSettings);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(SeriesStyleSolutionList), JsonConvert.SerializeObject(_DefaultSeriesStyleSolutions, Formatting.None, _hasTypeNameJsonSettings));
                else
                    SetValue(nameof(SeriesStyleSolutionList), JsonConvert.SerializeObject(value, Formatting.None, _hasTypeNameJsonSettings));
            }
        }

        public void SaveHabited()
        {
            Save();
        }
    }
}