using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Datas.Entities
{
    public class ExhibitInfomation
    {
        public int Id { get; set; }

        public IDevice Device { get; set; }

        public IExhibit Exhibit { get; set; }

        public List<string> Commands { get; set; } 
    }
}
