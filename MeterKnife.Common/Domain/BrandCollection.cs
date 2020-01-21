using System;
using System.Collections.Generic;
using System.Linq;

namespace NKnife.MeterKnife.Common.Domain
{
    public class BrandCollection
    {
        private readonly Dictionary<string, List<Tuple<string, string, string>>> _BrandDictionary =
            new Dictionary<string, List<Tuple<string, string, string>>>();

        private readonly List<Tuple<string, string, string>> _Collection;

        public BrandCollection(List<Tuple<string, string, string>> collection)
        {
            _Collection = collection;
            foreach (var tuple in collection)
                if (_BrandDictionary.ContainsKey(tuple.Item1))
                {
                    _BrandDictionary[tuple.Item1].Add(tuple);
                }
                else
                {
                    var list = new List<Tuple<string, string, string>> {tuple};
                    _BrandDictionary.Add(tuple.Item1, list);
                }
        }

        public string[] Brands => _BrandDictionary.Keys.ToArray();

        public Tuple<string, string, string> this[int index] => _Collection[index];

        public List<Tuple<string, string, string>> ByBrand(string brand)
        {
            return _BrandDictionary[brand];
        }

        public Tuple<string, string, string> ByBrandAndName(string brand, string name)
        {
            var names = _BrandDictionary[brand];
            return names.FirstOrDefault(tuple => tuple.Item2 == name);
        }

        public bool HasBrand(string brand)
        {
            return _BrandDictionary.ContainsKey(brand);
        }

        public bool HasBrandName(string brand, string name)
        {
            return _BrandDictionary.ContainsKey(brand) && _BrandDictionary[brand].Any(tuple => tuple.Item2 == name);
        }
    }
}