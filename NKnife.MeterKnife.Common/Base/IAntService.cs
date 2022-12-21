using System.Threading.Tasks;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util.Tunnel;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IAntService
    {
        /// <summary>
        ///     绑定一个指定插槽的通讯服务
        /// </summary>
        /// <param name="slots">指定的插槽与该插槽的连接器</param>
        void Bind(params (Slot, IDataConnector)[] slots);

        /// <summary>
        ///     移除指定的插槽
        /// </summary>
        /// <param name="slot">指定的插槽</param>
        void UnBind(Slot slot);

        /// <summary>
        ///     启动指定的工程
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <returns>启动是否成功</returns>
        Task<bool> StartAsync(Project project);

        /// <summary>
        ///     启动指定的工程
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <returns>启动是否成功</returns>
        bool Pause(Project project);

        /// <summary>
        ///     停止指定的工程
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <returns>停止是否成功</returns>
        bool Stop(Project project);

        /// <summary>
        ///     恢复指定的工程
        /// </summary>
        /// <param name="project">指定的工程</param>
        /// <returns>停止是否成功</returns>
        bool Resume(Project project);
    }
}