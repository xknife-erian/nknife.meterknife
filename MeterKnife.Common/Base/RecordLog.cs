using System;

namespace NKnife.MeterKnife.Common.Base
{
    public class RecordLog
    {
        /// <summary>
        /// 记录类
        /// </summary>
        /// <param name="user">记录人</param>
        /// <param name="datetime">记录时间</param>
        /// <param name="note">注释,备注信息</param>
        public RecordLog(string user, DateTime datetime, string note = "")
        {
            this.user = user;
            SetTimeString(datetime);
            this.note = note;
        }

        public void SetTimeString(DateTime datetime)
        {
            time = datetime.ToString("yyMMdd.HHmmss.fff");
        }

        public string user { get; set; }
        public string time { get; set; }
        public string note { get; set; }
    }
}