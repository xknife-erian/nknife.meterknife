// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IRecord<T>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        T Id { get; set; }
    }
}