using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeterKnife.Interfaces.Gateways;
using NKnife.IoC;

namespace MeterKnife.Models
{
    public class InstrumentMap : Dictionary<GatewayModel, ObservableCollection<Instrument>>
    {
        public void Load(Dictionary<GatewayModel, List<Instrument>> map)
        {
            foreach (var pair in map)
            {
                var discover = DI.Get<IGatewayDiscover>(pair.Key.ToString());
                var list = new ObservableCollection<Instrument>();
                foreach (var instrument in pair.Value)
                {
                    list.Add(instrument);
                    discover.Instruments.Add(instrument);
                }
                Add(pair.Key, list);
            }
        }

        public Dictionary<GatewayModel, List<Instrument>> ToMap()
        {
            var map = new Dictionary<GatewayModel, List<Instrument>>();
            foreach (var pair in this)
            {
                var list = new List<Instrument>();
                list.AddRange(pair.Value);
                map.Add(pair.Key, list);
            }
            return map;
        }
    }
}