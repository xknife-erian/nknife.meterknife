using System.Collections.Generic;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Models.Exhibits
{
    public class ExhibitInfomation
    {
        public int Id { get; set; }

        public IDevice Device { get; set; }

        public IExhibit Exhibit { get; set; }

        public List<string> Commands { get; set; } 
    }
}
