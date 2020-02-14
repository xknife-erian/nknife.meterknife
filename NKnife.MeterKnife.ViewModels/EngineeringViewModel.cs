using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.ViewModels
{
    public class EngineeringViewModel : ViewModelBase
    {
        private readonly IEngineeringLogic _engineeringLogic;
        private readonly Dictionary<string, List<Engineering>> _engMap = new Dictionary<string, List<Engineering>>();

        public EngineeringViewModel(IEngineeringLogic engineeringLogic)
        {
            _engineeringLogic = engineeringLogic;
        }

        public async Task<Dictionary<string, List<Engineering>>> GetEngineeringAndDateMap()
        {
            _engMap.Clear();
            var engList = (await _engineeringLogic.GetAllEngineeringAsync()).ToList();
            engList.Sort((x, y) => y.CreateTime.CompareTo(x.CreateTime));
            foreach (var engineering in engList)
            {
                var date = engineering.CreateTime.ToString("yyyy-MM");
                if(_engMap.TryGetValue(date, out var list))
                {
                    list.Add(engineering);
                }
                else
                {
                    list = new List<Engineering> {engineering};
                    _engMap.Add(date, list);
                }
            }

            return _engMap;
        }

    }
}
