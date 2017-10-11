
using MeterKnife.Interfaces;

namespace MeterKnife.Models.Datas
{
    public class ExhibitItem
    {
        public int Id { get; set; }
        public IExhibit Exhibit { get; set; }
        public string RepositoryPath { get; set; }
    }
}