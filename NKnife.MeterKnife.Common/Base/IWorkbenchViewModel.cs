using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Base
{
    public interface IWorkbenchViewModel
    {
        /// <summary>
        ///     工程的采集状态
        /// </summary>
        ObservableCollection<ProjectState> ProjectStateList { get; set; }

        /// <summary>
        ///     当前激活的工程
        /// </summary>
        Project CurrentActiveProject { get; set; }

        /// <summary>
        ///     当前选择的工程
        /// </summary>
        Project CurrentSelectedProject { get; set; }

        #region DUT

        /// <summary>
        ///     创建一个被测物
        /// </summary>
        /// <param name="dut"></param>
        Task<bool> CreateDUTAsync(DUT dut);

        /// <summary>
        ///     获取所有的<see cref="DUT" />
        /// </summary>
        Task<IEnumerable<DUT>> GetAllDUTAsync();

        #endregion

        #region Instrument

        /// <summary>
        ///     创建一台仪器
        /// </summary>
        Task CreateInstrumentAsync(Instrument inst);

        /// <summary>
        ///     修改一台仪器
        /// </summary>
        Task UpdateInstrumentAsync(Instrument inst);

        /// <summary>
        ///     删除一台仪器
        /// </summary>
        Task DeleteInstrumentAsync(Instrument inst);

        /// <summary>
        ///     获取所有的仪器
        /// </summary>
        Task<IEnumerable<Instrument>> GetAllInstrumentAsync();

        #region Project

        #endregion

        /// <summary>
        ///     创建一个工程
        /// </summary>
        Task CreateProjectAsync(Project eng);

        /// <summary>
        ///     修改一个工程
        /// </summary>
        Task UpdateProjectAsync(Project eng);

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        Task DeleteProjectAsync(Project eng);

        /// <summary>
        ///     获取所有工程，并按工程的创建时间倒序排列
        /// </summary>
        Task<Dictionary<DateTime, List<Project>>> GetProjectAndDateMapAsync();

        /// <summary>
        ///     是否存在相同编号的工程
        /// </summary>
        /// <param name="projectId">工程编号</param>
        /// <returns>是否存在</returns>
        bool ExistProject(string projectId);

        /// <summary>
        ///     已打开的工程
        /// </summary>
        ObservableCollection<Project> OpenedProjects { get; set; }

        /// <summary>
        ///     正在测量的工程
        /// </summary>
        ObservableCollection<Project> AcquiringProjects { get; set; }

        /// <summary>
        /// 获取指定工程的被测物的测量数据记录数
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <param name="dut">工程中的被测物</param>
        /// <returns>测量数据记录数</returns>
        Task<long> CountDUTDataAsync(Project project, DUT dut);

        #endregion

        #region Slot

        /// <summary>
        ///     创建一个Care接驳器
        /// </summary>
        /// <param name="port">Care所在串口编号</param>
        /// <returns>是否创建成功</returns>
        Task<Slot> CreateMeterCareSlotAsync(short port);

        /// <summary>
        ///     获取所有的<see cref="Slot" />
        /// </summary>
        Task<IEnumerable<Slot>> GetAllSlotAsync();

        #endregion

        #region Acquire

        /// <summary>
        /// 开始采集
        /// </summary>
        Task StartAcquireAsync();

        /// <summary>
        /// 暂停采集
        /// </summary>
        void PauseAcquire();

        /// <summary>
        /// 恢复采集
        /// </summary>
        void ResumeAcquire();

        /// <summary>
        /// 停止采集
        /// </summary>
        void StopAcquire();

        #endregion

    }
}