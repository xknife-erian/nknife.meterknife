using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public class WorkbenchViewModel : ObservableRecipient, IWorkbenchViewModel
    {
        private readonly IAntService _antService;

        private readonly IStoragePlatform<Instrument> _instrumentStoragePlatform;
        private readonly IStoragePlatform<Project> _projectStoragePlatform;
        private readonly IStoragePlatform<Slot> _slotStoragePlatform;
        private readonly IStoragePlatform<DUT> _dutStoragePlatform;

        private readonly IStorageDUTRead<MeasureData> _dutRead;

        private readonly IProjectLogic _projectLogic;
        private readonly IMeasuringLogic _performLogic;

        /// <summary>
        ///     按时间排序的工程列表，供工程列表窗体显示
        /// </summary>
        private readonly Dictionary<DateTime, List<Project>> _projectMap = new Dictionary<DateTime, List<Project>>();

        /// <summary>
        ///     已打开的工程列表
        /// </summary>
        private ObservableCollection<Project> _openedProjects = new ObservableCollection<Project>();

        /// <summary>
        ///     正在采集的工程列表
        /// </summary>
        private ObservableCollection<Project> _acquiringProjects = new ObservableCollection<Project>();

        /// <summary>
        ///     当前激活的工程
        /// </summary>
        private Project _currentProject;

        public WorkbenchViewModel(
            IAntService antService,
            IProjectLogic projectLogic, IMeasuringLogic performLogic,
            IStoragePlatform<Project> projectStoragePlatform,
            IStoragePlatform<Slot> slotStoragePlatform,
            IStoragePlatform<DUT> dutStoragePlatform,
            IStoragePlatform<Instrument> instrumentStoragePlatform,
            IStorageDUTRead<MeasureData> dutRead)
        {
            _projectStoragePlatform = projectStoragePlatform;
            _slotStoragePlatform = slotStoragePlatform;
            _dutStoragePlatform = dutStoragePlatform;
            _projectLogic = projectLogic;
            _instrumentStoragePlatform = instrumentStoragePlatform;
            _dutRead = dutRead;
            _performLogic = performLogic;
            _antService = antService;
        }

        /// <summary>
        ///     工程的采集状态
        /// </summary>
        public ObservableCollection<ProjectState> ProjectStateList { get; set; } = new ObservableCollection<ProjectState>();

        /// <summary>
        ///     当前激活（被选择）的工程
        /// </summary>
        public Project CurrentActiveProject
        {
            get => _currentProject;
            set => SetProperty(ref _currentProject, value);
        }

        /// <summary>
        ///     当前选择的工程
        /// </summary>
        public Project CurrentSelectedProject { get; set; }

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
        public async Task CreateProjectAsync(Project eng)
        {
            await _projectLogic.CreateProjectAsync(eng);
        }

        /// <summary>
        ///     修改一个工程
        /// </summary>
        public async Task UpdateProjectAsync(Project eng)
        {
            await _projectLogic.UpdateProjectAsync(eng);
        }

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        public async Task DeleteProjectAsync(Project eng)
        {
            await _projectLogic.RemoveProjectAsync(eng);
        }

        /// <summary>
        /// 获取所有工程，并按工程的创建时间倒序排列
        /// </summary>
        public async Task<Dictionary<DateTime, List<Project>>> GetProjectAndDateMapAsync()
        {
            _projectMap.Clear();
            var projects = (await _projectStoragePlatform.FindAllAsync()).ToList();
            projects.Sort((x, y) => y.CreateTime.CompareTo(x.CreateTime));
            foreach (var project in projects)
            {
                var date = new DateTime(project.CreateTime.Year, project.CreateTime.Month, 1, 0, 0, 0);
                if (_projectMap.TryGetValue(date, out var list))
                {
                    list.Add(project);
                }
                else
                {
                    list = new List<Project> {project};
                    _projectMap.Add(date, list);
                }
            }

            return _projectMap;
        }

        /// <summary>
        /// 是否存在相同编号的工程
        /// </summary>
        /// <param name="projectId">工程编号</param>
        /// <returns>是否存在</returns>
        public bool ExistProject(string projectId)
        {
            return _projectStoragePlatform.ExistAsync(projectId).Result;
        }

        /// <summary>
        ///     已打开的工程
        /// </summary>
        public ObservableCollection<Project> OpenedProjects
        {
            get => _openedProjects;
            set => SetProperty(ref _openedProjects, value);
        }

        /// <summary>
        ///     正在测量的工程
        /// </summary>
        public ObservableCollection<Project> AcquiringProjects
        {
            get => _acquiringProjects;
            set => SetProperty(ref _acquiringProjects, value);
        }

        /// <summary>
        /// 获取指定工程的被测物的测量数据记录数
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <param name="dut">工程中的被测物</param>
        /// <returns>测量数据记录数</returns>
        public async Task<long> CountDUTDataAsync(Project project, DUT dut)
        {
            return await _dutRead.CountAsync((project, dut));
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
            if (CurrentActiveProject != null)
            {
                if (!_acquiringProjects.Contains(CurrentActiveProject))
                    _acquiringProjects.Add(CurrentActiveProject);
                foreach (var slot in CurrentActiveProject.GetIncludedSlots())
                {
                    var config = JsonConvert.DeserializeObject<(short, SerialConfig)>(slot.Config);
                    slot.SetMeterCare(slot.SlotType, config);
                    _performLogic.SetDUTMap(CurrentActiveProject.CommandPools, CurrentActiveProject);
                    _antService.Bind((slot, Kernel.Container.Resolve<IDataConnector>()));
                }

                ProjectState es = ProjectStateList.FirstOrDefault(state => state.ProjectId == CurrentActiveProject.Id);
                if (es != null)
                    es.EngState = ProjectState.State.Start;
                else
                    ProjectStateList.Add(new ProjectState(CurrentActiveProject.Id));
                await _antService.StartAsync(CurrentActiveProject);
            }
        }

        /// <summary>
        /// 暂停采集
        /// </summary>
        public void PauseAcquire()
        {
            if (CurrentActiveProject != null)
            {
                ProjectState es = ProjectStateList.FirstOrDefault(state => state.ProjectId == CurrentActiveProject.Id);
                if (es != null) 
                    es.EngState = ProjectState.State.Pause;
                _antService.Pause(CurrentActiveProject);
            }
        }

        /// <summary>
        /// 恢复采集
        /// </summary>
        public void ResumeAcquire()
        {
            if (CurrentActiveProject != null)
            {
                ProjectState es = ProjectStateList.FirstOrDefault(state => state.ProjectId == CurrentActiveProject.Id);
                if (es != null)
                    es.EngState = ProjectState.State.Start;
                _antService.Resume(CurrentActiveProject);
            }
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopAcquire()
        {
            if (CurrentActiveProject != null)
            {
                ProjectState es = ProjectStateList.FirstOrDefault(state => state.ProjectId == CurrentActiveProject.Id);
                if (es != null)
                    es.EngState = ProjectState.State.Stop;
                _antService.Stop(CurrentActiveProject);
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
