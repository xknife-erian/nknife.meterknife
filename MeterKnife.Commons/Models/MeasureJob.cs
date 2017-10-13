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
        public string Id { get; set; }
        public List<IExhibit> Exhibits { get; set; } = new List<IExhibit>(1);
        public List<Instrument> Instruments { get; set; } = new List<Instrument>(1);
        public List<Measure> Durations { get; set; } = new List<Measure>(1);

        /// <summary>
        ///     “测量”功能的基接口，描述“测量”功能的核心函数与事件。
        /// </summary>
        public class Measure
        {
            public ScpiSubject ScpiSubject { get; set; }
            public DateTime Start { get; set; }
            public DateTime Stop { get; set; }
        }
    }
}