using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.ViewModels
{
    public class InstrumentsDiscoveryViewModel : ViewmodelBaseKnife
    {
        public InstrumentsDiscoveryViewModel()
        {
            DiscoverMap = Load(HabitedDatas.Gateways);
            OnDiscoverInstrumentsCollectionChanged();
            OnInstrumentConnectionStateChanged();
        }

        #region Property: SelectedInstrument

        private Instrument _SelectedInstrument;

        public Instrument SelectedInstrument
        {
            get => _SelectedInstrument;
            set { Set(() => SelectedInstrument, ref _SelectedInstrument, value); }
        }

        #endregion

        #region Discover

        public Dictionary<GatewayModel, IGatewayDiscover> DiscoverMap { get; }

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

        /// <summary>
        ///     在构造函数里注册所有InstrumentsConllection
        /// </summary>
        private void OnDiscoverInstrumentsCollectionChanged()
        {
            foreach (var discrover in DiscoverMap.Values)
                discrover.Instruments.CollectionChanged += (s, e) => { HabitedDatas.Gateways = ToMap(DiscoverMap); };
        }

        #endregion

        #region Gateway

        /// <summary>
        ///     刷新指定的测量途径下所有保存的仪器的连接状态
        /// </summary>
        /// <param name="model">指定的测量途径</param>
        public void RefreshInstrumentStateByGateway(GatewayModel model)
        {
            var discrover = DiscoverMap[model];
            var newStates = discrover.Refresh();
            var oldStates = InstrumentStateMap[model];
            for (var i = 0; i < newStates.Count; i++)
                if (oldStates[i].Equals(newStates[i]))
                    oldStates[i] = newStates[i];
        }

        public void GatewayModelDelete(GatewayModel model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Instrument

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

        #endregion

        #region InstrumentState

        public StateMap InstrumentStateMap { get; } = new StateMap();

        /// <summary>
        ///     在构造函数里注册了所有仪器的连接状态发生变化时
        /// </summary>
        private void OnInstrumentConnectionStateChanged()
        {
            foreach (var pair in InstrumentStateMap)
                pair.Value.CollectionChanged += (s, e) => { };
        }

        public class StateMap : Dictionary<GatewayModel, ObservableCollection<InstrumentConnectionState>>
        {
        }

        #endregion
    }
}