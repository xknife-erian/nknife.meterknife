using System.Collections.Generic;
using MeterKnife.Base;

namespace MeterKnife.Models
{
    /// <summary>
    /// 描述一项测量工作的类型。
    /// 该测量工作是指在一套对一个或多个被测物循环环执行测量的过程。
    /// </summary>
    public class MeasureJob
    {
        public string Number { get; set; }
        public List<ExhibitBase> Exhibits { get; set; }
    }
}