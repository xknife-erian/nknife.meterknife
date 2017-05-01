using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Datas.Entities
{
    public class ExhibitItem
    {
        public int Id { get; set; }
        public IExhibit Exhibit { get; set; }
        public string RepositoryPath { get; set; }
    }
}