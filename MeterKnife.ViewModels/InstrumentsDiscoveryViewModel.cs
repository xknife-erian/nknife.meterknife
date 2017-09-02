using System;
using System.Collections.Generic;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.ViewModels
{
    public class InstrumentsDiscoveryViewModel : ViewmodelBaseKnife
    {
        private Instrument _SelectedInstrument;

        public InstrumentsDiscoveryViewModel()
        {
            DiscoverMap = Load(HabitedDatas.Gateways);
            foreach (var discrover in DiscoverMap.Values)
                discrover.Instruments.CollectionChanged += (s, e) => { HabitedDatas.Gateways = ToMap(DiscoverMap); };
        }

        public Dictionary<GatewayModel, IGatewayDiscover> DiscoverMap { get; }

        public Instrument SelectedInstrument
        {
            get => _SelectedInstrument;
            set { Set(() => SelectedInstrument, ref _SelectedInstrument, value); }
        }

        public void CreateInstrument(GatewayModel model)
        {
            var discrover = DiscoverMap[model];
            discrover.CreateInstrument();
        }

        public void DeleteInstrument(GatewayModel model, Instrument instrument)
        {
            var discrover = DiscoverMap[model];
            discrover.DeleteInstrument(instrument);
        }

        public void GatewayModelUpdate(GatewayModel model)
        {
            throw new NotImplementedException();
        }

        public void GatewayModelDelete(GatewayModel model)
        {
            throw new NotImplementedException();
        }

        public void InstrumentCommandManager(GatewayModel model, Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public void InstrumentConnectionTest(GatewayModel model, Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public void InstrumentDatasManager(GatewayModel model, Instrument instrument)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     将从保存的用户习惯数据中取出的数据转换成Discover的字典
        /// </summary>
        /// <param name="map">从保存的用户习惯数据中取出的数据</param>
        private static Dictionary<GatewayModel, IGatewayDiscover> Load(Dictionary<GatewayModel, List<Instrument>> map)
        {
            var result = new Dictionary<GatewayModel, IGatewayDiscover>();
            foreach (var pair in map)
            {
                var discover = DI.Get<IGatewayDiscover>(pair.Key.ToString());
                foreach (var instrument in pair.Value)
                    discover.Instruments.Add(instrument);
                result.Add(pair.Key, discover);
            }
            return result;
        }

        /// <summary>
        ///     将Discover的字典转换成可以保存成用户习惯数据的格式
        /// </summary>
        private static Dictionary<GatewayModel, List<Instrument>> ToMap(Dictionary<GatewayModel, IGatewayDiscover> discoverMap)
        {
            var map = new Dictionary<GatewayModel, List<Instrument>>();
            foreach (var pair in discoverMap)
            {
                var list = new List<Instrument>();
                list.AddRange(pair.Value.Instruments);
                map.Add(pair.Key, list);
            }
            return map;
        }
    }
}