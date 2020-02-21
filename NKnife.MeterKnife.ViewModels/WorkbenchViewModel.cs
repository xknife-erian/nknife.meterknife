﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util.Serial.Common;

namespace NKnife.MeterKnife.ViewModels
{
    public class WorkbenchViewModel : ViewModelBase, IWorkbenchViewModel
    {
        private readonly IStoragePlatform<Instrument> _instrumentStoragePlatform;
        private readonly IStoragePlatform<Engineering> _engineeringStoragePlatform;
        private readonly IStoragePlatform<Slot> _slotStoragePlatform;
        private readonly IStoragePlatform<DUT> _dutStoragePlatform;

        private readonly IEngineeringLogic _engineeringLogic;

        private readonly Dictionary<DateTime, List<Engineering>> _engMap = new Dictionary<DateTime, List<Engineering>>();

        public WorkbenchViewModel(IStoragePlatform<Engineering> engineeringStoragePlatform, IStoragePlatform<Slot> slotStoragePlatform, IStoragePlatform<DUT> dutStoragePlatform, IEngineeringLogic engineeringLogic, IStoragePlatform<Instrument> instrumentStoragePlatform)
        {
            _engineeringStoragePlatform = engineeringStoragePlatform;
            _slotStoragePlatform = slotStoragePlatform;
            _dutStoragePlatform = dutStoragePlatform;
            _engineeringLogic = engineeringLogic;
            _instrumentStoragePlatform = instrumentStoragePlatform;
        }

        /// <summary>
        ///     创建一台仪器
        /// </summary>
        public async Task CreateInstrumentAsync(Instrument inst)
        {
            await _instrumentStoragePlatform.InsertAsync(inst);
        }

        /// <summary>
        ///     获取所有的仪器
        /// </summary>
        public async Task<IEnumerable<Instrument>> GetAllInstrumentAsync()
        {
            return await _instrumentStoragePlatform.FindAllAsync();
        }

        /// <summary>
        /// 创建一个工程
        /// </summary>
        public async Task CreateEngineeringAsync(Engineering eng)
        {
            await _engineeringLogic.CreateEngineeringAsync(eng);
        }

        /// <summary>
        /// 获取所有工程，并按工程的创建时间倒序排列
        /// </summary>
        public async Task<Dictionary<DateTime, List<Engineering>>> GetEngineeringAndDateMapAsync()
        {
            _engMap.Clear();
            var engList = (await _engineeringStoragePlatform.FindAllAsync()).ToList();
            engList.Sort((x, y) => y.CreateTime.CompareTo(x.CreateTime));
            foreach (var engineering in engList)
            {
                var date = new DateTime(engineering.CreateTime.Year, engineering.CreateTime.Month, 1, 0, 0, 0);
                if (_engMap.TryGetValue(date, out var list))
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

        /// <summary>
        /// 是否存在相同编号的工程
        /// </summary>
        /// <param name="engId">工程编号</param>
        /// <returns>是否存在</returns>
        public bool ExistEngineering(string engId)
        {
            return _engineeringStoragePlatform.ExistAsync(engId).Result;
        }

        /// <summary>
        /// 创建一个Care接驳器
        /// </summary>
        /// <param name="port">Care所在串口编号</param>
        /// <returns>是否创建成功</returns>
        public async Task<Slot> CreatMeterCareSlotAsync(short port)
        {
            var slot = new Slot();
            slot.SetMeterCare(SlotType.MeterCare, (port, new SerialConfig() {BaudRate = 115200}));
            var b =  await _slotStoragePlatform.InsertAsync(slot);
            if (b)
                return slot;
            return null;
        }

        /// <summary>
        /// 获取所有的<see cref="Slot"/>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Slot>> GetAllSlotAsync()
        {
            return await _slotStoragePlatform.FindAllAsync();
        }

        /// <summary>
        /// 获取所有的<see cref="DUT"/>
        /// </summary>
        public async Task<IEnumerable<DUT>> GetAllDUTAsync()
        {
            return await _dutStoragePlatform.FindAllAsync();
        }
    }
}
