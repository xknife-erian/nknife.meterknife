using System;
using System.Collections.Generic;
using System.IO;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Scpis;

namespace NKnife.MeterKnife.Scpis
{
    public class ScpiInfoGetter : IScpiInfoGetter
    {
        private readonly DirectoryInfo _directory = new DirectoryInfo(ScpiUtil.ScpisPath);

        public IEnumerable<CareCommandSubjectList> GetScpiSubjectCollections()
        {
            var files = _directory.GetFiles("*.xml", SearchOption.AllDirectories);
            var list = new List<CareCommandSubjectList>(files.Length);
            foreach (var file in files)
            {
                var collection = new CareCommandSubjectList();
                collection.BuildScpiFile(file.FullName);
                collection.TryParse(null);
                list.Add(collection);
            }
            return list;
        }

        public List<Tuple<string, string, string>> GetMeterInfoList()
        {
            var list = new List<Tuple<string, string, string>>();
            var files = _directory.GetFiles("*.xml", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var collection = new CareCommandSubjectList();
                collection.BuildScpiFile(file.FullName);
                collection.TryParse(null, false);//快速解析
                list.Add(new Tuple<string, string, string>(collection.Brand, collection.Name, collection.Description));
            }
            return list;
        }
    }
}