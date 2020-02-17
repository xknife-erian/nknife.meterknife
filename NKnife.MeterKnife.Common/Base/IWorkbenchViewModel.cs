using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Common.Base
{
    public interface IWorkbenchViewModel
    {
        #region Engineering
        
        /// <summary>
        /// 获取所有工程，并按工程的创建时间倒序排列
        /// </summary>
        Task<Dictionary<string, List<Engineering>>> GetEngineeringAndDateMap();

        #endregion

        #region Slot

        /// <summary>
        /// 创建一个Care接驳器
        /// </summary>
        /// <param name="port">Care所在串口编号</param>
        /// <returns>是否创建成功</returns>
        Task<bool> CreatMeterCareSlot(short port);

        /// <summary>
        /// 获取所有的<see cref="Slot"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Slot>> GetAllSlotAsync();

        #endregion

        #region DUT



        #endregion

    }
}
