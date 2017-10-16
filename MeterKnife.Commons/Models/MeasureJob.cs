using System;
using System.Collections.Generic;
using MeterKnife.Events;
using MeterKnife.Interfaces;
using MeterKnife.Scpis;

namespace MeterKnife.Models
{
    /// <summary>
    ///     描述一项测量事务的类型。
    ///     该测量事务是指在一套对一个或多个被测物循环环执行测量的过程，该过程可能会多次启动与停止。
    ///     本软件会将本过程所采集的数据单独存储在一个文件数据库中。
    /// </summary>
    public class MeasureJob
    {
        public MeasureJob()
        {
            Number = Guid.NewGuid();
        }
        public Guid Number { get; }
        public List<IExhibit> Exhibits { get; set; } = new List<IExhibit>(1);
        public List<Instrument> Instruments { get; set; } = new List<Instrument>(1);
        public List<Measure> Measures { get; set; } = new List<Measure>(1);

        /// <summary>
        ///     测量事务中“测量”事件的基接口，描述“测量”事件的相关属性。
        /// </summary>
        public class Measure
        {
            public Measure(MeasureJob job, ScpiSubject scpiSubject, DateTime startTime)
            {
                Job = job;
                ScpiSubject = scpiSubject;
                Start = startTime;
                job.Measures.Add(this);
            }
            public MeasureJob Job { get; }
            public ScpiSubject ScpiSubject { get; }
            public DateTime Start { get; }
            public DateTime Stop { get; set; }
        }

        #region Overrides of Object

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        public override bool Equals(object obj)
        {
            var it = obj as MeasureJob;
            if (it != null)
                return Equals(it);
            return false;
        }

        #region Equality members

        protected bool Equals(MeasureJob other)
        {
            return Number.Equals(other.Number);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        #endregion

        #endregion
    }
}