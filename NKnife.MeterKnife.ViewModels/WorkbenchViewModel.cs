using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Util.Serial;
using NKnife.MeterKnife.Util.Serial.Common;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.ViewModels
{
    public class WorkbenchViewModel : ViewModelBase, IWorkbenchViewModel
    {
        private readonly IAntService _antService;

        private readonly IStoragePlatform<Instrument> _instrumentStoragePlatform;
        private readonly IStoragePlatform<Engineering> _engineeringStoragePlatform;
        private readonly IStoragePlatform<Slot> _slotStoragePlatform;
        private readonly IStoragePlatform<DUT> _dutStoragePlatform;

        private readonly IStorageDUTRead<MeasureData> _dutRead;

        private readonly IEngineeringLogic _engineeringLogic;
        private readonly IMeasuringLogic _performLogic;

        /// <summary>
        ///     按时间排序的工程列表，供工程列表窗体显示
        /// </summary>
        private readonly Dictionary<DateTime, List<Engineering>> _engMap = new Dictionary<DateTime, List<Engineering>>();

        /// <summary>
        ///     已打开的工程列表
        /// </summary>
        private ObservableCollection<Engineering> _openedEngineerings = new ObservableCollection<Engineering>();

        /// <summary>
        ///     正在采集的工程列表
        /// </summary>
        private ObservableCollection<Engineering> _acquiringEngineerings = new ObservableCollection<Engineering>();

        /// <summary>
        ///     当前激活的工程
        /// </summary>
        private Engineering _currentEngineering;

        public WorkbenchViewModel(
            IAntService antService,
            IEngineeringLogic engineeringLogic, IMeasuringLogic performLogic,
            IStoragePlatform<Engineering> engineeringStoragePlatform,
            IStoragePlatform<Slot> slotStoragePlatform,
            IStoragePlatform<DUT> dutStoragePlatform,
            IStoragePlatform<Instrument> instrumentStoragePlatform,
            IStorageDUTRead<MeasureData> dutRead)
        {
            _engineeringStoragePlatform = engineeringStoragePlatform;
            _slotStoragePlatform = slotStoragePlatform;
            _dutStoragePlatform = dutStoragePlatform;
            _engineeringLogic = engineeringLogic;
            _instrumentStoragePlatform = instrumentStoragePlatform;
            _dutRead = dutRead;
            _performLogic = performLogic;
            _antService = antService;
        }

        /// <summary>
        ///     工程的采集状态
        /// </summary>
        public ObservableCollection<EngineeringState> EngineeringStateList { get; set; } = new ObservableCollection<EngineeringState>();

        /// <summary>
        ///     当前激活（被选择）的工程
        /// </summary>
        public Engineering CurrentActiveEngineering
        {
            get => _currentEngineering;
            set => Set(ref _currentEngineering, value);
        }

        /// <summary>
        ///     当前选择的工程
        /// </summary>
        public Engineering CurrentSelectedEngineering { get; set; }

        /// <summary>
        ///     创建一台仪器
        /// </summary>
        public async Task CreateInstrumentAsync(Instrument inst)
        {
            await _instrumentStoragePlatform.InsertAsync(inst);
        }

        /// <summary>
        ///     修改一台仪器
        /// </summary>
        public async Task UpdateInstrumentAsync(Instrument inst)
        {
            await _instrumentStoragePlatform.UpdateAsync(inst);
        }

        /// <summary>
        ///     删除一台仪器
        /// </summary>
        public async Task DeleteInstrumentAsync(Instrument inst)
        {
            await _instrumentStoragePlatform.RemoveAsync(inst.Id);
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
        ///     修改一个工程
        /// </summary>
        public async Task UpdateEngineeringAsync(Engineering eng)
        {
            await _engineeringLogic.UpdateEngineeringAsync(eng);
        }

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        public async Task DeleteEngineeringAsync(Engineering eng)
        {
            await _engineeringLogic.RemoveEnginneringAsync(eng);
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
        ///     已打开的工程
        /// </summary>
        public ObservableCollection<Engineering> OpenedEngineerings
        {
            get => _openedEngineerings;
            set
            {
                _openedEngineerings = value;
                RaisePropertyChanged(() => OpenedEngineerings);
            }
        }

        /// <summary>
        ///     正在测量的工程
        /// </summary>
        public ObservableCollection<Engineering> AcquiringEngineerings
        {
            get => _acquiringEngineerings;
            set
            {
                _acquiringEngineerings = value;
                RaisePropertyChanged(() => AcquiringEngineerings);
            }
        }

        /// <summary>
        /// 获取指定工程的被测物的测量数据记录数
        /// </summary>
        /// <param name="eng">指定的工程</param>
        /// <param name="dut">工程中的被测物</param>
        /// <returns>测量数据记录数</returns>
        public async Task<long> CountDUTDataAsync(Engineering eng, DUT dut)
        {
            return await _dutRead.CountAsync((eng, dut));
        }

        /// <summary>
        /// 创建一个Care接驳器
        /// </summary>
        /// <param name="port">Care所在串口编号</param>
        /// <returns>是否创建成功</returns>
        public async Task<Slot> CreateMeterCareSlotAsync(short port)
        {
            var slot = new Slot();
            slot.SetMeterCare(SlotType.MeterCare, (port, new SerialConfig() {BaudRate = 115200}));
            var insertSuccess = await _slotStoragePlatform.InsertAsync(slot);
            return insertSuccess ? slot : null;
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
        /// 开始采集
        /// </summary>
        public async Task StartAcquireAsync()
        {
            if (CurrentActiveEngineering != null)
            {
                if (!_acquiringEngineerings.Contains(CurrentActiveEngineering))
                    _acquiringEngineerings.Add(CurrentActiveEngineering);
                foreach (var slot in CurrentActiveEngineering.GetIncludedSlots())
                {
                    var config = JsonConvert.DeserializeObject<(short, SerialConfig)>(slot.Config);
                    slot.SetMeterCare(slot.SlotType, config);
                    _performLogic.SetDUTMap(CurrentActiveEngineering.CommandPools, CurrentActiveEngineering);
                    _antService.Bind((slot, Kernel.Container.Resolve<IDataConnector>()));
                }

                EngineeringState es = EngineeringStateList.FirstOrDefault(state => state.EngineeringId == CurrentActiveEngineering.Id);
                if (es != null)
                    es.EngState = EngineeringState.State.Start;
                else
                    EngineeringStateList.Add(new EngineeringState(CurrentActiveEngineering.Id));
                await _antService.StartAsync(CurrentActiveEngineering);
            }
        }

        /// <summary>
        /// 暂停采集
        /// </summary>
        public void PauseAcquire()
        {
            if (CurrentActiveEngineering != null)
            {
                EngineeringState es = EngineeringStateList.FirstOrDefault(state => state.EngineeringId == CurrentActiveEngineering.Id);
                if (es != null) 
                    es.EngState = EngineeringState.State.Pause;
                _antService.Pause(CurrentActiveEngineering);
            }
        }

        /// <summary>
        /// 恢复采集
        /// </summary>
        public void ResumeAcquire()
        {
            if (CurrentActiveEngineering != null)
            {
                EngineeringState es = EngineeringStateList.FirstOrDefault(state => state.EngineeringId == CurrentActiveEngineering.Id);
                if (es != null)
                    es.EngState = EngineeringState.State.Start;
                _antService.Resume(CurrentActiveEngineering);
            }
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopAcquire()
        {
            if (CurrentActiveEngineering != null)
            {
                EngineeringState es = EngineeringStateList.FirstOrDefault(state => state.EngineeringId == CurrentActiveEngineering.Id);
                if (es != null)
                    es.EngState = EngineeringState.State.Stop;
                _antService.Stop(CurrentActiveEngineering);
            }
        }

        /// <summary>
        ///     创建一个被测物
        /// </summary>
        /// <param name="dut"></param>
        public async Task<bool> CreateDUTAsync(DUT dut)
        {
            return await _dutStoragePlatform.InsertAsync(dut);
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
