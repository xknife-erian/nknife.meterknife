using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Base
{
    public interface IWorkbenchViewModel
    {
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
        ///     获取所有的仪器
        /// </summary>
        Task<IEnumerable<Instrument>> GetAllInstrumentAsync();

        #region Engineering

        #endregion

        /// <summary>
        ///     创建一个工程
        /// </summary>
        Task CreateEngineeringAsync(Engineering eng);

        /// <summary>
        ///     获取所有工程，并按工程的创建时间倒序排列
        /// </summary>
        Task<Dictionary<DateTime, List<Engineering>>> GetEngineeringAndDateMapAsync();

        /// <summary>
        ///     是否存在相同编号的工程
        /// </summary>
        /// <param name="engId">工程编号</param>
        /// <returns>是否存在</returns>
        bool ExistEngineering(string engId);

        /// <summary>
        ///     已打开的工程
        /// </summary>
        ObservableCollection<Engineering> OpenedEngineerings { get; set; }

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

    }
}