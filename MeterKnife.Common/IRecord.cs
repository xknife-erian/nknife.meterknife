namespace NKnife.MeterKnife.Common
{
    public interface IRecord<T>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        T Id { get; set; }
    }
}