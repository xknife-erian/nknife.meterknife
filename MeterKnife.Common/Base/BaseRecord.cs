using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using NLog;

namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 记录的基础属性
    /// </summary>
    public abstract class BaseRecord : IRecord
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        protected BaseRecord()
        {
            Id = SequentialGuid.Create().ToString("N");
            State = RecordState.Normal;
            Logs = JsonConvert.SerializeObject(new List<RecordLog> {new RecordLog("sys", DateTime.Now, "Init")});
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [MaxLength(32)]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 采用Json格式存储其他不常用的属性
        /// </summary>
        public string Other { get; set; } = string.Empty;

        /// <summary>
        /// 采用Json格式存储记录的操作信息
        /// </summary>
        public string Logs { get; protected set; }

        /// <summary>
        /// 记录的当前状态
        /// </summary>
        public RecordState State { get; set; }

        /// <summary>
        /// 添加记录操作信息
        /// </summary>
        /// <param name="user">记录者</param>
        /// <param name="note">注释,注解</param>
        public virtual void AddLog(string user, string note = "")
        {
            var recordLogs = new List<RecordLog>(0);
            if (Logs != null)
            {
                try
                {
                    recordLogs = JsonConvert.DeserializeObject<List<RecordLog>>(Logs);
                }
                catch (Exception e)
                {
                    _Logger.Warn(e, $"历史日志信息无法解析:{Logs}");
                }
            }
            recordLogs.Add(new RecordLog(user, DateTime.Now, note));
            Logs = JsonConvert.SerializeObject(recordLogs);
        }
    }
}
