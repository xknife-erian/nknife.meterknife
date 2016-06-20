using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.DataModels
{
    public class FiguredDataFilter
    {
        public FiguredDataFilter()
        {
            InStatistical = true;
            IsSave = true;
        }

        public uint Multiple { get; set; }
        /// <summary>
        /// 异常值是否保存
        /// </summary>
        public bool IsSave { get; set; }
        /// <summary>
        /// 异常值是否参与统计
        /// </summary>
        public bool InStatistical { get; set; }

        public void Update(FiguredDataFilter figuredDataFilter)
        {
            if (figuredDataFilter != null)
            {
                Multiple = figuredDataFilter.Multiple;
                InStatistical = figuredDataFilter.InStatistical;
                IsSave = figuredDataFilter.IsSave;
            }
        }
    }
}
