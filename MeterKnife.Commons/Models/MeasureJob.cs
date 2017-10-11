using System;
using System.Collections.Generic;
using MeterKnife.Interfaces;

namespace MeterKnife.Models
{
    /// <summary>
    ///     描述一项测量工作的类型。
    ///     该测量工作是指在一套对一个或多个被测物循环环执行测量的过程。
    ///     本软件会将本过程所采集的数据单独存储在一个文件数据库中。
    /// </summary>
    public class MeasureJob
    {
        public string Id { get; set; }
        public List<IExhibit> Exhibits { get; set; }
        public List<Instrument> Instruments { get; set; }
        public List<string> Commands { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
    }
}