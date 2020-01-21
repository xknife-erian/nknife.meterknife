
namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 领域对象
    /// </summary>
    public interface IDomain : IRecord
    {
        /// <summary>
        /// 对象名，通常是显示出来的名称
        /// </summary>
        string Name { get; set; }
    }
}
