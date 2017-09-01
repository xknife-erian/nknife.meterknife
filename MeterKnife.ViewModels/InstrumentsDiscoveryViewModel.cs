using System.Collections.Generic;
using System.Collections.Specialized;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.ViewModels
{
    public class InstrumentsDiscoveryViewModel : ViewmodelBaseKnife
    {
        private readonly Dictionary<GatewayModel, IGatewayDiscover> _DiscoverMap = new Dictionary<GatewayModel, IGatewayDiscover>();

        public InstrumentsDiscoveryViewModel()
        {
            InstrumentMap.Load(HabitedDatas.Gateways);
            foreach (var pair in InstrumentMap)
            {
                var model = pair.Key;
                var tempInsts = pair.Value;
                tempInsts.CollectionChanged += (s, e) =>
                {
                    HabitedDatas.Gateways = InstrumentMap.ToMap();
                };

                var discrover = GetDiscover(model);
                discrover.Instruments.CollectionChanged += (s, e) =>
                {
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        {
                            foreach (Instrument item in e.NewItems)
                                tempInsts.Add(item);
                            break;
                        }
                        case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (Instrument item in e.OldItems)
                                tempInsts.Remove(item);
                            break;
                        }
                        case NotifyCollectionChangedAction.Move:
                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Reset:
                            break;
                    }
                };
            }
        }

        public InstrumentMap InstrumentMap { get; set; } = new InstrumentMap();

        private IGatewayDiscover GetDiscover(GatewayModel model)
        {
            IGatewayDiscover discover;
            if (!_DiscoverMap.TryGetValue(model, out discover))
            {
                discover = DI.Get<IGatewayDiscover>(model.ToString());
                _DiscoverMap.Add(model, discover);
            }
            return discover;
        }

        public void CreateInstrument(GatewayModel model)
        {
            var discrover = GetDiscover(model);
            discrover.CreateInstrument();
        }

        public void DeleteInstrument(GatewayModel model, Instrument instrument)
        {
            var discrover = GetDiscover(model);
            discrover.DeleteInstrument(instrument);
        }

        public void GatewayModelUpdate(GatewayModel model)
        {
            throw new System.NotImplementedException();
        }

        public void GatewayModelDelete(GatewayModel model)
        {
            throw new System.NotImplementedException();
        }

        public void InstrumentCommandManager(GatewayModel model, Instrument instrument)
        {
            throw new System.NotImplementedException();
        }

        public void InstrumentConnectionTest(GatewayModel model, Instrument instrument)
        {
            throw new System.NotImplementedException();
        }

        public void InstrumentDatasManager(GatewayModel model, Instrument instrument)
        {
            throw new System.NotImplementedException();
        }
    }
}