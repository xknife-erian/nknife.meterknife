using System.Collections.Generic;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.Base.Viewmodels
{
    public class GatewayViewModelBase : CommonViewModelBase
    {
        public GatewayViewModelBase()
        {
            DiscoverMap = Load(Habited.Gateways);
        }

        public Dictionary<GatewayModel, IGatewayDiscover> DiscoverMap { get; protected set; }

        /// <summary>
        ///     将从保存的用户习惯数据中取出的数据转换成Discover的字典
        /// </summary>
        /// <param name="map">从保存的用户习惯数据中取出的数据</param>
        protected static Dictionary<GatewayModel, IGatewayDiscover> Load(Dictionary<GatewayModel, List<Instrument>> map)
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
        protected static Dictionary<GatewayModel, List<Instrument>> ToMap(Dictionary<GatewayModel, IGatewayDiscover> discoverMap)
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