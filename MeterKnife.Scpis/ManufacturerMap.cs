using System;
using System.Collections.Generic;
using System.Linq;

namespace MeterKnife.Scpis
{
    public class ManufacturerMap
    {
        private readonly Dictionary<string, List<Tuple<string, string, string>>> _brandMap =
            new Dictionary<string, List<Tuple<string, string, string>>>();

        private readonly List<Tuple<string, string, string>> _collection;

        public ManufacturerMap(List<Tuple<string, string, string>> collection)
        {
            _collection = collection;
            foreach (var tuple in collection)
            {
                if (_brandMap.ContainsKey(tuple.Item1))
                {
                    _brandMap[tuple.Item1].Add(tuple);
                }
                else
                {
                    var list = new List<Tuple<string, string, string>> {tuple};
                    _brandMap.Add(tuple.Item1, list);
                }
            }
        }

        public string[] Brands => _brandMap.Keys.ToArray();

        public Tuple<string, string, string> this[int index] => _collection[index];

        public List<Tuple<string, string, string>> ByBrand(string brand)
        {
            return _brandMap[brand];
        }

        public Tuple<string, string, string> ByBrandAndName(string brand, string name)
        {
            var names = _brandMap[brand];
            return names.FirstOrDefault(tuple => tuple.Item2 == name);
        }

        public bool HasBrand(string brand)
        {
            return _brandMap.ContainsKey(brand);
        }

        public bool HasBrandName(string brand, string name)
        {
            return _brandMap.ContainsKey(brand) && _brandMap[brand].Any(tuple => tuple.Item2 == name);
        }
    }
}