using System;
using System.Collections.Generic;
using System.IO;
using NKnife.Scpi;

namespace MeterKnife.Scpis
{
    public class ScpiInfoGetter : IScpiInfoGetter
    {
        private readonly DirectoryInfo _Directory = new DirectoryInfo(ScpiUtil.ScpisPath);

        public IEnumerable<ScpiSubjectCollection> GetScpiSubjectCollections()
        {
            var files = _Directory.GetFiles("*.xml", SearchOption.AllDirectories);
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

        public List<Tuple<string, string, string>> GetMeterInfoList()
        {
            var list = new List<Tuple<string, string, string>>();
            var files = _Directory.GetFiles("*.xml", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var collection = new ScpiSubjectCollection();
                collection.BuildScpiFile(file.FullName);
                collection.TryParse(null, false);//快速解析
                list.Add(new Tuple<string, string, string>(collection.Brand, collection.Name, collection.Description));
            }
            return list;
        }
    }
}