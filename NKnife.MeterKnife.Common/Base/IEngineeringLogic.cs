using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IEngineeringLogic
    {
        /// <summary>
        ///     新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        Task<bool> CreateEngineeringAsync(Engineering engineering);

        /// <summary>
        /// 获取所有工程
        /// </summary>
        Task<IEnumerable<Engineering>> GetAllEngineeringAsync();
    }
}