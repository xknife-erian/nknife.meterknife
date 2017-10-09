using System;

namespace MeterKnife.Electronics.Helper
{
    /// <summary>非标电阻值分解模式
    /// </summary>
    [Flags]
    public enum ResistanceValueResolveModel
    {
        /// <summary>
        /// 串联,大阻值优先
        /// </summary>
        SeriesBigValuePriority = 1,
        /// <summary>
        /// 串联,小阻值优先
        /// </summary>
        SeriesSmallValuePriority = 2,
        /// <summary>
        /// 简单并联
        /// </summary>
        ParallelingSimple = 4,

        All = SeriesBigValuePriority | SeriesSmallValuePriority | ParallelingSimple
    }
}
