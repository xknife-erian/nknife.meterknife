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
            Discovers.Load(HabitedDatas.Gateways);
            foreach (var pair in Discovers)
            {
                var model = pair.Key;
                var instruments = pair.Value;

                var discrover = DI.Get<IGatewayDiscover>(model.ToString());
                discrover.InstrumentAdded += (s, e) =>
                {
                    instruments.Add(e.Instrument);
                    HabitedDatas.Gateways = Discovers.ToMap();
                };
            }
        }

        public InstrumentMap Discovers { get; set; } = new InstrumentMap();

        public void AddInstrument(GatewayModel model)
        {
            var discrover = DI.Get<IGatewayDiscover>(model.ToString());
            discrover.AddInstrument();
        }
    }
}