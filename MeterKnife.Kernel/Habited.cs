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
        private static readonly JsonSerializerSettings HasTypeNameJsonSettings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        private readonly Dictionary<GatewayModel, List<Instrument>> _defaultDiscovers;
        private readonly List<PlotTheme> _defaultPlotThemes;
        private readonly List<PlotSeriesStyleSolution> _defaultSeriesStyleSolutions;

        public Habited()
        {
            var pt = new PlotTheme();
            _defaultPlotThemes = new List<PlotTheme>(new[] {pt});
            _defaultSeriesStyleSolutions = new List<PlotSeriesStyleSolution>(0);

            _defaultDiscovers = new Dictionary<GatewayModel, List<Instrument>>();
            _defaultDiscovers.Add(GatewayModel.Aglient82357A, new List<Instrument>());
            _defaultDiscovers.Add(GatewayModel.CareOne, new List<Instrument>());
        }

        /// <summary>
        ///     用户自定义的主题
        /// </summary>
        public List<PlotTheme> PlotThemes
        {
            get
            {
                var content = GetValue(nameof(PlotThemes), JsonConvert.SerializeObject(_defaultPlotThemes));
                var value = JsonConvert.DeserializeObject<List<PlotTheme>>(content);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(PlotThemes), JsonConvert.SerializeObject(_defaultPlotThemes));
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
                var content = GetValue(nameof(Gateways), JsonConvert.SerializeObject(_defaultDiscovers));
                var value = JsonConvert.DeserializeObject<Dictionary<GatewayModel, List<Instrument>>>(content);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(Gateways), JsonConvert.SerializeObject(_defaultDiscovers));
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
                    JsonConvert.SerializeObject(_defaultSeriesStyleSolutions, Formatting.None, HasTypeNameJsonSettings));
                var value = JsonConvert.DeserializeObject<List<PlotSeriesStyleSolution>>(content, HasTypeNameJsonSettings);
                return value;
            }
            set
            {
                if (value == null || value.Count <= 0)
                    SetValue(nameof(SeriesStyleSolutionList), JsonConvert.SerializeObject(_defaultSeriesStyleSolutions, Formatting.None, HasTypeNameJsonSettings));
                else
                    SetValue(nameof(SeriesStyleSolutionList), JsonConvert.SerializeObject(value, Formatting.None, HasTypeNameJsonSettings));
            }
        }

        public void SaveHabited()
        {
            Save();
        }
    }
}