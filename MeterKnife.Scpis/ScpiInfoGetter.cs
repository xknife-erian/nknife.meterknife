using System.Collections.Generic;
using System.IO;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public class ScpiInfoGetter : IScpiInfoGetter
    {
        public IEnumerable<ScpiSubjectCollection> GetScpiSubjectCollections()
        {
            var dir = new DirectoryInfo(ScpiUtil.ScpisPath);
            var files = dir.GetFiles("*.xml", SearchOption.AllDirectories);
            var list = new List<ScpiSubjectCollection>(files.Length);
            foreach (var file in files)
            {
                var collection = new ScpiSubjectCollection();
                collection.BuildScpiFile(file.FullName);
                collection.TryParse(null);
                list.Add(collection);
            }
            return list;
        }
    }
}