namespace NKnife.MeterKnife.Common.Base
{
    public interface IRecord
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 采用Json格式存储其他不常用的属性
        /// </summary>
        string Other { get; set; }

        /// <summary>
        /// 采用Json格式存储记录的操作信息
        /// </summary>
        string Logs { get;}

        /// <summary>
        /// 记录的当前状态
        /// </summary>
        RecordState State { get; set; }

        /// <summary>
        ///  添加记录操作信息
        /// </summary>
        /// <param name="user">记录者</param>
        /// <param name="note">注释,备注信息</param>
        void AddLog(string user, string note = "");
    }
}